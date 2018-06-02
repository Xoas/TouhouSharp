﻿using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using OpenTK;
using OpenTK.Graphics;
using Symcol.Core.NeuralNetworking;
using touhou.sharp.Game.Config;

namespace touhou.sharp.Game.Characters.VitaruPlayers.DrawableVitaruPlayers
{
    public class DrawableTHSharpPlayer : Character
    {
        #region Fields
        protected readonly Gamemodes Gamemode = THSharpSettings.THSharpConfigManager.GetBindable<Gamemodes>(THSharpSetting.GameMode);

        protected readonly GraphicsOptions PlayerVisuals = THSharpSettings.THSharpConfigManager.GetBindable<GraphicsOptions>(THSharpSetting.PlayerVisuals);

        private readonly DebugConfiguration configuration = THSharpSettings.THSharpConfigManager.GetBindable<DebugConfiguration>(THSharpSetting.DebugConfiguration);

        protected readonly THSharpNeuralContainer THSharpNeuralContainer;

        public readonly THSharpPlayer Player;

        protected override string CharacterName => Player.FileName;

        public override double MaxHealth => Player.MaxHealth;

        public override Color4 PrimaryColor => Player.PrimaryColor;

        public override Color4 SecondaryColor => Player.SecondaryColor;

        public override Color4 ComplementaryColor => Player.ComplementaryColor;

        public int ScoreZone = 100;

        public double SpeedMultiplier = 1;

        public Dictionary<THSharpAction, bool> Actions = new Dictionary<THSharpAction, bool>();

        //(MinX,MaxX,MinY,MaxY)
        protected Vector4 PlayerBounds
        {
            get
            {
                if (Gamemode == Gamemodes.Touhosu)
                    return new Vector4(0, 512, 0, 820);
                else
                    return new Vector4(0, 512, 0, 820);
            }
        }

        protected readonly Container Cursor;

        protected readonly THSharpNetworkingClientHandler THSharpNetworkingClientHandler;

        /// <summary>
        /// Are we a slave over the net?
        /// </summary>
        public bool Puppet { get; set; }

        /// <summary>
        /// If we will control ourselves
        /// </summary>
        public bool Auto { get; set; }

        protected bool HealthHacks { get; private set; }

        protected bool BoundryHacks { get; private set; }

        //Is reset after healing applied
        public double HealingMultiplier = 1;

        public string PlayerID;

        private double lastQuarterBeat = -1;
        private double nextHalfBeat = -1;
        private double nextQuarterBeat = -1;
        private double beatLength = 1000;

        protected List<HealingBullet> HealingBullets { get; private set; } = new List<HealingBullet>();

        protected const double HEALING_FALL_OFF = 0.85d;

        private const double field_of_view = 120;

        private const double healing_range = 64;
        private const double healing_min = 0.5d;
        private const double healing_max = 2d;
        #endregion

        public DrawableTHSharpPlayer(THSharpPlayfield playfield, THSharpPlayer player, THSharpNetworkingClientHandler vitaruNetworkingClientHandler) : base(playfield)
        {
            Player = player;
            THSharpNetworkingClientHandler = vitaruNetworkingClientHandler;

            Add(THSharpNeuralContainer = new THSharpNeuralContainer(playfield, this));

            THSharpNeuralContainer.Pressed = Pressed;
            THSharpNeuralContainer.Released = Released;

            Actions[THSharpAction.Up] = false;
            Actions[THSharpAction.Down] = false;
            Actions[THSharpAction.Left] = false;
            Actions[THSharpAction.Right] = false;
            Actions[THSharpAction.Slow] = false;
            Actions[THSharpAction.Shoot] = false;

            THSharpPlayfield.GameField.Add(Cursor = new Container
            {
                Anchor = Anchor.TopLeft,
                Origin = Anchor.Centre,
                Size = new Vector2(4),
                CornerRadius = 2,
                Alpha = 0.2f,
                Masking = true,

                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!Puppet)
            {
                switch (configuration)
                {
                    case DebugConfiguration.General:
                        DebugToolkit.GeneralDebugItems.Add(new DebugAction(() => { Auto = !Auto; }) { Text = "Auto Hacks" });
                        DebugToolkit.GeneralDebugItems.Add(new DebugAction(() => { BoundryHacks = !BoundryHacks; DrawableBullet.BoundryHacks = !DrawableBullet.BoundryHacks; }) { Text = "Boundry Hacks" });
                        DebugToolkit.GeneralDebugItems.Add(new DebugAction(() => { HealthHacks = !HealthHacks; }) { Text = "Health Hacks" });
                        break;
                    case DebugConfiguration.NeuralNetworking:
                        Bindable<NeuralNetworkState> bindable = THSharpSettings.THSharpConfigManager.GetBindable<NeuralNetworkState>(THSharpSetting.NeuralNetworkState);
                        bindable.ValueChanged += value => { THSharpNeuralContainer.TensorFlowBrain.NeuralNetworkState = value; };
                        bindable.TriggerChange();

                        DebugToolkit.MachineLearningDebugItems.Add(new DebugStat<NeuralNetworkState>(bindable) { Text = "Neural Network State" });
                        DebugToolkit.MachineLearningDebugItems.Add(new DebugAction(() => { bindable.Value = NeuralNetworkState.Idle; }, false) { Text = "Set Idle State" });
                        DebugToolkit.MachineLearningDebugItems.Add(new DebugAction(() => { bindable.Value = NeuralNetworkState.Learning; }, false) { Text = "Set Learning State" });
                        DebugToolkit.MachineLearningDebugItems.Add(new DebugAction(() => { bindable.Value = NeuralNetworkState.Active; }, false) { Text = "Set Active State" });
                        DebugToolkit.MachineLearningDebugItems.Add(new DebugAction(() => { HealthHacks = !HealthHacks; }) { Text = "Health Hacks" });
                        break;
                }
            }
        }

        protected override void LoadAnimationSprites(TextureStore textures, Storage storage)
        {
            base.LoadAnimationSprites(textures, storage);

            RightSprite.Texture = THSharpSkinElement.LoadSkinElement(CharacterName + "Right", storage);
            KiaiRightSprite.Texture = THSharpSkinElement.LoadSkinElement(CharacterName + "KiaiRight", storage);
        }

        #region Beat Handling
        protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, TrackAmplitudes amplitudes)
        {
            base.OnNewBeat(beatIndex, timingPoint, effectPoint, amplitudes);

            float amplitudeAdjust = Math.Min(1, 0.4f + amplitudes.Maximum);

            beatLength = timingPoint.BeatLength;

            OnHalfBeat();
            lastQuarterBeat = Time.Current;
            nextHalfBeat = Time.Current + timingPoint.BeatLength / 2;
            nextQuarterBeat = Time.Current + timingPoint.BeatLength / 4;

            const double beat_in_time = 60;

            Seal.Sign.ScaleTo(1 - 0.02f * amplitudeAdjust, beat_in_time, Easing.Out);
            using (Seal.Sign.BeginDelayedSequence(beat_in_time))
                Seal.Sign.ScaleTo(1, beatLength * 2, Easing.OutQuint);

            if (effectPoint.KiaiMode && Gamemode != Gamemodes.Touhosu)
            {
                Seal.Sign.FadeTo(0.25f * amplitudeAdjust, beat_in_time, Easing.Out);
                using (Seal.Sign.BeginDelayedSequence(beat_in_time))
                    Seal.Sign.FadeOut(beatLength);
            }

            if (effectPoint.KiaiMode && SoulContainer.Alpha == 1 && PlayerVisuals != GraphicsOptions.StandardV2)
            {
                if (!Dead && Gamemode != Gamemodes.Gravaru)
                {
                    KiaiContainer.FadeInFromZero(timingPoint.BeatLength / 4);
                    SoulContainer.FadeOutFromOne(timingPoint.BeatLength / 4);
                }

                if (Gamemode != Gamemodes.Touhosu)
                    Seal.Sign.FadeTo(0.15f, timingPoint.BeatLength / 4);
            }
            if (!effectPoint.KiaiMode && KiaiContainer.Alpha == 1 && PlayerVisuals != GraphicsOptions.StandardV2)
            {
                if (!Dead && Gamemode != Gamemodes.Gravaru)
                {
                    SoulContainer.FadeInFromZero(timingPoint.BeatLength);
                    KiaiContainer.FadeOutFromOne(timingPoint.BeatLength);
                }

                if (Gamemode != Gamemodes.Touhosu)
                    Seal.Sign.FadeTo(0f, timingPoint.BeatLength);
            }
        }

        protected virtual void OnHalfBeat()
        {
            nextHalfBeat = -1;

            if (Actions[THSharpAction.Shoot])
                PatternWave();
        }

        protected virtual void OnQuarterBeat()
        {
            lastQuarterBeat = nextQuarterBeat;
            nextQuarterBeat += beatLength / 4;

            if (HealingBullets.Count > 0)
            {
                double fallOff = 1;

                for (int i = 0; i < HealingBullets.Count - 1; i++)
                    fallOff *= HEALING_FALL_OFF;

                foreach (HealingBullet HealingBullet in HealingBullets)
                {
                    Heal((GetBulletHealingMultiplier(HealingBullet.EdgeDistance) * fallOff) * HealingMultiplier);
                }
                HealingBullets = new List<HealingBullet>();
                HealingMultiplier = 1;

                if (Gamemode != Gamemodes.Touhosu)
                {
                    Seal.Sign.Alpha = 0.2f;
                    Seal.Sign.FadeOut(beatLength / 4);
                }
            }
        }
        #endregion

        protected override void Update()
        {
            base.Update();

            if (HealthHacks)
                Heal(999999);

            foreach (Drawable draw in THSharpPlayfield.GameField.QuarterAbstraction)
                if (draw is DrawableBullet bullet && bullet.Hitbox != null)
                    ParseBullet(bullet);

            foreach (Drawable draw in THSharpPlayfield.GameField.HalfAbstraction)
                if (draw is DrawableBullet bullet && bullet.Hitbox != null)
                    ParseBullet(bullet);

            foreach (Drawable draw in THSharpPlayfield.GameField.FullAbstraction)
                if (draw is DrawableBullet bullet && bullet.Hitbox != null)
                    ParseBullet(bullet);

            Position = GetNewPlayerPosition(0.25d);

            if (Auto)
            {
                Character closestCharacter = null;
                double closestCharaterDistance = double.MaxValue;

                foreach (Drawable draw in THSharpPlayfield.GameField.Current)
                    if (draw is Character character)
                    {
                        if (character.Team != Team)
                        {
                            Vector2 object2Pos = character.ToSpaceOfOtherDrawable(Vector2.Zero, this) + new Vector2(6);
                            double distance = Math.Sqrt(Math.Pow(object2Pos.X, 2) + Math.Pow(object2Pos.Y, 2));

                            if (distance < 0)
                                distance *= -1;

                            if (distance < closestCharaterDistance)
                            {
                                closestCharacter = character;
                                closestCharaterDistance = distance;
                            }
                        }
                    }

                Cursor.Position = closestCharacter.Position;
            }
            else if (!Puppet)
                Cursor.Position = THSharpCursor.CenterCircle.ToSpaceOfOtherDrawable(Vector2.Zero, Parent) + new Vector2(6);

            if (nextHalfBeat <= Time.Current && nextHalfBeat != -1)
                OnHalfBeat();

            if (nextQuarterBeat <= Time.Current && nextQuarterBeat != -1)
                OnQuarterBeat();
        }

        protected override void ParseBullet(DrawableBullet bullet)
        {
            base.ParseBullet(bullet);

            //Not sure why this offset is needed atm
            Vector2 object2Pos = bullet.ToSpaceOfOtherDrawable(Vector2.Zero, this) + new Vector2(6);
            double distance = Math.Sqrt(Math.Pow(object2Pos.X, 2) + Math.Pow(object2Pos.Y, 2));
            double edgeDistance = distance - (bullet.Width / 2 + Hitbox.Width / 2);

            if (edgeDistance < 64 && bullet.Bullet.Team != Team)
            {
                bool add = true;
                foreach (HealingBullet HealingBullet in HealingBullets)
                    if (HealingBullet.DrawableBullet == bullet)
                    {
                        HealingBullet.EdgeDistance = edgeDistance;
                        add = false;
                    }

                if (add)
                    HealingBullets.Add(new HealingBullet(bullet, edgeDistance));
            }

            if (Gamemode == Gamemodes.Dodge)
                edgeDistance *= 1.5f;

            if (edgeDistance <= 64 && bullet.ScoreZone < 300)
                bullet.ScoreZone = 300;
            else if (edgeDistance <= 128 && bullet.ScoreZone < 100)
                bullet.ScoreZone = 100;
        }

        protected class HealingBullet
        {
            public readonly DrawableBullet DrawableBullet;

            public double EdgeDistance { get; set; }

            public HealingBullet(DrawableBullet bullet, double distance)
            {
                DrawableBullet = bullet;
                EdgeDistance = distance;
            }
        }

        protected virtual Vector2 GetNewPlayerPosition(double playerSpeed)
        {
            Vector2 playerPosition = Position;

            double yTranslationDistance = playerSpeed * Clock.ElapsedFrameTime * SpeedMultiplier;
            double xTranslationDistance = playerSpeed * Clock.ElapsedFrameTime * SpeedMultiplier;

            if (Auto)
            {
                Actions[THSharpAction.Up] = false;
                Actions[THSharpAction.Down] = false;
                Actions[THSharpAction.Left] = false;
                Actions[THSharpAction.Right] = false;
                Actions[THSharpAction.Slow] = false;
                Actions[THSharpAction.Shoot] = false;
                VisibleHitbox.Alpha = 0;

                DrawableBullet closestBullet = null;
                float closestBulletEdgeDitance = float.MaxValue;
                float closestBulletAngle = 0;

                DrawableBullet secondClosestBullet = null;
                float secondClosestBulletEdgeDitance = float.MaxValue;
                float secondClosestBulletAngle = 0;

                //bool bulletBehind = false;
                float behindBulletEdgeDitance = float.MaxValue;
                float behindBulletAngle = 0;

                foreach (Drawable draw in THSharpPlayfield.GameField.Current)
                    if (draw is DrawableBullet)
                    {
                        DrawableBullet bullet = draw as DrawableBullet;
                        if (bullet.Bullet.Team != Team)
                        {
                            Vector2 pos = bullet.ToSpaceOfOtherDrawable(Vector2.Zero, this);
                            float distance = (float)Math.Sqrt(Math.Pow(pos.X, 2) + Math.Pow(pos.Y, 2));
                            float edgeDistance = distance - (bullet.Width / 2 + Hitbox.Width / 2);
                            float angleToBullet = MathHelper.RadiansToDegrees((float)Math.Atan2((bullet.Position.Y - Position.Y), (bullet.Position.X - Position.X))) + 90 + Rotation;

                            if (closestBulletAngle < 360 - field_of_view | closestBulletAngle < -field_of_view && closestBulletAngle > field_of_view | closestBulletAngle > 360 + field_of_view)
                                if (closestBullet.Position.X > Position.X && bullet.Position.X < Position.X || closestBullet.Position.X < Position.X && bullet.Position.X > Position.X)
                                {
                                    //bulletBehind = true;
                                    behindBulletEdgeDitance = edgeDistance;
                                    behindBulletAngle = angleToBullet;
                                }

                            if (edgeDistance < closestBulletEdgeDitance)
                            {
                                secondClosestBullet = closestBullet;
                                secondClosestBulletEdgeDitance = closestBulletEdgeDitance;
                                secondClosestBulletAngle = closestBulletAngle;

                                closestBullet = bullet;
                                closestBulletEdgeDitance = edgeDistance;
                                closestBulletAngle = angleToBullet;
                            }
                        }
                    }

                if (closestBulletEdgeDitance <= 20)
                {
                    if (closestBulletEdgeDitance <= 16 && closestBulletEdgeDitance >= 8)
                    {
                        Actions[THSharpAction.Down] = true;
                        Actions[THSharpAction.Slow] = true;
                    }

                    if ((closestBulletAngle > 360 - field_of_view || closestBulletAngle > -field_of_view) && (closestBulletAngle < field_of_view || closestBulletAngle < 360 + field_of_view) && secondClosestBulletEdgeDitance - closestBulletEdgeDitance >= 1)
                    {
                        if (closestBullet.X < Position.X)
                            Actions[THSharpAction.Right] = true;
                        else
                            Actions[THSharpAction.Left] = true;
                    }
                }
                else
                {
                    if (Position.X > 512 - 250)
                        Actions[THSharpAction.Left] = true;
                    else if (Position.X < 250)
                        Actions[THSharpAction.Right] = true;

                    if (Position.Y < 640)
                        Actions[THSharpAction.Down] = true;
                    else if (Position.Y > 680)
                        Actions[THSharpAction.Up] = true;
                }

                Actions[THSharpAction.Shoot] = true;

                if (Actions[THSharpAction.Slow])
                    VisibleHitbox.Alpha = 1;
            }

            if (Actions[THSharpAction.Slow])
            {
                xTranslationDistance /= 2;
                yTranslationDistance /= 2;
            }

            if (Actions[THSharpAction.Up])
                playerPosition.Y -= (float)yTranslationDistance;
            if (Actions[THSharpAction.Left])
                playerPosition.X -= (float)xTranslationDistance;
            if (Actions[THSharpAction.Down])
                playerPosition.Y += (float)yTranslationDistance;
            if (Actions[THSharpAction.Right])
                playerPosition.X += (float)xTranslationDistance;

            if (!BoundryHacks)
            {
                playerPosition = Vector2.ComponentMin(playerPosition, PlayerBounds.Yw);
                playerPosition = Vector2.ComponentMax(playerPosition, PlayerBounds.Xz);
            }

            return playerPosition;
        }

        protected double GetBulletHealingMultiplier(double value)
        {
            double scale = (healing_max - healing_min) / (0 - healing_range);
            return healing_min + ((value - healing_range) * scale);
        }

        protected override void Death()
        {
            //base.Death();
        }

        #region Shooting Handling
        private void bulletAddRad(double speed, double angle, Color4 color, double size, double damage)
        {
            DrawableBullet drawableBullet;

            THSharpPlayfield.GameField.Add(drawableBullet = new DrawableBullet(new Bullet
            {
                StartTime = Time.Current,
                Position = Position,
                BulletAngle = angle,
                BulletSpeed = speed,
                BulletDiameter = size,
                BulletDamage = damage,
                ColorOverride = color,
                Team = Team,
                DummyMode = true,
                SliderType = SliderType.Straight,
                Abstraction = 3,
            }, THSharpPlayfield));

            //if (vampuric)
            //drawableBullet.OnHit = () => Heal(0.5f);
            drawableBullet.MoveTo(Position);
        }

        protected void PatternWave()
        {
            const int numberbullets = 3;
            double directionModifier = -0.2d;
            Color4 color = PrimaryColor;
            double size = 16;
            double damage = 18;

            double cursorAngle = 0;

            if (Actions[THSharpAction.Slow])
            {
                cursorAngle = (MathHelper.RadiansToDegrees(Math.Atan2((Cursor.Position.Y - Position.Y), (Cursor.Position.X - Position.X))) + 90 + Rotation) - 12;
                directionModifier = 0.1d;
            }

            for (int i = 1; i <= numberbullets; i++)
            {
                if (i % 2 == 0)
                {
                    size = 20;
                    damage = 24;
                    color = PrimaryColor;
                }
                else
                {
                    size = 12;
                    damage = 18;
                    color = SecondaryColor;
                }

                //-90 = up
                bulletAddRad(1, MathHelper.DegreesToRadians(-90 + cursorAngle) + directionModifier, color, size, damage);

                if (Actions[THSharpAction.Slow])
                    directionModifier += 0.1d;
                else
                    directionModifier += 0.2d;
            }
        }
        #endregion

        #region Input Handling
        public override bool ReceiveMouseInputAt(Vector2 screenSpacePos) => true;

        protected virtual void Pressed(THSharpAction action)
        {
            //Keyboard Stuff
            if (action == THSharpAction.Up)
                Actions[THSharpAction.Up] = true;
            if (action == THSharpAction.Down)
                Actions[THSharpAction.Down] = true;
            if (action == THSharpAction.Left)
                Actions[THSharpAction.Left] = true;
            if (action == THSharpAction.Right)
                Actions[THSharpAction.Right] = true;
            if (action == THSharpAction.Slow)
            {
                Actions[THSharpAction.Slow] = true;
                VisibleHitbox.Alpha = 1;
            }

            //Mouse Stuff
            if (action == THSharpAction.Shoot)
                Actions[THSharpAction.Shoot] = true;

            sendPacket(action);
        }

        protected virtual void Released(THSharpAction action)
        {
            //Keyboard Stuff
            if (action == THSharpAction.Up)
                Actions[THSharpAction.Up] = false;
            if (action == THSharpAction.Down)
                Actions[THSharpAction.Down] = false;
            if (action == THSharpAction.Left)
                Actions[THSharpAction.Left] = false;
            if (action == THSharpAction.Right)
                Actions[THSharpAction.Right] = false;
            if (action == THSharpAction.Slow)
            {
                Actions[THSharpAction.Slow] = false;
                VisibleHitbox.Alpha = 0;
            }
            //Mouse Stuff
            if (action == THSharpAction.Shoot)
                Actions[THSharpAction.Shoot] = false;

            sendPacket(THSharpAction.None, action);
        }
        #endregion

        public static DrawableTHSharpPlayer GetDrawableTHSharpPlayer(THSharpPlayfield playfield, string name, THSharpNetworkingClientHandler vitaruNetworkingClientHandler)
        {
            switch (name)
            {
                default:
                    return new DrawableTHSharpPlayer(playfield, THSharpPlayer.GetTHSharpPlayer(name), vitaruNetworkingClientHandler);
                case "Alex":
                    return new DrawableTHSharpPlayer(playfield, THSharpPlayer.GetTHSharpPlayer(name), vitaruNetworkingClientHandler);
            }
        }
    }
}

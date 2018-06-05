using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;
using THSharp.Game.Graphics.UI;
using THSharp.Game.NeuralNetworking;
using THSharp.Gamemodes.TouhouSharp.Playfield;
using THSharp.Gamemodes.TouhouSharp.Projectiles;
using THSharp.Gamemodes.TouhouSharp.Projectiles.DrawableProjectiles;

namespace THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters
{
    public abstract class DrawableTouhouSharpPlayer<P> : DrawableTouhouSharpCharacter<P>
        where P : TouhouSharpPlayer
    {
        protected readonly THSharpInputHandler THSharpInputHandler;

        public Dictionary<THSharpAction, bool> Actions = new Dictionary<THSharpAction, bool>();

        //(MinX,MaxX,MinY,MaxY)
        protected Vector4 PlayerBounds = new Vector4(0, 512, 0, 820);

        protected readonly SymcolContainer Cursor;

        protected double LastX;

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public double Energy { get; private set; }

        protected DrawableTouhouSharpPlayer(P p, TouhouSharpPlayfield playfield)
            : base(p, playfield)
        {
            Add(THSharpInputHandler = new THSharpInputHandler());

            THSharpInputHandler.Pressed = Pressed;
            THSharpInputHandler.Released = Released;

            Actions[THSharpAction.Up] = false;
            Actions[THSharpAction.Down] = false;
            Actions[THSharpAction.Left] = false;
            Actions[THSharpAction.Right] = false;
            Actions[THSharpAction.Slow] = false;
            Actions[THSharpAction.Shoot] = false;

            playfield.GameField.Add(Cursor = new SymcolContainer
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

        private double lastShootTime = double.MinValue;

        protected override void Update()
        {
            base.Update();

            Position = GetNewPlayerPosition(0.25d);

            if (Time.Current >= lastShootTime + 200 && Actions[THSharpAction.Shoot])
            {
                PatternWave();
                lastShootTime = Time.Current;
            }

            Cursor.Position = THSharpCursor.CenterCircle.ToSpaceOfOtherDrawable(Vector2.Zero, Parent) + new Vector2(6);
        }

        protected virtual Vector2 GetNewPlayerPosition(double playerSpeed)
        {
            Vector2 playerPosition = Position;

            double yTranslationDistance = playerSpeed * Clock.ElapsedFrameTime;
            double xTranslationDistance = playerSpeed * Clock.ElapsedFrameTime;

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

            playerPosition = Vector2.ComponentMin(playerPosition, PlayerBounds.Yw);
            playerPosition = Vector2.ComponentMax(playerPosition, PlayerBounds.Xz);

            return playerPosition;
        }

        #region Shooting Handling
        private void bulletAddRad(double speed, double angle, Color4 color, double size, double damage)
        {
            DrawableBullet drawableBullet;

            TouhouSharpPlayfield.GameField.Add(drawableBullet = new DrawableBullet(new Bullet
            {
                //StartTime = Time.Current,
                StartPosition = Position,
                Angle = angle,
                Speed = speed,
                Diameter = size,
                Damage = damage,
                Color = color,
                Team = Character.Team,
                //DummyMode = true,
                //SliderType = SliderType.Straight,
                //Abstraction = 3,
            }));

            //if (vampuric)
            //drawableBullet.OnHit = () => Heal(0.5f);
            drawableBullet.MoveTo(Position);
        }

        protected void PatternWave()
        {
            const int numberbullets = 3;
            double directionModifier = -0.2d;

            double cursorAngle = 0;

            if (Actions[THSharpAction.Slow])
            {
                cursorAngle = MathHelper.RadiansToDegrees(Math.Atan2(Cursor.Position.Y - Position.Y, Cursor.Position.X - Position.X)) + 90 + Rotation - 12;
                directionModifier = 0.1d;
            }

            for (int i = 1; i <= numberbullets; i++)
            {
                Color4 color;
                double size;
                double damage;
                if (i % 2 == 0)
                {
                    size = 20;
                    damage = 24;
                    color = Character.PrimaryColor;
                }
                else
                {
                    size = 12;
                    damage = 18;
                    color = Character.SecondaryColor;
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
            switch (action)
            {
                //Keyboard Stuff
                case THSharpAction.Up:
                    Actions[THSharpAction.Up] = true;
                    break;
                case THSharpAction.Down:
                    Actions[THSharpAction.Down] = true;
                    break;
                case THSharpAction.Left:
                    Actions[THSharpAction.Left] = true;
                    break;
                case THSharpAction.Right:
                    Actions[THSharpAction.Right] = true;
                    break;
                case THSharpAction.Slow:
                    Actions[THSharpAction.Slow] = true;
                    VisibleHitbox.Alpha = 1;
                    break;
                //Mouse Stuff
                case THSharpAction.Shoot:
                    Actions[THSharpAction.Shoot] = true;
                    break;
            }

            //sendPacket(action);
        }

        protected virtual void Released(THSharpAction action)
        {
            switch (action)
            {
                //Keyboard Stuff
                case THSharpAction.Up:
                    Actions[THSharpAction.Up] = false;
                    break;
                case THSharpAction.Down:
                    Actions[THSharpAction.Down] = false;
                    break;
                case THSharpAction.Left:
                    Actions[THSharpAction.Left] = false;
                    break;
                case THSharpAction.Right:
                    Actions[THSharpAction.Right] = false;
                    break;
                case THSharpAction.Slow:
                    Actions[THSharpAction.Slow] = false;
                    VisibleHitbox.Alpha = 0;
                    break;
                //Mouse Stuff
                case THSharpAction.Shoot:
                    Actions[THSharpAction.Shoot] = false;
                    break;
            }

            //sendPacket(VitaruAction.None, action);
        }
        #endregion

        #region Networking Handling
        /*
        private void sendPacket(VitaruAction pressedAction = VitaruAction.None, VitaruAction releasedAction = VitaruAction.None)
        {
            if (VitaruNetworkingClientHandler != null && !Puppet)
            {
                VitaruPlayerInformation playerInformation = new VitaruPlayerInformation
                {
                    Character = Player.FileName,
                    CursorX = Cursor.Position.X,
                    CursorY = Cursor.Position.Y,
                    PlayerX = Position.X,
                    PlayerY = Position.Y,
                    PlayerID = PlayerID,
                    PressedAction = pressedAction,
                    ReleasedAction = releasedAction,
                };

                ClientInfo clientInfo = new ClientInfo
                {
                    IP = VitaruNetworkingClientHandler.ClientInfo.IP,
                    Port = VitaruNetworkingClientHandler.ClientInfo.Port
                };

                VitaruInMatchPacket packet = new VitaruInMatchPacket(clientInfo) { PlayerInformation = playerInformation };

                VitaruNetworkingClientHandler.SendToHost(packet);
                VitaruNetworkingClientHandler.SendToInGameClients(packet);
            }
        }

        private void packetReceived(Packet p)
        {
            if (p is VitaruInMatchPacket packet && Puppet)
            {
                VitaruNetworkingClientHandler.ShareWithOtherPeers(packet);

                if (packet.PlayerInformation.PlayerID == PlayerID)
                {
                    Position = new Vector2(packet.PlayerInformation.PlayerX, packet.PlayerInformation.PlayerY);
                    Cursor.Position = new Vector2(packet.PlayerInformation.CursorX, packet.PlayerInformation.CursorY);

                    if (packet.PlayerInformation.PressedAction != VitaruAction.None)
                        Pressed(packet.PlayerInformation.PressedAction);
                    if (packet.PlayerInformation.ReleasedAction != VitaruAction.None)
                        Released(packet.PlayerInformation.ReleasedAction);
                }
            }
        }
        */
        #endregion
    }
}

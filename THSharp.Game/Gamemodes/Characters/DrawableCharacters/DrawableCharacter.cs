using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Symcol.Core.GameObjects;
using Symcol.Core.Graphics.Containers;
using Symcol.Core.Graphics.Sprites;
using THSharp.Game.Gamemodes.Playfield;
using THSharp.Game.Graphics;

namespace THSharp.Game.Gamemodes.Characters.DrawableCharacters
{
    public abstract class DrawableCharacter<C> : SymcolContainer
        where C : Character
    {
        #region Fields
        public readonly C Character;

        protected SymcolContainer RealityContainer { get; set; }
        protected SymcolSprite RealityStillSprite { get; set; }
        protected SymcolSprite RealityRightSprite { get; set; }
        protected SymcolSprite RealityLeftSprite { get; set; }

        protected SymcolContainer SoulContainer { get; set; }
        protected SymcolSprite StillSprite { get; set; }
        protected SymcolSprite RightSprite { get; set; }
        protected SymcolSprite LeftSprite { get; set; }

        public abstract double MaxHealth { get; }

        public double Health { get; private set; }

        protected abstract string CharacterName { get; }

        public virtual Color4 PrimaryColor { get; } = Color4.Green;

        public virtual Color4 SecondaryColor { get; } = Color4.LightBlue;

        public virtual Color4 ComplementaryColor { get; } = Color4.LightGreen;

        protected virtual float HitboxWidth { get; } = 4;

        public bool Dead { get; protected set; }

        protected readonly GamePlayfield THSharpPlayfield;

        public int Abstraction { get; set; }

        public int Team { get; set; }
        protected CircularContainer VisibleHitbox;
        public SymcolHitbox Hitbox;
        protected float LastX;
        #endregion

        protected DrawableCharacter(C c, GamePlayfield playfield)
        {
            Character = c;
            THSharpPlayfield = playfield;
        }

        /// <summary>
        /// Does animations to better give the illusion of movement (could likely be cleaned up)
        /// </summary>
        protected virtual void MovementAnimations()
        {
            if (LeftSprite.Texture == null && RightSprite != null)
            {
                LeftSprite.Texture = RightSprite.Texture;
                LeftSprite.Size = new Vector2(-RightSprite.Size.X, RightSprite.Size.Y);
            }
            if (RealityLeftSprite.Texture == null && RealityRightSprite != null)
            {
                RealityLeftSprite.Texture = RealityRightSprite.Texture;
                RealityLeftSprite.Size = new Vector2(-RealityRightSprite.Size.X, RealityRightSprite.Size.Y);
            }
            if (Position.X > LastX)
            {
                if (LeftSprite?.Texture != null)
                    LeftSprite.Alpha = 0;
                if (RightSprite?.Texture != null)
                    RightSprite.Alpha = 1;
                if (StillSprite?.Texture != null)
                    StillSprite.Alpha = 0;
                if (RealityLeftSprite?.Texture != null)
                    RealityLeftSprite.Alpha = 0;
                if (RealityRightSprite?.Texture != null)
                    RealityRightSprite.Alpha = 1;
                if (RealityStillSprite?.Texture != null)
                    RealityStillSprite.Alpha = 0;
            }
            else if (Position.X < LastX)
            {
                if (LeftSprite?.Texture != null)
                    LeftSprite.Alpha = 1;
                if (RightSprite?.Texture != null)
                    RightSprite.Alpha = 0;
                if (StillSprite?.Texture != null)
                    StillSprite.Alpha = 0;
                if (RealityLeftSprite?.Texture != null)
                    RealityLeftSprite.Alpha = 1;
                if (RealityRightSprite?.Texture != null)
                    RealityRightSprite.Alpha = 0;
                if (RealityStillSprite?.Texture != null)
                    RealityStillSprite.Alpha = 0;
            }
            else
            {
                if (LeftSprite?.Texture != null)
                    LeftSprite.Alpha = 0;
                if (RightSprite?.Texture != null)
                    RightSprite.Alpha = 0;
                if (StillSprite?.Texture != null)
                    StillSprite.Alpha = 1;
                if (RealityLeftSprite?.Texture != null)
                    RealityLeftSprite.Alpha = 0;
                if (RealityRightSprite?.Texture != null)
                    RealityRightSprite.Alpha = 0;
                if (RealityStillSprite?.Texture != null)
                    RealityStillSprite.Alpha = 1;
            }
            LastX = Position.X;
        }

        protected override void Update()
        {
            base.Update();

            if (Health <= 0 && !Dead)
                Death();

            // ReSharper disable once UnusedVariable
            foreach (Drawable draw in THSharpPlayfield)
            {
                /*
                DrawableBullet bullet = draw as DrawableBullet;
                if (bullet?.Hitbox != null)
                {
                    ParseBullet(bullet);
                    if (Hitbox.HitDetect(Hitbox, bullet.Hitbox))
                    {
                        Hurt(bullet.Bullet.BulletDamage);
                        bullet.Bullet.BulletDamage = 0;
                        bullet.Hit = true;
                    }
                }

                DrawableSeekingBullet seekingBullet = draw as DrawableSeekingBullet;
                if (seekingBullet?.Hitbox != null)
                {
                    if (Hitbox.HitDetect(Hitbox, seekingBullet.Hitbox))
                    {
                        Hurt(seekingBullet.SeekingBullet.BulletDamage);
                        seekingBullet.SeekingBullet.BulletDamage = 0;
                        seekingBullet.Hit = true;
                    }
                }

                DrawableLaser laser = draw as DrawableLaser;
                if (laser?.Hitbox != null)
                {
                    if (Hitbox.HitDetect(Hitbox, laser.Hitbox))
                    {
                    Hurt(laser.Laser.LaserDamage * (1000 / (float)Clock.ElapsedFrameTime));
                        laser.Hit = true;
                    }
                }
                */
            }

            MovementAnimations();
        }

        /// <summary>
        /// Gets called just before hit detection
        /// </summary>
        //protected virtual void ParseBullet(DrawableBullet bullet) { }

        protected virtual void LoadAnimationSprites(THSharpSkinElement textures)
        {
            StillSprite.Texture = textures.GetSkinTextureElement(CharacterName);
            RealityStillSprite.Texture = textures.GetSkinTextureElement(CharacterName + "Kiai");
        }

        /// <summary>
        /// Child loading for all Characters (Enemies, Player, Bosses)
        /// </summary>
        [BackgroundDependencyLoader]
        private void load(THSharpSkinElement textures)
        {
            Health = MaxHealth;

            Anchor = Anchor.TopLeft;
            Origin = Anchor.Centre;

            //TODO: Temp?
            Size = new Vector2(64);

            AddRange(new Drawable[]
            {
                SoulContainer = new SymcolContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = PrimaryColor,
                    Alpha = 1,
                    Children = new Drawable[]
                    {
                        StillSprite = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 1,
                        },
                        RightSprite = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                        },
                        LeftSprite = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                        },
                    }
                },
                RealityContainer = new SymcolContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 0,
                    Children = new Drawable[]
                    {
                        RealityStillSprite = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 1,
                        },
                        RealityRightSprite = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                        },
                        RealityLeftSprite = new SymcolSprite
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                        },
                    }
                },
                VisibleHitbox = new CircularContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 0,
                    Size = new Vector2(HitboxWidth + HitboxWidth / 4),
                    BorderColour = PrimaryColor,
                    BorderThickness = HitboxWidth / 4,
                    Masking = true,

                    Child = new Box
                    {
                        RelativeSizeAxes = Axes.Both
                    },
                    EdgeEffect = new EdgeEffectParameters
                    {

                        Radius = HitboxWidth / 2,
                        Type = EdgeEffectType.Shadow,
                        Colour = PrimaryColor.Opacity(0.5f)
                    }
                }
            });

            Add(Hitbox = new SymcolHitbox(new Vector2(HitboxWidth)) { Team = Team });

            if (CharacterName == "player" || CharacterName == "enemy")
                RealityContainer.Colour = PrimaryColor;

            LoadAnimationSprites(textures);
        }

        /// <summary>
        /// Removes "damage"
        /// </summary>
        /// <param name="damage"></param>
        public virtual double Hurt(double damage)
        {
            Health -= damage;

            if (Health < 0)
            {
                Health = 0;
                Death();
            }

            return Health;
        }

        /// <summary>
        /// Adds "health"
        /// </summary>
        /// <param name="health"></param>
        public virtual double Heal(double health)
        {
            if (Health <= 0 && health > 0)
                Revive();

            Health += health;

            if (Health > MaxHealth)
                Health = MaxHealth;

            return Health;
        }

        protected virtual void Death()
        {
            Dead = true;
            Delete();
        }

        protected virtual void Revive()
        {
            Dead = false;
        }
    }
}


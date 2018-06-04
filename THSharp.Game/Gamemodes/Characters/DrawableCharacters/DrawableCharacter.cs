using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Symcol.Core.GameObjects;
using Symcol.Core.Graphics.Containers;
using Symcol.Core.Graphics.Sprites;
using THSharp.Game.Gamemodes.Playfield;
using THSharp.Game.Gamemodes.Projectiles;
using THSharp.Game.Gamemodes.Projectiles.DrawableProjectiles;
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

        public double Health { get; private set; }

        public bool Dead { get; protected set; }

        protected readonly GamePlayfield THSharpPlayfield;

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

            MovementAnimations();

            foreach (Drawable draw in THSharpPlayfield)
            {
                DrawableProjectile<Projectile> drawableProjectile = draw as DrawableProjectile<Projectile>;
                if (drawableProjectile?.Hitbox != null)
                {
                    ParseProjectiles(drawableProjectile);
                    if (Hitbox.HitDetect(Hitbox, drawableProjectile.Hitbox))
                    {
                        Hurt(drawableProjectile.Projectile.Damage);
                        drawableProjectile.Projectile.Damage = 0;
                        drawableProjectile.Delete();
                    }
                }
            }            
        }

        /// <summary>
        /// Gets called just before hit detection
        /// </summary>
        protected virtual void ParseProjectiles(DrawableProjectile<Projectile> drawableProjectile) { }

        protected virtual void LoadAnimationSprites(THSharpSkinElement textures)
        {
            StillSprite.Texture = textures.GetSkinTextureElement(Character.Name);
            RealityStillSprite.Texture = textures.GetSkinTextureElement(Character.Name + "Kiai");
        }

        /// <summary>
        /// Child loading for all Characters (Enemies, Player, Bosses)
        /// </summary>
        [BackgroundDependencyLoader]
        private void load(THSharpSkinElement textures)
        {
            Health = Character.MaxHealth;

            Anchor = Anchor.TopLeft;
            Origin = Anchor.Centre;

            //TODO: Temp?
            Size = new Vector2(64);

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

            if (Health > Character.MaxHealth)
                Health = Character.MaxHealth;

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


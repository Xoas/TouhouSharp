﻿using OpenTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using Symcol.Core.GameObjects;
using Symcol.Core.Graphics.Containers;
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

        public double Health { get; private set; }

        public bool Dead { get; protected set; }

        protected readonly GamemodePlayfield GamemodePlayfield;

        public SymcolHitbox Hitbox;
        #endregion

        protected DrawableCharacter(C c, GamemodePlayfield playfield)
        {
            Character = c;
            GamemodePlayfield = playfield;

            Size = c.Size;
            Add(Hitbox = new SymcolHitbox(new Vector2((float)c.HitboxWidth)));
        }

        /// <summary>
        /// Child loading for all Characters (Enemies, Player, Bosses)
        /// </summary>
        [BackgroundDependencyLoader]
        private void load(THSharpSkinElement textures)
        {
            Health = Character.MaxHealth;
            LoadAnimationSprites(textures);
        }

        /// <summary>
        /// Can be used to load custom character sprites
        /// </summary>
        /// <param name="textures"></param>
        protected virtual void LoadAnimationSprites(THSharpSkinElement textures) { }

        protected override void Update()
        {
            base.Update();

            MovementAnimations();

            foreach (Drawable draw in GamemodePlayfield)
            {
                if (draw is DrawableProjectile<Projectile> drawableProjectile && drawableProjectile.Hitbox != null)
                    ParseProjectiles(drawableProjectile);
            }            
        }

        /// <summary>
        /// Does animations to better give the illusion of movement (could likely be cleaned up)
        /// </summary>
        protected virtual void MovementAnimations() { }

        /// <summary>
        /// Called once for every bullet per frame
        /// </summary>
        protected virtual void ParseProjectiles(DrawableProjectile<Projectile> drawableProjectile)
        {
            if (Hitbox.HitDetect(Hitbox, drawableProjectile.Hitbox))
            {
                Hurt(drawableProjectile.Projectile.Damage);
                drawableProjectile.Delete();
            }
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
        }

        protected virtual void Revive()
        {
            Dead = false;
        }
    }
}


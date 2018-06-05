using Symcol.Core.GameObjects;
using Symcol.Core.Graphics.Containers;

namespace THSharp.Game.Gamemodes.Projectiles.DrawableProjectiles
{
    public abstract class DrawableProjectile : SymcolContainer
    {
        // ReSharper disable once NotAccessedField.Local
        public readonly Projectile Projectile;

        public readonly SymcolHitbox Hitbox;

        protected DrawableProjectile(Projectile p)
        {
            Projectile = p;
            Add(Hitbox = new SymcolHitbox(p.Size, p.Shape)
            {
                Team = p.Team
            });
        }
    }
}

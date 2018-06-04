using Symcol.Core.GameObjects;
using Symcol.Core.Graphics.Containers;

namespace THSharp.Game.Gamemodes.Projectiles.DrawableProjectiles
{
    public abstract class DrawableProjectile<P> : SymcolContainer
        where P : Projectile
    {
        public P Projectile { get; }

        public readonly SymcolHitbox Hitbox;

        protected DrawableProjectile(P p)
        {
            Projectile = p;
            Add(Hitbox = new SymcolHitbox(p.Size, p.Shape)
            {
                Team = p.Team
            });
        }
    }
}

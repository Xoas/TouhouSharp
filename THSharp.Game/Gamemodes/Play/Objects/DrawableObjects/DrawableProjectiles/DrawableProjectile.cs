using Symcol.Core.GameObjects;
using THSharp.Game.Gamemodes.Play.Objects.Projectiles;

namespace THSharp.Game.Gamemodes.Play.Objects.DrawableObjects.DrawableProjectiles
{
    public abstract class DrawableProjectile : DrawableTHSharpObject
    {
        // ReSharper disable once NotAccessedField.Local
        public readonly Projectile Projectile;

        public readonly SymcolHitbox Hitbox;

        protected DrawableProjectile(Projectile p) : base(p)
        {
            Projectile = p;
            Add(Hitbox = new SymcolHitbox(p.Size, p.Shape)
            {
                Team = p.Team
            });
        }
    }
}

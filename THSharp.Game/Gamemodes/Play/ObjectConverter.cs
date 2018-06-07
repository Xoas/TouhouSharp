using System.Collections.Generic;
using THSharp.Game.Gamemodes.Play.Objects.Projectiles;

namespace THSharp.Game.Gamemodes.Play
{
    public abstract class ObjectConverter
    {
        public abstract IEnumerable<Projectile> GetConvertedProjectile(Projectile projectile);
    }
}

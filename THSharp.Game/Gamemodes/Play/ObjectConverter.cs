using System.Collections.Generic;
using THSharp.Game.Gamemodes.Projectiles;

namespace THSharp.Game.Gamemodes.Play
{
    public abstract class ObjectConverter
    {
        public abstract IEnumerable<Projectile> GetConvertedProjectile(Projectile projectile);
    }
}

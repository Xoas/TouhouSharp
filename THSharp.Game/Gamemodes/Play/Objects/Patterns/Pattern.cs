using System.Collections.Generic;
using THSharp.Game.Gamemodes.Play.Objects.Projectiles;

namespace THSharp.Game.Gamemodes.Play.Objects.Patterns
{
    //TODO: Patterns are un-intuitive as shit!
    public abstract class Pattern
    {
        public abstract string Name { get; }

        public abstract List<Projectile> GetPattern();
    }
}

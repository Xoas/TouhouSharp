using System.Collections.Generic;

namespace THSharp.Game.Gamemodes.Projectiles.Patterns
{
    //TODO: Patterns are un-intuitive as shit!
    public abstract class Pattern
    {
        public abstract string Name { get; }

        public abstract List<Projectile> GetPattern();
    }
}

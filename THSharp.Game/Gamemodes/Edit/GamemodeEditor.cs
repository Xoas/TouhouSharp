using THSharp.Game.Gamemodes.Characters;
using THSharp.Game.Gamemodes.Projectiles;

namespace THSharp.Game.Gamemodes.Edit
{
    public abstract class GamemodeEditor
    {
        public abstract Projectile[] Projectiles { get; }

        public abstract Character[] Enemies { get; }
    }
}

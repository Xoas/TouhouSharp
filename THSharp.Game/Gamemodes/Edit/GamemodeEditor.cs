using THSharp.Game.Gamemodes.Characters;
using THSharp.Game.Gamemodes.Projectiles;
using THSharp.Game.Gamemodes.Projectiles.Patterns;
using THSharp.Game.Screens.Editor.Pieces;

namespace THSharp.Game.Gamemodes.Edit
{
    public abstract class GamemodeEditor
    {
        public abstract EditorPlayfield GetEditorPlayfield();

        public abstract Character[] Enemies { get; }

        //TODO: include some basic patterns
        public virtual Pattern[] Patterns => null;

        public abstract Projectile[] Projectiles { get; }
    }
}

using THSharp.Game.Gamemodes.Edit.Editables;
using THSharp.Game.Gamemodes.Play.Objects.Patterns;
using THSharp.Game.Gamemodes.Play.Objects.Projectiles;
using THSharp.Game.Screens.Editor.Pieces;

namespace THSharp.Game.Gamemodes.Edit
{
    public abstract class GamemodeEditor
    {
        public abstract EditorPlayfield GetEditorPlayfield();

        public abstract EditableCharacter[] Enemies { get; }

        //TODO: include some basic patterns
        public virtual Pattern[] Patterns => null;

        public abstract Projectile[] Projectiles { get; }
    }
}

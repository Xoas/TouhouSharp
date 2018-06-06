using THSharp.Game.Gamemodes.Characters;
using THSharp.Game.Gamemodes.Projectiles;
using THSharp.Game.Screens.Editor.Pieces;

namespace THSharp.Game.Gamemodes.Edit
{
    public abstract class GamemodeEditor
    {
        public abstract EditorPlayfield GetEditorPlayfield();

        public abstract Projectile[] Projectiles { get; }

        public abstract Character[] Enemies { get; }
    }
}

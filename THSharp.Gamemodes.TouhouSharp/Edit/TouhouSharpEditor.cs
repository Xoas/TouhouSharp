using THSharp.Game.Gamemodes.Edit;
using THSharp.Game.Gamemodes.Edit.Editables;
using THSharp.Game.Screens.Editor.Pieces;
using THSharp.Gamemodes.TouhouSharp.Edit.Editables;

namespace THSharp.Gamemodes.TouhouSharp.Edit
{
    public class TouhouSharpEditor : GamemodeEditor
    {
        public override EditorPlayfield GetEditorPlayfield() => new TouhouSharpEditorPlayfield(); 

        public override EditableProjectile[] EditableProjectiles => new EditableProjectile[]
        {
            new EditableBullet(),
        };

        public override EditableCharacter[] EditableEnemies => new EditableCharacter[]
        {
            new EditableEnemy(),
        };
    }
}

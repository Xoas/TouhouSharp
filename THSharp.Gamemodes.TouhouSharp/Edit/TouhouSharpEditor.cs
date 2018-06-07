using THSharp.Game.Gamemodes.Edit;
using THSharp.Game.Gamemodes.Edit.Editables;
using THSharp.Game.Gamemodes.Play.Objects.Projectiles;
using THSharp.Game.Screens.Editor.Pieces;

namespace THSharp.Gamemodes.TouhouSharp.Edit
{
    public class TouhouSharpEditor : GamemodeEditor
    {
        public override EditorPlayfield GetEditorPlayfield() => new TouhouSharpEditorPlayfield(); 

        public override Projectile[] Projectiles => new Projectile[]
        {
            //new Bullet(),
            //new Laser(),
            //new Pawn(),
        };

        public override EditableCharacter[] Enemies => new EditableCharacter[]
        {
            //new TouhouSharpEnemy(),
            //new TouhouSharpBoss(),
        };
    }
}

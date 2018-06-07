using THSharp.Game.Gamemodes.Play;
using THSharp.Game.Screens.Editor.Pieces;
using THSharp.Gamemodes.TouhouSharp.Play;

namespace THSharp.Gamemodes.TouhouSharp.Edit
{
    public class TouhouSharpEditorPlayfield : EditorPlayfield
    {
        protected override Playfield GetPlayfield() => new TouhouSharpPlayfield();
    }
}

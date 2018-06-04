using THSharp.Game.Gamemodes.Characters.DrawableCharacters;
using THSharp.Game.Gamemodes.Playfield;

namespace THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters
{
    public class DrawableTouhouSharpPlayer : DrawablePlayer<TouhouSharpPlayer>
    {
        public DrawableTouhouSharpPlayer(TouhouSharpPlayer p, GamePlayfield playfield)
            : base(p, playfield)
        {
        }
    }
}

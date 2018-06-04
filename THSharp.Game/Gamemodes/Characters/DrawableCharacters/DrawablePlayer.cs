using THSharp.Game.Gamemodes.Playfield;

namespace THSharp.Game.Gamemodes.Characters.DrawableCharacters
{
    public abstract class DrawablePlayer<P> : DrawableCharacter<P>
        where P : Player
    {
        protected DrawablePlayer(P p, GamePlayfield playfield) : base(p, playfield)
        {

        }
    }
}

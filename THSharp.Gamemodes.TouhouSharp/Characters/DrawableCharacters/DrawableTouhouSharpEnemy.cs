using THSharp.Gamemodes.TouhouSharp.Play;

namespace THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters
{
    public class DrawableTouhouSharpEnemy : DrawableTouhouSharpCharacter
    {
        public readonly TouhouSharpEnemy Enemy;

        public DrawableTouhouSharpEnemy(TouhouSharpEnemy e, TouhouSharpPlayfield playfield)
            : base(e, playfield)
        {
            Enemy = e;
        }
    }
}

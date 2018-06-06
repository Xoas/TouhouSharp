using THSharp.Gamemodes.TouhouSharp.Play;

namespace THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters
{
    public class DrawableTouhouSharpEnemy : DrawableTouhouSharpCharacter
    {
        public new readonly TouhouSharpEnemy Character;

        public DrawableTouhouSharpEnemy(TouhouSharpEnemy e, TouhouSharpPlayfield playfield)
            : base(e, playfield)
        {
            Character = e;
        }
    }
}

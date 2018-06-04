using Symcol.Core.Graphics.Containers;
using touhou.sharp.Game.Gameplay.Characters.TouhosuPlayers.DrawableTouhosuPlayers;

namespace touhou.sharp.Game.Gameplay.Playfield
{
    public class GamePlayfield : SymcolContainer
    {
        public readonly AbstractionField GameField;

        public GamePlayfield()
        {
            Child = GameField = new AbstractionField();

            GameField.Current.Add(new DrawableSakuya(this));
        }
    }
}

using osu.Framework.Graphics;
using OpenTK;
using Symcol.Core.Graphics.Containers;
using touhou.sharp.Game.Gameplay.Characters.TouhosuPlayers.DrawableTouhosuPlayers;

namespace touhou.sharp.Game.Gameplay.Playfield
{
    public class GamePlayfield : SymcolContainer
    {
        public readonly AbstractionField GameField;

        public GamePlayfield()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Name = "GamePlayfield";

            Child = GameField = new AbstractionField
            {
                Size = new Vector2(512, 820)
            };

            GameField.Current.Add(new DrawableSakuya(this));
        }

        protected override void Update()
        {
            base.Update();

            GameField.Scale = new Vector2(DrawSize.Y * 10f / 16f / GameField.Size.X, DrawSize.Y / GameField.Size.Y) * 0.9f;
        }
    }
}

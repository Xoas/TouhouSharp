using osu.Framework.Graphics;
using OpenTK;
using THSharp.Game.Gamemodes.Playfield;
using THSharp.Gamemodes.TouhouSharp.Characters;
using THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters;

namespace THSharp.Gamemodes.TouhouSharp.Playfield
{
    public class TouhouSharpPlayfield : GamePlayfield
    {
        public readonly AbstractionField GameField;

        public TouhouSharpPlayfield()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Name = "GamePlayfield";

            Child = GameField = new AbstractionField
            {
                Size = new Vector2(512, 820)
            };

            GameField.Current.Add(new DrawableTouhouSharpPlayer(new TouhouSharpPlayer(), this));
        }

        protected override void Update()
        {
            base.Update();

            GameField.Scale = new Vector2(DrawSize.Y * 10f / 16f / GameField.Size.X, DrawSize.Y / GameField.Size.Y) * 0.8f;
        }
    }
}
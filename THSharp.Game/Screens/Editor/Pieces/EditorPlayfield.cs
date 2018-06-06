using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;
using THSharp.Game.Gamemodes.Play;

namespace THSharp.Game.Screens.Editor.Pieces
{
    public abstract class EditorPlayfield : SymcolContainer
    {
        public abstract Playfield GetPlayfield();

        protected readonly Playfield Playfield;

        protected EditorPlayfield()
        {
            Children = new Drawable[]
            {
                Playfield = GetPlayfield()
            };
            Playfield.Add(new BorderContainer());
        }

        protected class BorderContainer : SymcolContainer
        {
            public BorderContainer()
            {
                Name = "Border";

                RelativeSizeAxes = Axes.Both;
                Masking = true;

                BorderColour = Color4.White;
                BorderThickness = 3;

                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                    AlwaysPresent = true
                };
            }
        }
    }
}

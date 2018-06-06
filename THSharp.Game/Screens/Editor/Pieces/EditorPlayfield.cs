using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK;
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
            RelativeSizeAxes = Axes.Both;

            Child = new DeadContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = Playfield = GetPlayfield()
            };
            Playfield.Add(new BorderContainer());
        }

        //TODO: Hacky as fuck, even for me
        protected class DeadContainer : SymcolContainer
        {
            public override bool HandleMouseInput => false;
            public override bool HandleKeyboardInput => false;
        }

        //TODO: make this autosize itself to be larger than playfield slightly
        protected class BorderContainer : SymcolContainer
        {
            public BorderContainer()
            {
                Name = "Border";

                RelativeSizeAxes = Axes.Both;
                Masking = true;

                BorderColour = Color4.White;
                BorderThickness = 8;
                Scale = new Vector2(1.04f);

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

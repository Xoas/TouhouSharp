using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;
using THSharp.Game.Graphics.UI.Toolbar.Pieces;

namespace THSharp.Game.Graphics.UI.Toolbar
{
    public class Toolbar : SymcolContainer
    {
        protected readonly FillFlowContainer<ToolbarPiece> RightSection;
        protected readonly FillFlowContainer<ToolbarPiece> LeftSection;

        public Toolbar()
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;

            RelativeSizeAxes = Axes.X;
            Height = 50;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black,
                    Alpha = 0.8f
                },
                new FillFlowContainer
                {

                },

            };
        }
    }
}

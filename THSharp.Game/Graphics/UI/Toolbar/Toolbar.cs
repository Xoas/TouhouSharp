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
        private readonly FillFlowContainer<ToolbarPiece> rightSection;
        private readonly FillFlowContainer<ToolbarPiece> leftSection;

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
                rightSection = new FillFlowContainer<ToolbarPiece>
                {
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.4f,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Direction = FillDirection.Horizontal
                },
                leftSection = new FillFlowContainer<ToolbarPiece>
                {
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.4f,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Direction = FillDirection.Horizontal
                },
                new ToolbarGamemodeSelector()
            };
        }
    }
}

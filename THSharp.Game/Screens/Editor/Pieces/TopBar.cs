using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;

namespace THSharp.Game.Screens.Editor.Pieces
{
    public class TopBar : SymcolContainer
    {
        public TopBar()
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;

            RelativeSizeAxes = Axes.Both;

            Height = 0.1f;

            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black,
                Alpha = 0.8f
            };
        }
    }
}

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;

namespace THSharp.Game.Screens.Editor.Pieces.Bars
{
    public class RightBar : SideBar
    {
        public RightBar()
        {
            Anchor = Anchor.CentreRight;
            Origin = Anchor.CentreRight;

            RelativeSizeAxes = Axes.Both;

            Width = 0.2f;
            Height = 0.6f;

            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black,
                Alpha = 0.6f
            };
        }
    }
}

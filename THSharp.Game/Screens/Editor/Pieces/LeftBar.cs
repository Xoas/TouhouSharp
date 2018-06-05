using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;
using THSharp.Game.Gamemodes;

namespace THSharp.Game.Screens.Editor.Pieces
{
    public class LeftBar : SymcolContainer
    {
        public LeftBar(Gamemode g)
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;

            RelativeSizeAxes = Axes.Both;

            Width = 0.2f;
            Height = 0.6f;

            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black,
                Alpha = 0.8f
            };
        }
    }
}

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;
using THSharp.Game.Gamemodes;

namespace THSharp.Game.Screens.Editor.Pieces
{
    public class BottomBar : SymcolContainer
    {
        public BottomBar(Gamemode g)
        {
            Anchor = Anchor.BottomCentre;
            Origin = Anchor.BottomCentre;

            RelativeSizeAxes = Axes.Both;

            Height = 0.18f;

            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black,
                Alpha = 0.8f
            };
        }
    }
}

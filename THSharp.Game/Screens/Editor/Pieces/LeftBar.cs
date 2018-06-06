using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;
using THSharp.Game.Gamemodes.Edit;
using THSharp.Game.Graphics.UI;

namespace THSharp.Game.Screens.Editor.Pieces
{
    public class LeftBar : SymcolContainer
    {
        public LeftBar(GamemodeEditor e)
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;

            RelativeSizeAxes = Axes.Both;

            Width = 0.2f;
            Height = 0.6f;

            THSharpTabControl<BarTabs> tabControl;
            FillFlowContainer<SelectionItem> content = new FillFlowContainer<SelectionItem>();

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black,
                    Alpha = 0.8f
                },
                tabControl = new THSharpTabControl<BarTabs>
                {

                },
                content
            };

            tabControl.Current.ValueChanged += value =>
            {
                switch (value)
                {
                    case BarTabs.Projectiles:
                        break;
                    case BarTabs.Enemies:
                        break;
                }
            };
            tabControl.Current.TriggerChange();
        }

        private class SelectionItem : SymcolClickableContainer
        {
            public SelectionItem()
            {

            }
        }

        private enum BarTabs
        {
            Projectiles,
            Enemies
        }
    }
}

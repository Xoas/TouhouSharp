using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using OpenTK;
using OpenTK.Graphics;
using THSharp.Game.Gamemodes.Characters;
using THSharp.Game.Gamemodes.Edit;
using THSharp.Game.Gamemodes.Projectiles;
using THSharp.Game.Graphics.UI;

namespace THSharp.Game.Screens.Editor.Pieces.Bars
{
    public class LeftBar : SideBar
    {
        public LeftBar(GamemodeEditor e)
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;

            RelativeSizeAxes = Axes.Both;

            Width = 0.2f;
            Height = 0.6f;

            THSharpTabControl<BarTabs> tabControl;
            FillFlowContainer<SelectionItem> content;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black,
                    Alpha = 0.6f
                },
                //TODO: Make this thing pretty
                tabControl = new THSharpTabControl<BarTabs>
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.Both,
                    Height = 0.04f
                },
                content = new FillFlowContainer<SelectionItem>
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.98f,
                    Height = 0.96f,
                    Spacing = new Vector2(0, 4)
                }
            };

            tabControl.Current.ValueChanged += value =>
            {
                switch (value)
                {
                    case BarTabs.Projectiles:
                        content.Children = new SelectionItem[] { };
                        foreach (Projectile p in e.Projectiles)
                            content.Add(new SelectionItem(p.Name, () =>
                            {
                                foreach (SelectionItem i in content)
                                {
                                    i.FadeTo(0.5f, 200, Easing.OutCubic);
                                    i.Selected = false;
                                }
                            }));
                        break;
                    case BarTabs.Enemies:
                        content.Children = new SelectionItem[] { };
                        foreach (Character c in e.Enemies)
                            content.Add(new SelectionItem(c.Name, () =>
                            {
                                foreach (SelectionItem i in content)
                                {
                                    i.FadeTo(0.5f, 200, Easing.OutCubic);
                                    i.Selected = false;
                                }
                            }));
                        break;
                }
            };
            tabControl.Current.TriggerChange();
        }

        private enum BarTabs
        {
            Projectiles,
            Enemies
        }
    }
}

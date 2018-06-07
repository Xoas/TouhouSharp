using osu.Framework.Graphics;
using THSharp.Game.Gamemodes.Edit;
using THSharp.Game.Gamemodes.Edit.Editables;
using THSharp.Game.Gamemodes.Play.Objects.Patterns;

namespace THSharp.Game.Screens.Editor.Pieces.Bars
{
    public class RightBar : SideBar<RightBarTabs>
    {
        public RightBar(GamemodeEditor e) : base(Anchor.CentreRight)
        {
            TabControl.Current.ValueChanged += value =>
            {
                switch (value)
                {
                    case RightBarTabs.Patterns:
                        ItemList.Children = new SelectionItem[] { };
                        if (e.Patterns != null)
                            foreach (Pattern p in e.Patterns)
                                ItemList.Add(new SelectionItem(p.Name, () =>
                                {
                                    foreach (SelectionItem i in ItemList)
                                    {
                                        i.FadeTo(0.5f, 200, Easing.OutCubic);
                                        i.Selected = false;
                                    }
                                }));
                        break;
                    case RightBarTabs.Projectiles:
                        ItemList.Children = new SelectionItem[] { };
                        foreach (EditableProjectile p in e.EditableProjectiles)
                            ItemList.Add(new SelectionItem(p.GetNewObject().Name, () =>
                            {
                                foreach (SelectionItem i in ItemList)
                                {
                                    i.FadeTo(0.5f, 200, Easing.OutCubic);
                                    i.Selected = false;
                                }
                            }));
                        break;
                }
            };
            TabControl.Current.TriggerChange();
        }
    }

    public enum RightBarTabs
    {
        Patterns,
        Projectiles
    }
}

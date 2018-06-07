using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;
using OpenTK;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;
using THSharp.Game.Graphics.UI;

namespace THSharp.Game.Screens.Editor.Pieces.Bars
{
    public abstract class SideBar<T> : SymcolContainer
        where T : struct
    {
        protected readonly THSharpTabControl<T> TabControl;
        protected readonly FillFlowContainer<SelectionItem> ItemList;

        protected SideBar(Anchor anchor)
        {
            Anchor = anchor;
            Origin = anchor;

            RelativeSizeAxes = Axes.Both;

            Width = 0.2f;
            Height = 0.6f;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black,
                    Alpha = 0.6f
                },
                //TODO: Make this thing pretty
                TabControl = new THSharpTabControl<T>
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.Both,
                    Height = 0.04f
                },
                ItemList = new FillFlowContainer<SelectionItem>
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.98f,
                    Height = 0.96f,
                    Spacing = new Vector2(0, 4)
                }
            };
        }

        protected class SelectionItem : SymcolClickableContainer
        {
            public bool Selected;

            private readonly Box box;

            public SelectionItem(string text, Action action = null)
            {
                Action = action;

                Action += () =>
                {
                    Selected = !Selected;
                    if (Selected)
                        this.FadeTo(1, 200, Easing.OutCubic);
                    else
                        this.FadeTo(0.5f, 200, Easing.OutCubic);
                };

                RelativeSizeAxes = Axes.X;
                Height = 40;
                Alpha = 0.5f;

                Masking = true;
                CornerRadius = 8;

                Children = new Drawable[]
                {
                    box = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Black,
                        Alpha = 0.6f
                    },
                    new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        TextSize = 32,
                        Text = text
                    }
                };

                Action?.Invoke();
            }

            protected override bool OnHover(InputState state)
            {
                box.FadeTo(0.2f, 200, Easing.OutCubic);
                return base.OnHover(state);
            }

            protected override void OnHoverLost(InputState state)
            {
                box.FadeTo(0.6f, 200, Easing.OutCubic);
                base.OnHoverLost(state);
            }
        }
    }
}

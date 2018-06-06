using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;

namespace THSharp.Game.Screens.Editor.Pieces.Bars
{
    public abstract class SideBar : SymcolContainer
    {
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

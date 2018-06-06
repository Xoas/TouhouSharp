using System;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using Symcol.Core.Extentions;

namespace THSharp.Game.Graphics.UI
{
    public class THSharpTabControl<T> : TabControl<T>
        where T : struct
    {
        protected override Dropdown<T> CreateDropdown() => new THSharpTabDropdown();

        protected override TabItem<T> CreateTabItem(T value) => new THSharpTabItem(value);

        private static bool isEnumType => typeof(T).IsEnum;

        public THSharpTabControl()
        {
            if (isEnumType)
                foreach (var val in (T[])Enum.GetValues(typeof(T)))
                    AddItem(val);
        }

        public class THSharpTabItem : TabItem<T>
        {
            protected readonly SpriteText Text;

            private const float transition_length = 200;

            private void fadeActive()
            {
                Text.FadeTo(0.8f, transition_length, Easing.OutQuint);
            }

            private void fadeInactive()
            {
                Text.FadeTo(0.2f, transition_length, Easing.OutQuint);
            }

            protected override bool OnHover(InputState state)
            {
                if (!Active)
                    fadeActive();
                return true;
            }

            protected override void OnHoverLost(InputState state)
            {
                if (!Active)
                    fadeInactive();
            }

            [BackgroundDependencyLoader]
            private void load()
            {
            }

            public THSharpTabItem(T value) : base(value)
            {
                AutoSizeAxes = Axes.X;
                RelativeSizeAxes = Axes.Y;

                Children = new Drawable[]
                {
                    Text = new SpriteText
                    {
                        Margin = new MarginPadding { Top = 5, Bottom = 5 },
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                        Text = (value as IHasDescription)?.Description ?? (value as Enum)?.GetDescription() ?? value.ToString(),
                        TextSize = 20
                    }
                };
            }

            protected override void OnActivated() => fadeActive();

            protected override void OnDeactivated() => fadeInactive();
        }

        private class THSharpTabDropdown : Dropdown<T>
        {
            protected override DropdownHeader CreateHeader() => new THSharpDropdownHeader();

            private class THSharpDropdownHeader : DropdownHeader
            {
                protected override string Label { get; set; }

                public THSharpDropdownHeader()
                {
                    RelativeSizeAxes = Axes.None;
                    AutoSizeAxes = Axes.X;
                }
            }
        }
    }
}

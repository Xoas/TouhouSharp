using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using OpenTK;
using Symcol.Core.Graphics.Containers;
using Symcol.Core.Graphics.Sprites;
using THSharp.Game.Config;
using THSharp.Game.Gamemodes;

namespace THSharp.Game.Graphics.UI.Toolbar.Pieces
{
    public class ToolbarGamemodeSelector : ToolbarPiece
    {
        [BackgroundDependencyLoader]
        private void load(THSharpConfigManager config)
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Width = 0.2f;

            FillFlowContainer<GamemodeIcon> gamemodeContainer;

            Children = new Drawable[]
            {
                gamemodeContainer = new FillFlowContainer<GamemodeIcon>
                {
                    RelativeSizeAxes = Axes.Both,
                }
            };

            foreach (Gamemode gamemode in GamemodeStore.LoadedGamemodes)
            {
                GamemodeIcon icon = new GamemodeIcon(gamemode);
                icon.Action = () =>
                {
                    config.Set(THSharpSetting.Gamemode, gamemode.Name);

                    foreach (GamemodeIcon i in gamemodeContainer)
                            i.FadeTo(0.2f, 100, Easing.InOutBack);

                    icon.FadeTo(0.8f, 100, Easing.InOutBack);
                };

                gamemodeContainer.Add(icon);
            }

            GamemodeStore.OnGamemodeRemoved += gamemode =>
            {
                foreach (GamemodeIcon icon in gamemodeContainer)
                    if (icon.Gamemode.Name == gamemode.Name)
                    {
                        gamemodeContainer.Remove(icon);

                        if (icon.Gamemode.Name == config.Get<string>(THSharpSetting.Gamemode))
                            gamemodeContainer.FirstOrDefault()?.Action?.Invoke();

                        break;
                    }
            };

            GamemodeStore.OnGamemodeAdd += gamemode =>
            {
                GamemodeIcon icon = new GamemodeIcon(gamemode);
                icon.Action = () =>
                {
                    config.Set(THSharpSetting.Gamemode, gamemode.Name);

                    foreach (GamemodeIcon i in gamemodeContainer)
                        i.FadeTo(0.2f, 100, Easing.InOutBack);

                    icon.FadeTo(0.8f, 100, Easing.InOutBack);
                };

                gamemodeContainer.Add(icon);
            };

            foreach (GamemodeIcon icon in gamemodeContainer)
                if (icon.Gamemode.Name == config.Get<string>(THSharpSetting.Gamemode))
                    icon.Action?.Invoke();
        }

        public class GamemodeIcon : SymcolClickableContainer
        {
            public readonly Gamemode Gamemode;

            public GamemodeIcon(Gamemode gamemode)
            {
                Gamemode = gamemode;

                RelativeSizeAxes = Axes.Y;

                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;

                Scale = new Vector2(0.8f);

                Child = new SymcolSprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Texture = gamemode.Icon
                };
            }

            protected override bool OnHover(InputState state)
            {
                this.ScaleTo(Vector2.One, 200, Easing.OutCubic);

                return base.OnHover(state);
            }

            protected override void OnHoverLost(InputState state)
            {
                base.OnHoverLost(state);

                this.ScaleTo(new Vector2(0.8f), 200, Easing.OutCubic);
            }

            protected override void Update()
            {
                base.Update();

                Width = DrawHeight;
            }
        }
    }
}

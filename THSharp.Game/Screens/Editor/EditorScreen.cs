using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using OpenTK.Graphics;
using OpenTK.Input;
using THSharp.Game.Config;
using THSharp.Game.Gamemodes;
using THSharp.Game.Screens.Editor.Pieces;

namespace THSharp.Game.Screens.Editor
{
    public class EditorScreen : THSharpScreen
    {
        public override bool ShowToolBar => false;

        [BackgroundDependencyLoader]
        private void load(THSharpConfigManager config)
        {
            Gamemode g = GamemodeStore.GetSelectedGamemode(config.Get<string>(THSharpSetting.Gamemode));

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Blue
                },
                new TopBar(g),
                new LeftBar(g),
                new RightBar(g),
                new BottomBar(g),
            };
        }

        //TODO: Confirm exit for saving changes
        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if (args.Repeat || !IsCurrentScreen) return false;

            switch (args.Key)
            {
                case Key.Escape:
                    Exit();
                    return true;
            }

            return base.OnKeyDown(state, args);
        }
    }
}

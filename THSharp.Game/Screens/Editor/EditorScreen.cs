using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using OpenTK.Graphics;
using OpenTK.Input;
using Symcol.Core.Graphics.Containers;
using Symcol.Core.Graphics.UserInterface;
using THSharp.Game.Config;
using THSharp.Game.Gamemodes;
using THSharp.Game.Gamemodes.Edit;
using THSharp.Game.Screens.Editor.Pieces.Bars;

namespace THSharp.Game.Screens.Editor
{
    public class EditorScreen : THSharpScreen
    {
        protected override bool ShowToolBar => false;

#pragma warning disable 169
        private readonly SymcolWindow confirmExit;
#pragma warning restore 169

        [BackgroundDependencyLoader]
        private void load(THSharpConfigManager config)
        {
            GamemodeEditor editor = GamemodeStore.GetGamemode(config.Get<string>(THSharpSetting.Gamemode)).GetEditor();

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Blue
                },
                new TopBar(),
                new LeftBar(editor),
                new RightBar(),
                new BottomBar(),
                new SymcolContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,

                    RelativeSizeAxes = Axes.Both,
                    Height = 0.6f,
                    Width = 0.5f,

                    Child = editor.GetEditorPlayfield()
                }
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

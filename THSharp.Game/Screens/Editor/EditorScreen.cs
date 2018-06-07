using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using OpenTK.Graphics;
using OpenTK.Input;
using Symcol.Core.Graphics.Containers;
using THSharp.Game.Config;
using THSharp.Game.Gamemodes;
using THSharp.Game.Gamemodes.Edit;
using THSharp.Game.Screens.Editor.Pieces;
using THSharp.Game.Screens.Editor.Pieces.Bars;

namespace THSharp.Game.Screens.Editor
{
    public class EditorScreen : THSharpScreen
    {
        protected override bool ShowToolBar => false;

        private readonly EditableClock clock = new EditableClock();

        //private readonly SymcolWindow confirmExit;

        //private readonly SymcolWindow patternEditor;

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
                new RightBar(editor),
                new BottomBar(clock),
                new SymcolContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,

                    RelativeSizeAxes = Axes.Both,
                    Height = 0.6f,
                    Width = 0.5f,

                    Child = editor.GetEditorPlayfield(),
                    Clock = clock
                }
            };
        }

        //TODO: Confirm exit for unsaved changes
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

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using OpenTK.Graphics;
using OpenTK.Input;
using THSharp.Game.Screens.Editor.Pieces;

namespace THSharp.Game.Screens.Editor
{
    public class EditorScreen : THSharpScreen
    {
        public override bool ShowToolBar => false;

        public EditorScreen()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Blue
                },
                new TopBar(),
                new LeftBar(),
                new RightBar(),
                new BottomBar(), 
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

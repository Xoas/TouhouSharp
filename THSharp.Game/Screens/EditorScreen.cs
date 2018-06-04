using osu.Framework.Input;
using OpenTK.Input;

namespace THSharp.Game.Screens
{
    public class EditorScreen : THSharpScreen
    {
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

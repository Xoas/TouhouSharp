using osu.Framework.Graphics.Cursor;
using osu.Framework.Screens;

namespace THSharp.Game.Screens
{
    public class THSharpScreen : Screen
    {
        /// <summary>
        /// Whether the toolbar is visible by default in this screen
        /// </summary>
        protected virtual bool ShowToolBar => true;

        /// <summary>
        /// Currently this does nothing but in the future this will apply whenever this screen is current
        /// </summary>
        protected virtual CursorContainer OverrideCursor => null;

        protected override void OnEntering(Screen last)
        {
            base.OnEntering(last);
            if (ShowToolBar)
                THSharpGame.Toolbar.Show();
            else
                THSharpGame.Toolbar.Hide();
        }

        protected override void OnResuming(Screen last)
        {
            base.OnResuming(last);
            if (ShowToolBar)
                THSharpGame.Toolbar.Show();
            else
                THSharpGame.Toolbar.Hide();
        }
    }
}

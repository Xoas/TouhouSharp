using osu.Framework.Screens;

namespace THSharp.Game.Screens
{
    public class THSharpScreen : Screen
    {
        /// <summary>
        /// Currently this does nothing but in the future this will apply whenever this screen is current
        /// </summary>
        public virtual bool ShowToolBar => true;

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

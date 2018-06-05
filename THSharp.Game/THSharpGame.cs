using osu.Framework.Input;
using OpenTK.Input;
using THSharp.Game.Gamemodes;
using THSharp.Game.Graphics.UI;
using THSharp.Game.Screens;

namespace THSharp.Game
{
    public class THSharpGame : THSharpBaseGame
    {
        public static THSharpCursor THSharpCursor;

        protected override void LoadComplete()
        {
            base.LoadComplete();

            var homeScreen = new HomeScreen();

            Add(THSharpCursor = new THSharpCursor());
            Add(homeScreen);

            homeScreen.Exited += _ => Scheduler.AddDelayed(Exit, 500);

            GamemodeStore.ReloadGamemodes(THSharpSkinElement, Host.Storage);
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if (!args.Repeat && args.Key == Key.F5)
                GamemodeStore.ReloadGamemodes(THSharpSkinElement, Host.Storage);

            return base.OnKeyDown(state, args);
        }
    }
}

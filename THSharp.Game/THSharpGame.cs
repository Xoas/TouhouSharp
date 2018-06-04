using osu.Framework.Input;
using OpenTK.Input;
using THSharp.Game.Gamemodes;
using THSharp.Game.Screens;

namespace THSharp.Game
{
    public class THSharpGame : THSharpBaseGame
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();

            var homeScreen = new HomeScreen();

            Add(homeScreen);

            homeScreen.Exited += _ => Scheduler.AddDelayed(Exit, 500);

            GamemodeStore.ReloadGamemodes();
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if (!args.Repeat && args.Key == Key.F5)
                GamemodeStore.ReloadGamemodes();

            return base.OnKeyDown(state, args);
        }
    }
}

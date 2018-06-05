using osu.Framework.Input;
using OpenTK.Input;
using THSharp.Game.Gamemodes;
using THSharp.Game.Graphics.UI;
using THSharp.Game.Graphics.UI.Toolbar;
using THSharp.Game.Screens;

namespace THSharp.Game
{
    public class THSharpGame : THSharpBaseGame
    {
        public static THSharpCursor THSharpCursor;

        protected override void LoadComplete()
        {
            base.LoadComplete();

            GamemodeStore.ReloadGamemodes(THSharpSkinElement, Host.Storage);

            HomeScreen homeScreen = new HomeScreen();

            Add(homeScreen);
            Add(new Toolbar());
            Add(THSharpCursor = new THSharpCursor());

            homeScreen.Exited += _ => Scheduler.AddDelayed(Exit, 500);
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if (!args.Repeat && args.Key == Key.F5)
                GamemodeStore.ReloadGamemodes(THSharpSkinElement, Host.Storage);

            return base.OnKeyDown(state, args);
        }
    }
}

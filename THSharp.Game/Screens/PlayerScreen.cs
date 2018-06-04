using System.Linq;
using THSharp.Game.Gamemodes;

namespace THSharp.Game.Screens
{
    public class PlayerScreen : THSharpMenuScreen
    {
        public PlayerScreen()
        {
            if (GamemodeStore.LoadedGamemodes.FirstOrDefault() != null)
                Child = GamemodeStore.LoadedGamemodes.FirstOrDefault()?.GetGamemodePlayfield();
        }
    }
}

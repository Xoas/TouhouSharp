using System.Linq;
using THSharp.Game.Gamemodes;

namespace THSharp.Game.Screens
{
    public class PlayerScreen : THSharpMenuScreen
    {
        public PlayerScreen()
        {
            Child = GamemodeStore.LoadedGamemodes.FirstOrDefault().GetPlayfield();
        }
    }
}

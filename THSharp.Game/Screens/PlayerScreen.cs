using THSharp.Game.Gameplay.Playfield;

namespace THSharp.Game.Screens
{
    public class PlayerScreen : THSharpMenuScreen
    {
        public PlayerScreen()
        {
            Child = new GamePlayfield();
        }
    }
}

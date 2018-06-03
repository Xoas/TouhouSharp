using touhou.sharp.Game.Gameplay.Playfield;

namespace touhou.sharp.Game.Screens
{
    public class PlayerScreen : THSharpMenuScreen
    {
        public PlayerScreen()
        {
            Child = new GamePlayfield();
        }
    }
}

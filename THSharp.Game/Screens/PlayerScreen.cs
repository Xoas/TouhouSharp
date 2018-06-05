using osu.Framework.Allocation;
using THSharp.Game.Config;
using THSharp.Game.Gamemodes;

namespace THSharp.Game.Screens
{
    public class PlayerScreen : THSharpMenuScreen
    {
        [BackgroundDependencyLoader]
        private void load(THSharpConfigManager config)
        {
            Child = GamemodeStore.GetWorkingGamemode(config.Get<string>(THSharpSetting.Gamemode))?.GetGamemodePlayfield();
        }
    }
}

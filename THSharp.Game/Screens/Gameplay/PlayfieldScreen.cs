using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using THSharp.Game.Config;
using THSharp.Game.Gamemodes;

namespace THSharp.Game.Screens.Gameplay
{
    public class PlayfieldScreen : THSharpMenuScreen
    {
        public override bool ShowToolBar => false;

        [BackgroundDependencyLoader]
        private void load(THSharpConfigManager config)
        {
            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Blue
            };
            Add(GamemodeStore.GetWorkingGamemode(config.Get<string>(THSharpSetting.Gamemode))?.GetGamemodePlayfield());
        }
    }
}

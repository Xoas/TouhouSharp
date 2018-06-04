using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace touhou.sharp.Game.Config
{
    public class THSharpConfigManager : IniConfigManager<THSharpSetting>
    {
        protected override string Filename => @"THSharp.ini";

        public THSharpConfigManager(Storage storage) : base(storage)
        {
        }

        protected override void InitialiseDefaults()
        {
            Set(THSharpSetting.Gamemode, Gamemodes.TouhouSharp);

            Set(THSharpSetting.Graphics, Graphics.TouhouSharp);

            Set(THSharpSetting.Skin, "Default");

            Set(THSharpSetting.SavedName, "User");
            Set(THSharpSetting.SavedUserID, -1);
            Set(THSharpSetting.PlayerColor, "#ffffff");

            Set(THSharpSetting.HostIP, "Host's IP Address");
            Set(THSharpSetting.LocalIP, "Local IP Address");

            Set(THSharpSetting.HostPort, 25570);
            Set(THSharpSetting.LocalPort, 25570);
        }
    }

    public enum THSharpSetting
    {
        Gamemode,

        Graphics,

        Skin,

        SavedName,
        SavedUserID,
        PlayerColor,

        HostIP,
        LocalIP,

        HostPort,
        LocalPort
    }

    //TODO: Move this / delete this?
    public enum Gamemodes
    {
        TouhouSharp,
        TouhouClassic
    }

    //TODO: Move this?
    public enum Graphics
    {
        TouhouSharp,
        Vitaru,
    }
}
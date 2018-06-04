using THSharp.Game.Gamemodes;
using THSharp.Game.Gamemodes.Playfield;
using THSharp.Gamemodes.TouhouSharp.Playfield;

namespace THSharp.Gamemodes.TouhouSharp
{
    public class TouhouSharpLookup : Gamemode
    {
        public override GamemodePlayfield GetGamemodePlayfield() => new TouhouSharpPlayfield();

        public override string Name => "TouhouSharp";

        public override int? OfficialID => 0;
    }
}

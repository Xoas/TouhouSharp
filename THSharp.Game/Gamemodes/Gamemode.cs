using THSharp.Game.Gamemodes.Playfield;

namespace THSharp.Game.Gamemodes
{
    public abstract class Gamemode
    {
        public abstract GamePlayfield GetPlayfield();

        public virtual string Name => "";

        public virtual int? OfficialID => null;
    }
}

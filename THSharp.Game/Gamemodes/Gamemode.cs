using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using THSharp.Game.Gamemodes.Playfield;
using THSharp.Game.Graphics;

namespace THSharp.Game.Gamemodes
{
    public abstract class Gamemode
    {
        public abstract GamemodePlayfield GetGamemodePlayfield();

        public virtual string Name => "";

        public virtual Texture Icon => null;

        public virtual int? OfficialID => null;

        public virtual void LoadDependencies(THSharpSkinElement textures, Storage storage) { }
    }
}

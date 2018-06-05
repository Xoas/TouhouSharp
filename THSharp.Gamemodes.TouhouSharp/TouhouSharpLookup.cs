using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using THSharp.Game.Gamemodes;
using THSharp.Game.Gamemodes.Playfield;
using THSharp.Game.Graphics;
using THSharp.Gamemodes.TouhouSharp.Playfield;

namespace THSharp.Gamemodes.TouhouSharp
{
    public class TouhouSharpLookup : Gamemode
    {
        public override GamemodePlayfield GetGamemodePlayfield() => new TouhouSharpPlayfield();

        public override string Name => "TouhouSharp";

        private Texture icon;

        public override Texture Icon => icon;

        public override int? OfficialID => 0;

        public override void LoadDependencies(THSharpSkinElement textures, Storage storage)
        {
            base.LoadDependencies(textures, storage);

            icon = textures.GetSkinTextureElement("icon", true);
        }
    }
}

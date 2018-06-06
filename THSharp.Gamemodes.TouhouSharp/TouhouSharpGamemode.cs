using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using THSharp.Game.Gamemodes;
using THSharp.Game.Gamemodes.Edit;
using THSharp.Game.Gamemodes.Play;
using THSharp.Game.Graphics;
using THSharp.Gamemodes.TouhouSharp.Edit;
using THSharp.Gamemodes.TouhouSharp.Play;

namespace THSharp.Gamemodes.TouhouSharp
{
    public class TouhouSharpGamemode : Gamemode
    {
        public override Playfield GetPlayfield() => new TouhouSharpPlayfield();

        public override ObjectConverter ObjectConverter => null;

        //public override DrawableCharacter GetDrawablePlayer() => null;

        public override GamemodeEditor GetEditor() => new TouhouSharpEditor();

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

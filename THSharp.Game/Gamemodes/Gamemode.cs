using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using THSharp.Game.Gamemodes.Edit;
using THSharp.Game.Gamemodes.Play;
using THSharp.Game.Graphics;

namespace THSharp.Game.Gamemodes
{
    public abstract class Gamemode
    {
        public abstract Playfield GetPlayfield();

        // ReSharper disable once InconsistentNaming
        public virtual UI UI => new UI();

        public abstract ObjectConverter ObjectConverter { get; }

        //public abstract DrawableCharacter GetDrawablePlayer();

        public virtual GamemodeEditor GetEditor() => null;

        public virtual string Name => "";

        public virtual Texture Icon => null;

        public virtual int? OfficialID => null;

        public virtual void LoadDependencies(THSharpSkinElement textures, Storage storage) { }
    }
}

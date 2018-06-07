using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;
using THSharp.Game.Gamemodes.Edit.Editables;
using THSharp.Game.Gamemodes.Play.Objects.Characters;
using THSharp.Game.Gamemodes.Play.Objects.DrawableObjects.DrawableCharacters;
using THSharp.Game.Graphics;
using THSharp.Gamemodes.TouhouSharp.Play.Characters;
using THSharp.Gamemodes.TouhouSharp.Play.Characters.DrawableCharacters;

namespace THSharp.Gamemodes.TouhouSharp.Edit.Editables
{
    public class EditableEnemy : EditableCharacter
    {
        private Sprite sprite = new Sprite
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            Colour = Color4.Red
        };

        public override SymcolContainer GetOverlayContainer() => new SymcolContainer
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            Child = sprite
        };

        public override Character GetNewObject() => new TouhouSharpEnemy();
        public override DrawableCharacter GetNewDrawableObject(Character o) => new DrawableTouhouSharpEnemy((TouhouSharpEnemy)o, null);

        [BackgroundDependencyLoader]
        private void load(THSharpSkinElement textures)
        {
            sprite.Texture = textures.GetSkinTextureElement("Enemy");
        }
    }
}

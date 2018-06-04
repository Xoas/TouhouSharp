using osu.Framework.Graphics;
using Symcol.Core.Graphics.Sprites;
using THSharp.Game.Graphics;
using THSharp.Gamemodes.TouhouSharp.Playfield;

namespace THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters
{
    public class DrawableSakuya : DrawableTouhouSharpPlayer<Sakuya>
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public double SetRate { get; private set; }

        public DrawableSakuya(TouhouSharpPlayfield playfield)
            : base(new Sakuya(), playfield)
        {
        }

        protected override void LoadAnimationSprites(THSharpSkinElement textures)
        {
            base.LoadAnimationSprites(textures);

            SoulContainer.Alpha = 0;
            RealityContainer.Alpha = 1;

            RealitySpriteLeft.Alpha = 0;
            RealitySpriteRight.Alpha = 0;
            RealitySpriteStill.Alpha = 0;

            RealityContainer.AddRange(new Drawable[]
            {
                    new AnimatedSprite
                    {
                        RelativeSizeAxes = Axes.Both,
                        UpdateRate = 100,
                        Textures = new[]
                        {
                            textures.GetSkinTextureElement(Character.Name + " Kiai 0"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai 1"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai 2"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai 3"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai 4"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai 5"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai 6"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai 7"),
                        }
                    },
                    new AnimatedSprite
                    {
                        Alpha = 0,
                        RelativeSizeAxes = Axes.Both,
                        UpdateRate = 100,
                        Textures = new[]
                        {
                            textures.GetSkinTextureElement(Character.Name + " Kiai Left 0"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Left 1"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Left 2"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Left 3"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Left 4"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Left 5"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Left 6"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Left 7"),
                        }
                    },
                    new AnimatedSprite
                    {
                        Alpha = 0,
                        RelativeSizeAxes = Axes.Both,
                        UpdateRate = 100,
                        Textures = new[]
                        {
                            textures.GetSkinTextureElement(Character.Name + " Kiai Right 0"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Right 1"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Right 2"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Right 3"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Right 4"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Right 5"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Right 6"),
                            textures.GetSkinTextureElement(Character.Name + " Kiai Right 7"),
                        }
                    }
            });
        }
    }
}

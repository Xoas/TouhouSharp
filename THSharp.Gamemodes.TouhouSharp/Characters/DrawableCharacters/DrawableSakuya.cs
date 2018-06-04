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

        protected AnimatedSprite Idle;
        protected AnimatedSprite Left;
        protected AnimatedSprite Right;

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
                //TODO: rename "Kiai" files to "Reality"
                Idle = new AnimatedSprite
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
                Left = new AnimatedSprite
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
                Right = new AnimatedSprite
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

        protected override void MovementAnimations()
        {
            base.MovementAnimations();

            if (Position.X > LastX && Right.Alpha < 1)
            {
                Idle.Alpha = 0;
                Left.Alpha = 0;
                Right.Alpha = 1;
                Right.Reset();
            }
            else if (Position.X < LastX && Left.Alpha < 1)
            {
                Idle.Alpha = 0;
                Left.Alpha = 1;
                Right.Alpha = 0;
                Left.Reset();
            }
            else if (Position.X == LastX && Idle.Alpha < 1)
            {
                Idle.Alpha = 1;
                Left.Alpha = 0;
                Right.Alpha = 0;
                Idle.Reset();
            }

            LastX = Position.X;
        }
    }
}

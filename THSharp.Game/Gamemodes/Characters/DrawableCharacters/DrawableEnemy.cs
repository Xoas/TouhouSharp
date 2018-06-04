using OpenTK;
using THSharp.Game.Gamemodes.Playfield;
using THSharp.Game.Graphics;

namespace THSharp.Game.Gamemodes.Characters.DrawableCharacters
{
    public abstract class DrawableEnemy<E> : DrawableCharacter<E>
        where E : Enemy
    {
        protected DrawableEnemy(E e, GamePlayfield playfield) : base(e, playfield)
        {
            AlwaysPresent = true;
        }

        protected override void MovementAnimations()
        {
            if (LeftSprite.Texture == null && RightSprite != null)
            {
                LeftSprite.Texture = RightSprite.Texture;
                LeftSprite.Size = new Vector2(-RightSprite.Size.X, RightSprite.Size.Y);
            }
            if (RealityLeftSprite.Texture == null && RealityRightSprite != null)
            {
                RealityLeftSprite.Texture = RealityRightSprite.Texture;
                RealityLeftSprite.Size = new Vector2(-RealityRightSprite.Size.X, RealityRightSprite.Size.Y);
            }
            if (Position.X > LastX)
            {
                if (LeftSprite.Texture != null)
                    LeftSprite.Alpha = 0;
                if (RightSprite?.Texture != null)
                    RightSprite.Alpha = 1;
                if (StillSprite.Texture != null)
                    StillSprite.Alpha = 0;
                if (RealityLeftSprite.Texture != null)
                    RealityLeftSprite.Alpha = 0;
                if (RealityRightSprite?.Texture != null)
                    RealityRightSprite.Alpha = 1;
                if (RealityStillSprite.Texture != null)
                    RealityStillSprite.Alpha = 0;
            }
            else if (Position.X < LastX)
            {
                if (LeftSprite.Texture != null)
                    LeftSprite.Alpha = 1;
                if (RightSprite?.Texture != null)
                    RightSprite.Alpha = 0;
                if (StillSprite.Texture != null)
                    StillSprite.Alpha = 0;
                if (RealityLeftSprite.Texture != null)
                    RealityLeftSprite.Alpha = 1;
                if (RealityRightSprite?.Texture != null)
                    RealityRightSprite.Alpha = 0;
                if (RealityStillSprite.Texture != null)
                    RealityStillSprite.Alpha = 0;
            }
            LastX = Position.X;
        }

        protected override void LoadAnimationSprites(THSharpSkinElement textures)
        {
            base.LoadAnimationSprites(textures);
            RightSprite.Texture = textures.GetSkinTextureElement(Character.Name);
            RealityRightSprite.Texture = textures.GetSkinTextureElement(Character.Name + "Kiai");
        }

        protected override void Death()
        {
            Dead = true;
            Hitbox.HitDetection = false;
        }
    }
}

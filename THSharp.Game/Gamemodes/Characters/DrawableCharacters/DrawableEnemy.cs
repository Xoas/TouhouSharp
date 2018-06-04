using OpenTK;
using OpenTK.Graphics;
using THSharp.Game.Gamemodes.Playfield;
using THSharp.Game.Graphics;

namespace THSharp.Game.Gamemodes.Characters.DrawableCharacters
{
    public abstract class DrawableEnemy<E> : DrawableCharacter<E>
        where E : Enemy
    {
        public override double MaxHealth => 60;

        protected override string CharacterName => "enemy";

        public override Color4 PrimaryColor => characterColor;

        protected override float HitboxWidth => 48;

#pragma warning disable 649
        private Color4 characterColor;
#pragma warning restore 649

        protected DrawableEnemy(E e, GamePlayfield playfield) : base(e, playfield)
        {
            AlwaysPresent = true;

            Team = 1;
            //characterColor = drawablePattern.AccentColour;
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
            RightSprite.Texture = textures.GetSkinTextureElement(CharacterName);
            RealityRightSprite.Texture = textures.GetSkinTextureElement(CharacterName + "Kiai");
        }

        protected override void Death()
        {
            Dead = true;
            Hitbox.HitDetection = false;
        }
    }
}

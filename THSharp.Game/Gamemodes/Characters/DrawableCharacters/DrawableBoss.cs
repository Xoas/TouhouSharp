using OpenTK;
using THSharp.Game.Gamemodes.Playfield;
using THSharp.Game.Graphics;

namespace THSharp.Game.Gamemodes.Characters.DrawableCharacters
{
    public abstract class DrawableBoss<B> : DrawableCharacter<B>
        where B : Boss
    {
        public override double MaxHealth => 20000;

        protected override string CharacterName => "Kokoro Hatano";

        protected override float HitboxWidth => 64;

        protected DrawableBoss(B b, GamePlayfield playfield) : base(b, playfield)
        {
            // ReSharper disable once PossibleLossOfFraction
            Position = new Vector2(256, 384 / 2);
            AlwaysPresent = true;
            Abstraction = 3;
            Alpha = 0;
            Team = 1;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Hitbox.HitDetection = false;
        }

        protected override void MovementAnimations()
        {
            //base.MovementAnimations();
        }

        protected override void LoadAnimationSprites(THSharpSkinElement textures)
        {
            SoulContainer.Alpha = 0;
            RealityContainer.Alpha = 1;

            RealityLeftSprite.Alpha = 0;
            RealityRightSprite.Alpha = 0;
            RealityStillSprite.Alpha = 1;

            RealityStillSprite.Texture = textures.GetSkinTextureElement(CharacterName + " Kiai");

            Size = new Vector2(128);
        }

        protected override void Death()
        {
            //base.Death();
            Hitbox.HitDetection = false;
        }
    }
}

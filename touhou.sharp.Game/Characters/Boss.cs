﻿using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using OpenTK;
using touhou.sharp.Game.Gameplay;
using touhou.sharp.Game.Graphics;

namespace touhou.sharp.Game.Characters
{
    public class Boss : Character
    {
        public bool Free = true;

        public override double MaxHealth => 20000;

        protected override string CharacterName => "Kokoro Hatano";

        protected override float HitboxWidth => 64;

        public Boss(THSharpPlayfield playfield) : base(playfield)
        {
            Position = new Vector2(256 , 384 / 2);
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

            if (Seal.Alpha > 0)
                Seal.RotateTo((float)((Clock.CurrentTime / 1000) * 90));
        }

        protected override void LoadAnimationSprites(THSharpSkinElement textures, Storage storage)
        {
            SoulContainer.Alpha = 0;
            KiaiContainer.Alpha = 1;

            KiaiLeftSprite.Alpha = 0;
            KiaiRightSprite.Alpha = 0;
            KiaiStillSprite.Alpha = 1;

            //KiaiStillSprite.Texture = THSharpSkinElement.LoadSkinElement(CharacterName + " Kiai", storage);

            Size = new Vector2(128);
        }

        protected override void Death()
        {
            //base.Death();
            Hitbox.HitDetection = false;
        }
    }
}

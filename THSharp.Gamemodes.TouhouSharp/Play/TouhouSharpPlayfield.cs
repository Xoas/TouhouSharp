using System;
using osu.Framework.MathUtils;
using OpenTK;
using OpenTK.Graphics;
using THSharp.Game.Gamemodes.Play;
using THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters.DrawablePlayers;
using THSharp.Gamemodes.TouhouSharp.Projectiles;
using THSharp.Gamemodes.TouhouSharp.Projectiles.DrawableProjectiles;

namespace THSharp.Gamemodes.TouhouSharp.Play
{
    public class TouhouSharpPlayfield : Playfield
    {
        public readonly AbstractionField GameField;

        public TouhouSharpPlayfield()
        {
            Add(GameField = new AbstractionField());

            GameField.Current.Add(new DrawableSakuya(this));
        }

        protected override void Update()
        {
            base.Update();

            if (RNG.Next(1, 1000) < Time.Current / 500)
                GameField.Add(new DrawableBullet(new Bullet
                {
                    StartPosition = new Vector2(RNG.Next(0, 512), 0),
                    Angle = Math.PI / 2,
                    Speed = 0.2f,
                    Diameter = 20,
                    Damage = 10,
                    Color = Color4.Red,
                    Team = 3,
                }));
        }
    }
}
using System;
using osu.Framework.Graphics;
using osu.Framework.MathUtils;
using OpenTK;
using OpenTK.Graphics;
using THSharp.Game.Gamemodes.Playfield;
using THSharp.Gamemodes.TouhouSharp.Characters.DrawableCharacters.DrawablePlayers;
using THSharp.Gamemodes.TouhouSharp.Projectiles;
using THSharp.Gamemodes.TouhouSharp.Projectiles.DrawableProjectiles;

namespace THSharp.Gamemodes.TouhouSharp.Playfield
{
    public class TouhouSharpPlayfield : GamemodePlayfield
    {
        public readonly AbstractionField GameField;

        public TouhouSharpPlayfield()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Name = "TouhouSharpPlayfield";

            Add(GameField = new AbstractionField { Size = new Vector2(512, 820) });

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

            GameField.Scale = new Vector2(DrawSize.Y * 10f / 16f / GameField.Size.X, DrawSize.Y / GameField.Size.Y) * 0.8f;
        }
    }
}
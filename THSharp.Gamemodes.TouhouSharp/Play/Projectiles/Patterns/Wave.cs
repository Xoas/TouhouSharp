using System.Collections.Generic;
using OpenTK;
using THSharp.Game.Gamemodes.Projectiles;
using THSharp.Game.Gamemodes.Projectiles.Patterns;

namespace THSharp.Gamemodes.TouhouSharp.Play.Projectiles.Patterns
{
    public class Wave : Pattern
    {
        public override string Name => "Wave";

        public override List<Projectile> GetPattern()
        {
            throw new System.NotImplementedException();
        }
    }

    public class PlayerWave : Pattern
    {
        public override string Name => "PlayerWave";

        public override List<Projectile> GetPattern()
        {
            List<Projectile> pattern = new List<Projectile>();

            const int numberbullets = 3;
            double directionModifier = -0.2d;

            // ReSharper disable once ConvertToConstant.Local
            double cursorAngle = 0;

            for (int i = 1; i <= numberbullets; i++)
            {
                //Color4 color;
                double size;
                double damage;
                if (i % 2 == 0)
                {
                    size = 24;
                    damage = 24;
                    //color = Character.PrimaryColor;
                }
                else
                {
                    size = 18;
                    damage = 18;
                    //color = Character.SecondaryColor;
                }

                //-90 = up
                pattern.Add(bulletAddRad(1, MathHelper.DegreesToRadians(-90 + cursorAngle) + directionModifier, size, damage));

                //if (Actions[THSharpAction.Slow])
                //directionModifier += 0.1d;

                directionModifier += 0.2d;
            }

            return pattern;
        }

        private Bullet bulletAddRad(double speed, double angle, double size, double damage)
        {
            return new Bullet
            {
                //StartTime = Time.Current,
                //StartPosition = Position,
                Angle = angle,
                Speed = speed,
                Diameter = size,
                Damage = damage,
                //Color = color,
                //Team = Character.Team,
                //DummyMode = true,
                //SliderType = SliderType.Straight,
                //Abstraction = 3,
            };
        }
    }
}

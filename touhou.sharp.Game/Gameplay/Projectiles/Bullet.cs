using OpenTK;
using Symcol.Core.GameObjects;

namespace touhou.sharp.Game.Gameplay.Projectiles
{
    public class Bullet : Projectile
    {
        //TODO: bullets will come in all shapes and sizes
        public override Shape Shape => Shape.Circle;

        public double Diameter { get; set; }

        public override Vector2 Size => new Vector2((float)Diameter);

        public double Speed { get; set; } = 0.25d;
    }
}

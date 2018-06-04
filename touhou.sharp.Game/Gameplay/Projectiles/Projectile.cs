using OpenTK;
using OpenTK.Graphics;
using Symcol.Core.GameObjects;

namespace touhou.sharp.Game.Gameplay.Projectiles
{
    public abstract class Projectile
    {
        public int Team { get; set; }

        public double Damage { get; set; } = 10;

        public Vector2 Size { get; set; } = new Vector2(10);

        public Shape Shape { get; set; }

        public Color4 Color { get; set; }
    }
}

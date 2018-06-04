using System;
using OpenTK;
using OpenTK.Graphics;
using Symcol.Core.GameObjects;

namespace touhou.sharp.Game.Gameplay.Projectiles
{
    public abstract class Projectile
    {
        /// <summary>
        /// The Team this projectile is on and will therefor not harm by default
        /// </summary>
        public int Team { get; set; }

        public double Damage { get; set; } = 10;

        public Vector2 StartPosition { get; set; }

        /// <summary>
        /// Angle of this projectile in Radians
        /// </summary>
        public double Angle { get; set; } = Math.PI / 2;

        public virtual Vector2 Size { get; set; } = new Vector2(10);

        public virtual Shape Shape { get; set; }

        public Color4 Color { get; set; }
    }
}

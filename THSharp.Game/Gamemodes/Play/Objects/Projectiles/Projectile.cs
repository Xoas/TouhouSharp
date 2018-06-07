using System;
using OpenTK;
using OpenTK.Graphics;
using Symcol.Core.GameObjects;

namespace THSharp.Game.Gamemodes.Play.Objects.Projectiles
{
    public abstract class Projectile : THSharpObject
    {
        public override string Name => "Projectile";

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

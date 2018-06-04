using System;
using OpenTK;
using touhou.sharp.Game.Gameplay.Projectiles.DrawableProjectiles.Pieces;

namespace touhou.sharp.Game.Gameplay.Projectiles.DrawableProjectiles
{
    public class DrawableBullet : DrawableProjectile<Bullet>
    {
        //TODO: Make everything in the playfield use one of these
        public Vector4 BulletBounds = new Vector4(-10, -10, 520, 830);

        private readonly Vector2 velocity;

        public DrawableBullet(Bullet b) : base(b)
        {
            Add(new BulletPiece(this));

            Position = b.StartPosition;

            velocity = getBulletVelocity();
        }

        protected override void Update()
        {
            base.Update();

            Position += new Vector2(velocity.X * (float)Clock.ElapsedFrameTime, velocity.Y * (float)Clock.ElapsedFrameTime);

            if (Position.Y < BulletBounds.Y || Position.X < BulletBounds.X || Position.Y > BulletBounds.W || Position.X > BulletBounds.Z)
                Delete();
        }

        private Vector2 getBulletVelocity()
        {
            return new Vector2((float)Projectile.Speed * (float)Math.Cos(Projectile.Angle), (float)Projectile.Speed * (float)Math.Sin(Projectile.Angle));
        }
    }
}

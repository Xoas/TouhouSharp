using System;
using osu.Framework.Graphics;
using OpenTK;
using THSharp.Game.Gamemodes.Projectiles.DrawableProjectiles;
using THSharp.Gamemodes.TouhouSharp.Projectiles.DrawableProjectiles.Pieces;

namespace THSharp.Gamemodes.TouhouSharp.Projectiles.DrawableProjectiles
{
    public class DrawableBullet : DrawableProjectile
    {
        public override bool HandleMouseInput => false;
        public override bool HandleKeyboardInput => false;

        public readonly Bullet Bullet;

        //TODO: Make everything in the playfield use one of these
        public Vector4 BulletBounds = new Vector4(-10, -10, 520, 830);

        private readonly Vector2 velocity;

        public DrawableBullet(Bullet b) : base(b)
        {
            Bullet = b;

            Add(new BulletPiece(this));

            Anchor = Anchor.TopLeft;
            Origin = Anchor.Centre;

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
            return new Vector2((float)Bullet.Speed * (float)Math.Cos(Projectile.Angle), (float)Bullet.Speed * (float)Math.Sin(Projectile.Angle));
        }
    }
}

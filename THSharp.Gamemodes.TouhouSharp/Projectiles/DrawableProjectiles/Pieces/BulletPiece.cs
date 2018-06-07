using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using OpenTK;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;

namespace THSharp.Gamemodes.TouhouSharp.Projectiles.DrawableProjectiles.Pieces
{
    public class BulletPiece : SymcolCircularContainer
    {
        public override bool HandleMouseInput => false;
        public override bool HandleKeyboardInput => false;

        public BulletPiece(DrawableBullet drawableBullet)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Size = drawableBullet.Projectile.Size;

            Masking = true;

            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.White
            };

            EdgeEffect = new EdgeEffectParameters
            {
                Colour = drawableBullet.Projectile.Color.Opacity(0.5f),
                Type = EdgeEffectType.Shadow,
                Radius = drawableBullet.Projectile.Size.X / 2
            };
        }
    }
}

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
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
            BorderThickness = 4;
            BorderColour = Color4.White;

            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = drawableBullet.Projectile.Color
            };
        }
    }
}

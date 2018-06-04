using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK.Graphics;
using Symcol.Core.Graphics.Containers;

namespace THSharp.Gamemodes.TouhouSharp.Projectiles.DrawableProjectiles.Pieces
{
    public class BulletPiece : SymcolCircularContainer
    {
        public BulletPiece(DrawableBullet drawableBullet)
        {
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

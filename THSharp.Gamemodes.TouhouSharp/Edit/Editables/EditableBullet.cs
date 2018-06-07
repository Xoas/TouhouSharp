using Symcol.Core.Graphics.Containers;
using THSharp.Game.Gamemodes.Edit.Editables;
using THSharp.Game.Gamemodes.Play.Objects.DrawableObjects.DrawableProjectiles;
using THSharp.Game.Gamemodes.Play.Objects.Projectiles;
using THSharp.Gamemodes.TouhouSharp.Play.Projectiles;
using THSharp.Gamemodes.TouhouSharp.Play.Projectiles.DrawableProjectiles;

namespace THSharp.Gamemodes.TouhouSharp.Edit.Editables
{
    public class EditableBullet : EditableProjectile
    {
        public override SymcolContainer GetOverlayContainer() => new SymcolContainer();

        public override Projectile GetNewObject() => new Bullet();
        public override DrawableProjectile GetNewDrawableObject(Projectile o) => new DrawableBullet((Bullet)o);
    }
}

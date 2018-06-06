using THSharp.Game.Gamemodes.Characters;
using THSharp.Game.Gamemodes.Edit;
using THSharp.Game.Gamemodes.Projectiles;
using THSharp.Gamemodes.TouhouSharp.Characters;
using THSharp.Gamemodes.TouhouSharp.Projectiles;

namespace THSharp.Gamemodes.TouhouSharp.Edit
{
    public class TouhouSharpEditor : GamemodeEditor
    {
        public override Projectile[] Projectiles => new Projectile[]
        {
            new Bullet(),
            //new Laser(),
            //new Pawn(),
        };

        public override Character[] Enemies => new Character[]
        {
            new TouhouSharpEnemy(),
            new TouhouSharpBoss(),
        };
    }
}

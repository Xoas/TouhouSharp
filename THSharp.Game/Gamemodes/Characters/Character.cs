using OpenTK;
using OpenTK.Graphics;

namespace THSharp.Game.Gamemodes.Characters
{
    public abstract class Character
    {
        public abstract string Name { get; }

        public virtual double MaxHealth => 80;

        public virtual int Team => 0;

        public virtual Vector2 Size => new Vector2(32);

        public virtual double HitboxWidth => 10;

        public virtual Color4 PrimaryColor { get; } = Color4.Green;

        public virtual Color4 SecondaryColor { get; } = Color4.LightBlue;

        public virtual Color4 ComplementaryColor { get; } = Color4.LightGreen;
    }
}
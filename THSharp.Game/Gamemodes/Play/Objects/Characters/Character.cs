using OpenTK;
using OpenTK.Graphics;
using Symcol.Core.GameObjects;

namespace THSharp.Game.Gamemodes.Play.Objects.Characters
{
    public abstract class Character : THSharpObject
    {
        public override string Name => "Character";

        public virtual double MaxHealth => 80;

        public virtual Vector2 Size => new Vector2(32);

        public virtual Shape HitboxShape => Shape.Circle;

        public virtual double HitboxWidth => 10;

        public virtual Color4 PrimaryColor { get; } = Color4.Green;

        public virtual Color4 SecondaryColor { get; } = Color4.LightBlue;

        public virtual Color4 ComplementaryColor { get; } = Color4.LightGreen;
    }
}
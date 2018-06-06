using osu.Framework.Graphics;
using OpenTK;
using Symcol.Core.Graphics.Containers;

namespace THSharp.Game.Gamemodes.Play
{
    public abstract class Playfield : SymcolContainer
    {
        public override Vector2 Size => new Vector2(512, 820);

        public new virtual float Margin => 0.8f;

        public virtual Vector2 AspectRatio => new Vector2(10, 16);

        protected Playfield()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Name = "Playfield";
        }

        protected override void Update()
        {
            base.Update();

            Scale = GetScale();
        }

        protected virtual Vector2 GetScale()
        {
            return new Vector2(Parent.DrawSize.Y * AspectRatio.X / AspectRatio.Y / Size.X, Parent.DrawSize.Y /Size.Y) * Margin;
        }
    }
}

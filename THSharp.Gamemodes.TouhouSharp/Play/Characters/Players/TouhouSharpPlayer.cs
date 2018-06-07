using OpenTK;

namespace THSharp.Gamemodes.TouhouSharp.Play.Characters.Players
{
    public abstract class TouhouSharpPlayer : TouhouSharpCharacter
    {
        public virtual double MaxEnergy => 24;

        public override Vector2 Size => new Vector2(64);

        public override double HitboxWidth => 4;
    }
}

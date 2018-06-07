using Symcol.Core.Graphics.Containers;

namespace THSharp.Game.Gamemodes.Play.Objects.DrawableObjects
{
    public abstract class DrawableTHSharpObject : SymcolContainer
    {
        public readonly THSharpObject THSharpObject;

        protected DrawableTHSharpObject(THSharpObject o)
        {
            THSharpObject = o;
        }
    }
}

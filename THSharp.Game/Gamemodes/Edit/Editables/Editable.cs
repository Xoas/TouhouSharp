using Symcol.Core.Graphics.Containers;
using THSharp.Game.Gamemodes.Play.Objects;
using THSharp.Game.Gamemodes.Play.Objects.DrawableObjects;

namespace THSharp.Game.Gamemodes.Edit.Editables
{
    public abstract class Editable<O, D> : SymcolContainer
        where O : THSharpObject
        where D : DrawableTHSharpObject
    {
        public abstract SymcolContainer GetOverlayContainer();

        public abstract O GetNewObject();

        public readonly O Object;

        public abstract D GetNewDrawableObject(O o);

        public readonly D DrawableObject;

        protected Editable()
        {
            Object = GetNewObject();
            DrawableObject = GetNewDrawableObject(Object);
        }
    }
}

namespace THSharp.Game.Gamemodes.Play.Objects
{
    public abstract class THSharpObject
    {
        public virtual string Name => "Object";

        public virtual int Team { get; set; }
    }
}

namespace THSharp.Game.Gamemodes.Play.Objects
{
    public abstract class THSharpObject
    {
        public virtual string Name => "Object";

        //TODO: do this differently so multiplayer will be good
        public virtual int Team { get; set; } = 0;
    }
}

namespace THSharp.Game.Gamemodes.Play.Objects
{
    public abstract class THSharpObject
    {
        public abstract string Name { get; }

        //TODO: do this differently so multiplayer will be good
        public virtual int Team { get; set; } = 0;
    }
}

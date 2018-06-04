using System.Collections.Generic;
using osu.Framework.Input.Bindings;
using Symcol.Core.NeuralNetworking;

namespace THSharp.Game.NeuralNetworking
{
    public class THSharpInputHandler : NeuralInputContainer<THSharpAction>
    {
        public override TensorFlowBrain<THSharpAction> TensorFlowBrain => new THSharpBrain();

        public override THSharpAction[] GetActiveActions => new[]
        {
            THSharpAction.Up,
            THSharpAction.Down,
            THSharpAction.Left,
            THSharpAction.Right
        };

        public override IEnumerable<KeyBinding> DefaultKeyBindings => new[]
        {
            new KeyBinding(InputKey.W, THSharpAction.Up),
            new KeyBinding(InputKey.S, THSharpAction.Down),
            new KeyBinding(InputKey.A, THSharpAction.Left),
            new KeyBinding(InputKey.D, THSharpAction.Right),
            new KeyBinding(InputKey.MouseLeft, THSharpAction.Shoot),
            new KeyBinding(InputKey.MouseRight, THSharpAction.Spell),
            new KeyBinding(InputKey.E, THSharpAction.Increase),
            new KeyBinding(InputKey.Q, THSharpAction.Decrease),
            new KeyBinding(InputKey.Shift, THSharpAction.Slow),
        };
    }

    public enum THSharpAction
    {
        None = -1,

        //Movement
        Left = 0,
        Right,
        Up,
        Down,

        //Self-explanitory
        Shoot,
        Spell,

        //Slows the player + reveals hitbox
        Slow,

        //Sakuya
        Increase,
        Decrease
    }
}

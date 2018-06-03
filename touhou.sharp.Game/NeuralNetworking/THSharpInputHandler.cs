﻿using Symcol.Core.NeuralNetworking;
using touhou.sharp.Game.NeuralNetworking;

namespace touhou.sharp.Game.Gameplay
{
    public class THSharpInputHandler : NeuralInputContainer<THSharpAction>
    {
        public override TensorFlowBrain<THSharpAction> TensorFlowBrain => new THSharpBrain();

        public override THSharpAction[] GetActiveActions => new THSharpAction[]
        {

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

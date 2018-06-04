using System;
using Symcol.Core.NeuralNetworking;
using TensorFlow;

namespace THSharp.Game.NeuralNetworking
{
    public class THSharpBrain : TensorFlowBrain<THSharpAction>
    {
        public override TFTensor GetTensor(TFSession session, THSharpAction t)
        {
            throw new NotImplementedException();
        }
    }
}

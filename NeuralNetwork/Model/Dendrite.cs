using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetwork2Layers.Model
{
    public class Dendrite
    {
        public Neuron SourceNeuron { get; }
        public double Weight;

        public Dendrite(Neuron sourceNeuron)
        {
            SourceNeuron = sourceNeuron;
            Weight = 0;
        }
    }
}

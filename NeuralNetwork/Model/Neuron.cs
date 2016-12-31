using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetwork2Layers.Model
{
    public class Neuron
    {
        public double AxonValue = 0.5;
        public IEnumerable<Dendrite> Dendrites { get; }
        public Neuron(Layer layerToConnectWith = null)
        {
            List<Dendrite> dendrites = new List<Dendrite>();
            if (layerToConnectWith != null)
                foreach (Neuron neuron in layerToConnectWith.Neurons)
                    dendrites.Add(new Dendrite(neuron));

            Dendrites = dendrites;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetwork2Layers.Model
{
    public class Layer
    {
        public IEnumerable<Neuron> Neurons { get; }
        public Layer(int neuronsNumber, Layer layerToConnectWith = null)
        {
            List<Neuron> neurons = new List<Neuron>();
            for (int i = 0; i < neuronsNumber; i++)
                neurons.Add(new Neuron(layerToConnectWith));

            Neurons = neurons;
        }
    }
}

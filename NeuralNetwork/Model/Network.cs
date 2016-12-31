using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetwork2Layers.Model
{
    public class Network
    {
        private List<Layer> layers = new List<Layer>();
        public Layer LastLayer { get { return layers[1]; } }
        public Network()
        {
            layers.Add(new Layer(225));
            layers.Add(new Layer(3, layers[0]));
        }
        public void Evaluate(List<bool> data)
        {

            FillFirstLayer(ListOfBoolToListOfDouble(data));

            foreach (Neuron neuron in layers[1].Neurons)
            {
                double sum = 0;
                foreach (Dendrite dendrite in neuron.Dendrites)
                    sum += dendrite.SourceNeuron.AxonValue * dendrite.Weight;

                neuron.AxonValue = 1 / (1 + Math.Exp(-sum));
            }
        }

        private Neuron GetActiveNeuron(string arrowName)
        {
            Layer lastLayer = layers[1];
            int index = 0;

            if (arrowName == "Северо-восток")
                index = 1;
            if (arrowName == "Восток")
                index = 2;

            return lastLayer.Neurons.ToList()[index];
        }

        private List<double> ListOfBoolToListOfDouble(List<bool> data)
        {
            List<double> result = new List<double>();
            foreach (bool b in data)
            {
                double value = 1;
                if (!b)
                    value = 0;
                result.Add(value);
            }
            return result;
        }

        private void FillFirstLayer(List<double> data)
        {
            int i = 0;
            foreach (double d in data)
            {
                layers[0].Neurons.ToArray()[i].AxonValue = d;
                i++;
            }
        }

        public void Train(List<bool> data, string arrowName)
        {
            Neuron activeNeuron = GetActiveNeuron(arrowName);
            List<double> doubledData = ListOfBoolToListOfDouble(data);
            Evaluate(data);

            double delta = 0;
            double learningRate = 0.01;
            int i = 0;
            foreach (Dendrite dendrite in activeNeuron.Dendrites)
            {
                delta = doubledData[i] - activeNeuron.AxonValue;
                dendrite.Weight += doubledData[i] * delta * learningRate;
                i++;
            }
        }
    }
}

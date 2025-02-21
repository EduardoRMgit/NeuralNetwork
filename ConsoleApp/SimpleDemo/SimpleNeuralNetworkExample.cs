using NeuralNetwork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.SimpleDemo
{
    internal static class SimpleNeuralNetworkExample
    {
        public static void DoExample()
        {
            INeuralNetwork Network = new SimpleNeuralNetwork();

            // APLICARLE SOFTMAX REY :D
            var Outputs = Network.Predict([1, 2]);
            WriteNeuronsInfo(Network);
        }

        public static void WriteNeuronsInfo(INeuralNetwork neuralNetwork)
        {
            var NeuronsInfo = neuralNetwork.GetNeuronInfos();
            int CurrentLayer = -1;
            int CurrentNeuronIndex = -1;
            foreach(var Neuron in NeuronsInfo)
            {
                if(CurrentLayer != Neuron.LayerIndex)
                {
                    CurrentLayer = Neuron.LayerIndex;
                    Console.WriteLine($"Layer: { CurrentLayer} ");
                    CurrentNeuronIndex = -1;
                }
                CurrentNeuronIndex = Neuron.NeuronIndex;
                Console.WriteLine($"  Neuron Index: {CurrentNeuronIndex}");
                Console.WriteLine($"        Output Value: {Neuron.OutputValue:f4}");
                Console.WriteLine($"        Bias Value: {Neuron.Bias:f4}");

                if(Neuron.Weights.Any())
                {
                    Console.WriteLine($"            Weights: ");
                    for(int i = 0; i < Neuron.Weights.Count(); i++)
                    {
                        Console.WriteLine($"               Synapse:{i}: {Neuron.Weights.ElementAt(i):f2}");
                    }
                }
            }

        }
    }
}

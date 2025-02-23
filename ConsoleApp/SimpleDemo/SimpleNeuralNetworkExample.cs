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

            double[] Outputs = Network.Predict([1, 2]);
            WriteNeuronsInfo(Network);

            WriteFinalValues(SoftMaxActivationFunction(Outputs));

        }

        public static double[] SoftMaxActivationFunction(double[] outputValues)
        {
            double[] FinalValues = new double [outputValues.Count()];

            double WeightedSum = 0;
            foreach(double value in outputValues)
            {
                WeightedSum += Math.Pow(Math.E, value);
            }

            for(int i = 0; i < outputValues.Length; i++)
            {
                FinalValues[i] = Math.Pow(Math.E, outputValues[i]) / WeightedSum;
            }

            return FinalValues;
        }

        public static void WriteFinalValues(double[] outputValues)
        {
            Console.WriteLine("\nOutputValues (ActivationFunction):");
            for(int i = 0; i < outputValues.Length; i++)
            {
                Console.WriteLine($"  Neuron Index: {i}");
                Console.WriteLine($"        Output Value: {outputValues[i]:f4}");
            }
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

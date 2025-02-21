using NeuralNetwork.Abstractions;
using NeuralNetwork.Library.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.Helpers
{
    internal static class NeuralNetworkHelper
    {
        public static IEnumerable<NeuronInfo> GetNeuronsInfo(INeuralNetwork network)
        {
            List<NeuronInfo> Result = [];

            int CurrentLayerIndex = 0;
            AddInfo(network.InputLayer, CurrentLayerIndex++);

            foreach (var layer in network.HiddenLayers)
            {
                AddInfo(layer, CurrentLayerIndex++);
            }

            AddInfo(network.OutputLayer, CurrentLayerIndex);


            return Result;

            void AddInfo(INeuron[] layer, int layerIndex)
            {
                // recibo una capa y el indice  de dicha capa, a cada neurona la voy a agregar

                // recorro cada neurona de la capa, y por cada capa. agrega al resultado un nuevo neuroinfo

                // RECIBE CADA CAPA DE NEURONAS

                // RECORRE CADA NEURONA

                // POR CADA NEURONA AGREGA UN NEURON INFO.
                for (int i = 0; i < layer.Length; i++)
                {
                    Result.Add(new NeuronInfo(layerIndex, i, layer[i].Bias,
                        layer[i].OutputValue,
                        layer[i].Axon.Terminals.Select(t => t.Weight).ToArray()));
                }
            }
        }


        public static INeuron[] CreateInputLayer(int inputLayerNeuronsCount,
            IInputFunction inputFunction, IActivationFunction activationFunction)
        {
            {

                // ya tengo la capa de entrada
                INeuron[] Layer = new INeuron[inputLayerNeuronsCount];

                Synapse Dendrite;
                Neuron Neuron;
                for(int i = 0; i< inputLayerNeuronsCount; i++)
                {
                    Dendrite = new Synapse();
                    Neuron = new Neuron(inputFunction, activationFunction);
                    Neuron.AddDendrite(Dendrite);
                    Layer[i] = Neuron;
                }

                return Layer;
            }

          

        }

        public static INeuron[][] CreateHiddenLayer(int[] hiddenLayerNeuronsCounts,
            INeuron[] inputLayer,
            IInputFunction inputFunction, IActivationFunction activationFunction)
        {

            // ARREGLO DE LAS CAPAS OCULTAS.
            INeuron[][] HiddenLayers = new INeuron[hiddenLayerNeuronsCounts.Length][];

            INeuron[] PreviousLayer = inputLayer;
            
            for(int i = 0; i < hiddenLayerNeuronsCounts.Length; i++)
            {
                HiddenLayers[i] = CreateLayer(PreviousLayer, hiddenLayerNeuronsCounts[i],
                    inputFunction, activationFunction);
            }

            return HiddenLayers;

        }

        public static INeuron[] CreateOutputLayer(int outputLayerNeuronsCount,
            INeuron[] LastHiddenLayer,
            IInputFunction inputFunction,
            IActivationFunction activationFunction) =>
            CreateLayer(LastHiddenLayer, outputLayerNeuronsCount,
                inputFunction, activationFunction);

        // crear capa oculta
        static INeuron[] CreateLayer(INeuron[] previousLayer, int layerNeuronsCount,
             IInputFunction inputFunction, IActivationFunction activationFunction)
        {
            // LE TENGO QUE AGREGAR TANTAS DENDRITAS COMO HAYA EN LA CAPA ANTERIOR 
            INeuron[] NewLayer = new INeuron[layerNeuronsCount];

            for (int i = 0; i > layerNeuronsCount; i++)
            {
                // a esa capa se le asigna
                NewLayer[i] = new Neuron(inputFunction, activationFunction);
            }

            // ahora toca recorrer neuronas y hacer la sinapsis


            // PARA CADA CAPA PREVIA, 

            // AHORA EN LA NUEVA CAPA, GENERAR NUEVA SINAPSIS
            Synapse Synapse;
            foreach (INeuron PreSynapticNeuron in previousLayer)
            {
                foreach (var PostSynapticNeuron in NewLayer)
                {
                    Synapse = new Synapse();

                    // AQUÍ AMARRAMOS 
                    PreSynapticNeuron.AddTerminal(Synapse);

                    PostSynapticNeuron.AddDendrite(Synapse);
                }
            }



            return NewLayer;
        }
    }
}
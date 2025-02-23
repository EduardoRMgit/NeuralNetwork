using NeuralNetwork.Abstractions;
using NeuralNetwork.Library.ActivationFunctions;
using NeuralNetwork.Library.Helpers;
using NeuralNetwork.Library.InputFunctions;
using NeuralNetwork.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.Implementations
{
    public abstract class NeuralNetworkBase : INeuralNetwork
    {
        public INeuron[] InputLayer { get; private set; }
        public INeuron[][] HiddenLayers { get; private set; }
        public INeuron[] OutputLayer { get; private set; }

        public abstract double[] Predict(double[] inputs);

        public abstract void Train(double[][] trainingData, double[][] targets,
            int epochs, double learningRate);

        public IEnumerable<NeuronInfo> GetNeuronInfos() =>
            // me devuelve a mi mismo porque soy una red neuronal
            NeuralNetworkHelper.GetNeuronsInfo(this);

        public void SaveModel(string filePath)
        {
            var Model = new ModelParameters
            {
                InputLayerNeuronsCount = InputLayer.Count(),
                HidenLayerNeuronsCount = HiddenLayers.Select(l => l.Length).ToArray(),
                OutputLayerNeuronsCount = OutputLayer.Count(),
                NeuronInfos= NeuralNetworkHelper.GetNeuronsInfo(this)
            };

            File.WriteAllText(filePath, JsonSerializer.Serialize(Model));
        }

        public INeuralNetwork LoadModel(string filePath)
        {
            string Content = File.ReadAllText(filePath);
            var Model = JsonSerializer.Deserialize<ModelParameters>(Content);

            CreateNeuralNetwork(Model.InputLayerNeuronsCount,
                Model.HidenLayerNeuronsCount,
                Model.OutputLayerNeuronsCount);


            // indice de la capa de sálida
            int OutputLayerIndex = Model.HidenLayerNeuronsCount.Length + 1;
            INeuron Neuron;

            foreach(var Item in Model.NeuronInfos)
            {
                if(Item.LayerIndex == 0)
                {
                    Neuron = InputLayer[Item.NeuronIndex];
                }
                else if(Item.LayerIndex == OutputLayerIndex)
                {
                    Neuron = OutputLayer[Item.NeuronIndex];
                }
                else
                {
                    Neuron = HiddenLayers[Item.LayerIndex - 1][Item.NeuronIndex];
                }

                Neuron.SetBiasValue(Item.Bias);

                for(int i = 0; i < Item.Weights.Count(); i++)
                {
                    Neuron.Axon.Terminals[i].SetWeightValue(Item.Weights.ElementAt(i));
                }
            }

            return this;
        }


        #region Input and Activations Functions
        protected IInputFunction InputLayerInputFunction { get; set; } = null;
        protected IActivationFunction InputLayerActivationFunction { get; set; } = null;


        protected IInputFunction HiddenLayerInputFunction { get; set; } = new WeightedSumInputFunction();
        protected IActivationFunction HiddenLayerActivationFunction { get; set; } = new HyperbolicTangentActivationFunction();


        protected IInputFunction OutputLayerInputFunction { get; set; } = new WeightedSumInputFunction();
        protected IActivationFunction OutputLayerActivationFunction { get; set; } = null;
        #endregion

        // dendritas de entrada del input layer

        // dendritas del input, sú unica dendrita, voy a tomarla para que sea la dendrita de entrada.
        protected IEnumerable<ISynapse> InputDendrites =>
            InputLayer.Select(n => n.Dendrites.First());

        // dame n número de neuronas para poder construir mi red neuronal
        protected void CreateNeuralNetwork(
            int inputLayerNeuronsCount, 
            int[] hiddenLayerNeuronsCount,
            int outputLayerNeuronsCount)
        {
            InputLayer = NeuralNetworkHelper.CreateInputLayer(inputLayerNeuronsCount, 
                InputLayerInputFunction, InputLayerActivationFunction);

            // construir neurona, las dendritas,crear sinapse y asignale a la terminal de la capa previa
            // y asignar a la dendrita en esa capa

            HiddenLayers = NeuralNetworkHelper.CreateHiddenLayers(hiddenLayerNeuronsCount,
                InputLayer,
                HiddenLayerInputFunction,
                HiddenLayerActivationFunction);


            OutputLayer = NeuralNetworkHelper.CreateOutputLayer(outputLayerNeuronsCount,
                HiddenLayers[^1], // va a agarrar el último elemento.
                OutputLayerInputFunction, OutputLayerActivationFunction);

            // cuando construimos la red neuronal es establecer los pesos iniciales, por ejemplo, a la primer sinapsis, el 0.1, 0.2, 03
            // es bien importante la inicialización

            // por ejemplo, algoritmo de inicialización de Javier
            Initialize();
        }

        protected abstract void Initialize();
    }
}

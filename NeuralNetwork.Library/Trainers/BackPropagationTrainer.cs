using NeuralNetwork.Abstractions;
using NeuralNetwork.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.Trainers;

public static class BackPropagationTrainer
{
    public static double ApplyBackPropagation(INeuralNetwork network,
        double[] inputs, double[] targets, double learningRate,
        // función que recibe el arreglo de dobles y devuelve el arreglo de dobles, el delegado del predict
        Func<double[], double[]> predicDelegate)
    {
        // Propagación hacia adelante => alimentar las dendritas para que corra un ciclo.
        double[] Output = predicDelegate(inputs);
        double Loss = 0.0;

        // calcular el error y ajustar la capa de sálida
        for(int i = 0; i < network.OutputLayer.Length; i++)
        {
            INeuron Neuron = network.OutputLayer[i];
            // calcular el error como el valor de diferencia de la neurona y el valor esperado
            double Error = Neuron.OutputValue - targets[i];

            // delta es el gradiente del error y en que medida la neurona contribuyó al error

            double Delta = Error * FunctionsHelper.SigmoidDerivative(Neuron.OutputValue);
            // debemos guardar este delta, porque luego, cuando hagamos el back propagation.

            Neuron.Delta = Delta;

            // actualizar el valor del bias.
            Neuron.SetBiasValue(Neuron.Bias - learningRate * Delta);

            // GUARDAR LAS PERDIDAS
            Loss += Error * Error;


            // RECORRER CADA NEURONA DE LA ÚLTIMA CAPA OCULTA

            for(int j = 0; j< network.HiddenLayers[^1].Length; j++)
            {
                // de la última capa, la neurona j
                INeuron HiddenNeuron = network.HiddenLayers[^1][j];
                double CurrentWeight = HiddenNeuron.Axon.Terminals[i].Weight;

                // gradiante delta es grande, contribuyó al error, así que tiene que ser ajustado
                // ASUMAN QUE EL MATEMÁTICO LES DIJO XDXDXD
                double NewWeight = CurrentWeight - (learningRate * Delta * HiddenNeuron.OutputValue);

                HiddenNeuron.Axon.Terminals[i].SetWeightValue(NewWeight);
            }

        }


        // nos estamos yendo hacia atras.
        INeuron[] NextLayer =  network.OutputLayer;
        for(int LayerIndex = network.HiddenLayers.Length -1; LayerIndex >= 0; LayerIndex--)
        {
            for(int NeuronIndex = 0; NeuronIndex < network.HiddenLayers[LayerIndex].Length; NeuronIndex++)
            {
                INeuron CurrentNeuron =
                    network.HiddenLayers[LayerIndex][NeuronIndex];

                double Error = 0.0;

                // sumar los errores, en las redes neuronales, ahí andamos 

                for (int NextNeuronIndex = 0;
                     NextNeuronIndex < NextLayer.Length; NextNeuronIndex++)
                {
                    Error += CurrentNeuron.Axon.Terminals[NextNeuronIndex].Weight *
                        NextLayer[NextNeuronIndex].Delta;
                }

                double Delta = Error * FunctionsHelper.SigmoidDerivative(CurrentNeuron.OutputValue);
                CurrentNeuron.SetBiasValue(CurrentNeuron.Bias - learningRate*Delta);
                CurrentNeuron.Delta = Delta;

                // ahora ajustar el peso con las conexiones de la capa previa

                INeuron[] PreviousLayer;
                if(LayerIndex == 0)
                {
                    PreviousLayer = network.InputLayer;
                }
                else
                {
                    PreviousLayer = network.HiddenLayers[LayerIndex - 1];
                }

                for(int PrevNeuronIndex = 0; PrevNeuronIndex < PreviousLayer.Length;
                    PrevNeuronIndex++)
                {
                    INeuron PreviousNeuron = PreviousLayer[PrevNeuronIndex];
                    double CurrentWeight = PreviousNeuron.Axon.Terminals[NeuronIndex].Weight;

                    double NewWeight = CurrentWeight - learningRate * Delta *
                        PreviousNeuron.OutputValue;

                    PreviousNeuron.Axon.Terminals[NeuronIndex].SetWeightValue(NewWeight);
                }


            }

            NextLayer = network.HiddenLayers[LayerIndex];


        }
        return Loss / network.OutputLayer.Length;

    }
}

using NeuralNetwork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.Initializers;

public static class XavierInitializer
{

    public static void InitializeUniform(INeuralNetwork network)
    {
        InitializeWeightsWithXaverInitializer(network, (nin, nout) =>
        {
            double Limit = Math.Sqrt(6) / Math.Sqrt(nin + nout);

            return (-Limit, Limit);
        });
    }

    public static void InitializeNormal(INeuralNetwork network)
    {
        InitializeWeightsWithXaverInitializer(network, (nin, nout) =>
        {
            double Limit = Math.Sqrt(2) / Math.Sqrt(nin + nout);

            return (0, Limit);
        });
    }


    // uniforme de xavier 6
    // uniforme de normalización de xavier 2
    static void InitializeWeightsWithXaverInitializer(INeuralNetwork network,
        // numero de neuronas de la capa de entrada, capa de salida y que me devuelva la tupla con valores aleatorios
        Func<int, int, (double, double)> initializer)
    {
        var Rnd = new Random();

        // INITIALIZAR los pesos de la capa de entrada con la capa posterior (la primera oculta).
        Initialize(network.InputLayer, network.HiddenLayers[0], Rnd, initializer);

        for(int i = 0; i < network.HiddenLayers.Length -1; i++)
        {
            Initialize(network.HiddenLayers[i],
                network.HiddenLayers[i + 1], Rnd, initializer);
        }

        Initialize(network.HiddenLayers[^1], network.OutputLayer, Rnd, initializer);
    }


    static void Initialize(INeuron[] currentLayer, INeuron[] nextLayer,
        Random rnd, Func<int, int,(double, double)> initializer)
    {
        // va a inicializar el current layer, y cada una de uss terminales, le va a poner su peso, va a ser un peso random, que cae en un intervalo
        // del initializer
        int INeuronsCount = currentLayer.Length;
        int OutNeuronsCount = nextLayer.Length;

        (double LowerLimit, double UpperLimit) =
            initializer(INeuronsCount, OutNeuronsCount);

        foreach(INeuron Neuron in currentLayer)
        {
            foreach(var Terminal in Neuron.Axon.Terminals)
            {
                Terminal.SetWeightValue(LowerLimit + 
                    (rnd.NextDouble() * (UpperLimit - LowerLimit)));
            }
        }
    }

}

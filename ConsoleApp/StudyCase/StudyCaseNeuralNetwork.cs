using NeuralNetwork.Abstractions;
using NeuralNetwork.Library.ActivationFunctions;
using NeuralNetwork.Library.Implementations;
using NeuralNetwork.Library.Initializers;
using NeuralNetwork.Library.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.StudyCase;

// hereda del base e implementa el INeuralNetwork
public class StudyCaseNeuralNetwork : NeuralNetworkBase, INeuralNetwork
{
    public StudyCaseNeuralNetwork()
    {
        HiddenLayerActivationFunction = new SigmoidActivationFunction();
        OutputLayerActivationFunction = new SigmoidActivationFunction();
        CreateNeuralNetwork(2, [8], 1);
        // vamos a usar el sigmoid function.
    }

    protected override void Initialize()
    {
        XavierInitializer.InitializeUniform(this);
    }

    // Alimentando las dendritas
    public override double[] Predict(double[] inputs)
    {
        int InputIndex = 0;
        foreach(var Dendrite in InputDendrites)
        {
            Dendrite.ReceiveInputValue(inputs[InputIndex++]);
        }

        var Result = OutputLayer.Select(n => n.OutputValue).ToArray();

        return Result;
    }

    // Este es el cerebro de la red neuronal XD
    public override void Train(
        // primera y segunda columna del csv
        double[][] trainingData, 
        // resultados finales.
        double[][] targets, 
        // Cuantas veces lo repetimos
        int epochs, 
        // taza de aprendizaje 
        double learningRate)
    {
        // hay una regla 70-30, 
        // tienes tus datos, cuando empieces a entrenar, toma un 70% de los datos para entrenar y 30% para probar.
        for(int Epoch = 0; Epoch < epochs; Epoch++)
        {
            double TotalLoss = 0.0;

            // recorrer cada uno de los datos y predice
            for (int i = 0; i < trainingData.Length; i++)
            {
                // aplicar algoritmos de entrenamiento, uno de los más utilizados es el back propagation.

                // Propagación hacia adelante., 

                // calculo del error => calcula el error adecuado :D

                // retropropagación => ajustar los pesos

                // repetir
                // ajuste de pesos para mejorar predicciones.


                // Alimentar
                // toma los datos que le paso, y luego, los coloca en las dendritas y obtiene los datos de sálida
                // luego compara los resultados que generó, contra los targets

                // luego hacia tras (capa de atrasa) ajustando pesos. así hasta la capa inicial

                // vuelve a alimentar:

                TotalLoss += BackPropagationTrainer.ApplyBackPropagation(
                    this, trainingData[i], targets[i], learningRate, Predict);

            }
            // media square Error, esto ira reduciendo.
            double Mse = TotalLoss / trainingData.Length;
            Console.WriteLine($"Epoch {Epoch + 1}/ {epochs} - Loss (MSE): {Mse:F6}");


        }
    }
}

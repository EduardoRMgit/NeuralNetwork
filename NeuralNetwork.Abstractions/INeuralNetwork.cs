using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Abstractions
{
    public interface INeuralNetwork
    {

        // hay que distinguir entre capas.

        // un arregla de neuronas.
        // un arreglo bidimensional


        // Arreglo de neuronas.
        INeuron[] InputLayer { get; } // Capa de entrada Neurons [0], la oculta [1, 2, 3], la de salida sería 4.


        INeuron[][] HiddenLayers { get; }


        INeuron[] OutputLayer { get; }

        // arreglo de entradas => arreglo de salidas.
        double[] Predict(double[] input);

        // un arreglo de datos de entrada, y lo voy a comparar con un 

        // vas a pasar, por cada entrada, al proceso de entrenamiento le vamos a dar 2 datos de entrenamiento con datos de sálida, sino es la sálida esperada
        // 

        // epocas y un factor de cuánto se puede equivocar, los pases que hay que seguir
        // taza de aprendizaje

        //red neuronal con aprendizaje supervisado,

        // el learning rate a que paso va a ir resolviendo el error.
        void Train(double[][] trainingData, double[][] targets, int epochs, double learningRate);

        // obtener info de neuronas, peso y bias, exceptuando el valor de sálida
        IEnumerable<NeuronInfo> GetNeuronInfos();

        void SaveModel(string filepath);

        INeuralNetwork LoadModel(string filepath);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Abstractions
{
    public interface IActivationFunction
    {
        // va a recibir lo que sacó el input function
        double CalculateOutput(double input);


        // UNA NEURONA RECIBE UN VALOR, ESE 1.0, SE LO MANDA A NEURONAS ADYACENTES,

        // UNA NEURONA RECIBE VALORES CON PESOS, Y VAN RECIBIENDO SUS VALORES,

        // LA NEURONA, CON LOS VALORES RECIBIDOS, Y LUEGO SUMA LOS VALORES DE LOS PRODUCTOS Y LUEGO LE SUMA EL BIAS.

        // SUMA EL VALOR DE ENTRADA * PESO   Y A LA SUMA, EL BIAS.

        // Y EL PROCESO SE REPITE.
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Abstractions
{
    public interface IInputFunction
    {
        // método que reciba las dendritas con valor y peso y con eso ya calculo la entrada
        double CalculateInput(IEnumerable<ISynapse> dendrites, double bias);


        // La función que se diaspara es el InputFunction, es calcular el valor de entrada, que viene de las dendritas.
        // 

        // UNA VEZ QUE RECIBÍ EL VALOR, LO TENGO QUE PASAR, TIENE QUE PASAR OTRO PROCESO

        // ENTRA VALOR => INPUT FUNCTION => RESULTADO SE LO PASO AL ACTIVATION FUNCTION;
    }
}

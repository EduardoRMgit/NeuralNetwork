using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Abstractions
{
    public interface ISynapse
    {
        double Value { get; }
        double Weight { get; }

        // debe saber de que neurona viene (presinaptica) y que neurona va (post sinaptica)
        // recive el valor de entrada
        void ReceiveInputValue(double input);

        // establecer el peso
        void SetWeightValue(double value);
        // cuando entre un valor, se debe de ejecutar un método para que calcule el valor, invoco al método de activación => va a mandar el valor
        // yo necesito saber, qué hacer cuando me asignen un valor
        // sacarlo a las neuronas, cuando me alimenten tu dame una acción donde yo le pase la sinay

        // la neurona se va a enterar cuando la alimenten un valor, suscribiendose al evento de la synapse, 

        // quien quiera que le avise yo, que se suscriba a mi evento.
        event Action<ISynapse> OnInputValueReceived;


        // REPRESENTA EL PUNTO DE CONEXIÓN
    }
}

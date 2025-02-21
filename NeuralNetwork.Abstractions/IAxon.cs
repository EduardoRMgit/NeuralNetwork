using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Abstractions
{
    public interface IAxon
    {
        // mi axon va a tener terminales, ¿ qué son las terminales, son las sinapsis?
        List<ISynapse> Terminals { get; }

        // voy a recibir una sinapsis, 
        void AddTerminal(ISynapse terminal);


        // darle valor a mis terminales
        void SendOutputValueToTerminals(double value); 
    }
}

// EL AXON REPRESENTA DE LA NEURONA, LA SÁLIDA, EN UNA NEURONA HAY UN AXON,, AL FINAL DEL AXON ESTAN LAS TERMINALES SINAPTICAS
// Y SE LO PODEMOS AGREGAR CON EL MÉTODO, TAMBIÉN PUEDE ALIMENTAR UN VALOR.
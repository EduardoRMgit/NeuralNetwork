using NeuralNetwork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.Implementations
{
    internal class Axon : IAxon
    {
        public List<ISynapse> Terminals { get; } = [];

        //readonly List<ISynapse> TerminalsField = [];

        public void AddTerminal(ISynapse terminal)
        {
            Terminals.Add(terminal);
        }

        // enviar el valor de salida a las terminales
        public void SendOutputValueToTerminals(double value)
        {
            foreach (ISynapse Terminal in Terminals)
            {
                // a las terminales, recibe el papel de entrada.
                Terminal.ReceiveInputValue(value);
            }


        }
    }
}

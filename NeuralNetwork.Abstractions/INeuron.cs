using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Abstractions
{
    public interface INeuron
    {
        //listado de entradas
        IEnumerable<ISynapse> Dendrites { get; }
        // transportar el resultado :D
        IAxon Axon { get; }

        double OutputValue { get; }

        // necesitamos el sesgo
        double Bias { get; }

        void SetBiasValue(double value);

        void AddDendrite(ISynapse dendrite);

        void AddTerminal(ISynapse terminal);

        // action= recibe
        // func = retorne
        IInputFunction InputFunction { get; }
        IActivationFunction ActivationFunction { get; }

    }
}
// ES EL CIRCULITO AMARILLO
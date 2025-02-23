using NeuralNetwork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.Implementations
{
    // necesitamos que nos pasen
    internal class Neuron(
        IInputFunction inputFunction,
        IActivationFunction activationFunction) : INeuron
    {
        public IEnumerable<ISynapse> Dendrites => DendritesField;

        readonly List<ISynapse> DendritesField = [];


        public IAxon Axon { get; } = new Axon();

        public double OutputValue { get; private set; }

        public double Bias { get; private set; }
        public double Delta { get; set; }

        public IInputFunction InputFunction => inputFunction;

        public IActivationFunction ActivationFunction => activationFunction;

        public void AddDendrite(ISynapse dendrite)
        {
            dendrite.OnInputValueReceived += Dendrite_OnInputValueReceived;
            DendritesField.Add(dendrite);
        }

        public void AddTerminal(ISynapse terminal)
        {
            Axon.AddTerminal(terminal);
        }

        public void SetBiasValue(double value)
        {
            Bias = value;
        }

        private void Dendrite_OnInputValueReceived(ISynapse dendrite)
        {
            // ejecutar el input function porque recibió la entrada.
            // es como para validar si es default.
            double InputValue = inputFunction != default
                ? inputFunction.CalculateInput(DendritesField, Bias) :
                // si no hay input function, se pasa el valor de la dendrita
                dendrite.Value;

            OutputValue = activationFunction != default
                ? activationFunction.CalculateOutput(InputValue)
                : InputValue;

            Axon.SendOutputValueToTerminals(OutputValue);
        }


    }
}

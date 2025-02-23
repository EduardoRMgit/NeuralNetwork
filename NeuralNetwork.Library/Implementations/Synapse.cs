using NeuralNetwork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.Implementations
{
    public class Synapse : ISynapse
    {
        public double Value { get; private set; }

        public double Weight {  get; private set; }

        public event Action<ISynapse> OnInputValueReceived;

        public void ReceiveInputValue(double value)
        {
            // Recibe un valor y luego invoca, notifica, le va a avisar a las neuronas
            // que recibio el valor.
            // me avisas cuando me llegue a un valor.
            // a mi neurona, a mi sinapse, ya le llegó el valor.
            Value = value;
            OnInputValueReceived?.Invoke(this);
        }

        public void SetWeightValue(double value)
        {
            Weight = value;
        }
    }
}

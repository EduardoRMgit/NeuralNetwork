using NeuralNetwork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.ActivationFunctions
{
    public class HyperbolicTangentActivationFunction: IActivationFunction
    {
        public double CalculateOutput(double input) => 
            Math.Tanh(input);
    }
}

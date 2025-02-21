using NeuralNetwork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.InputFunctions
{
    public class WeightedSumInputFunction: IInputFunction
    {
        public double CalculateInput(IEnumerable<ISynapse> dendrites, double bias) =>
            dendrites.Sum(d => d.Value * d.Weight) + bias;
    }
}

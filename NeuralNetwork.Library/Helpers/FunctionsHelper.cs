using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.Helpers
{
    internal static class FunctionsHelper
    {
        public static double SigmoidDerivative(double x)
        {
            return x * (1 - x);
        }


    }
}

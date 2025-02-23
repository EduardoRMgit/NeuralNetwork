using NeuralNetwork.Abstractions;

namespace NeuralNetwork.Library.ActivationFunctions;

public class SigmoidActivationFunction : IActivationFunction
{
    public double CalculateOutput(double input)
    {
        return 1 / (1 + Math.Exp(-input));
    }
}

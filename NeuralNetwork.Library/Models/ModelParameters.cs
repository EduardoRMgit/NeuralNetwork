using NeuralNetwork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Library.Models
{
    // lo que guarde aquí voy me va a servir para predecir para reconstruir mi red neuronal
    internal class ModelParameters
    {
        //  n neuronas del input layer.
        public int InputLayerNeuronsCount { get; set; }

        public int[] HidenLayerNeuronsCount { get; set; }

        public int OutputLayerNeuronsCount { get; set;  }

        public IEnumerable<NeuronInfo> NeuronInfos { get; set; }

    }
}

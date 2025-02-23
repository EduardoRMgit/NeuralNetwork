using ConsoleApp.StudyCase.Model;
using CSVLibrary;
using NeuralNetwork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.StudyCase;

static class StudyCaseExample
{

    public static void ExecuteExample()
    {
        var Data = GetFileData();
        (var TrainingData, var TestData) = GetTranAndTestData(Data);

        // llamar a la red neuronal => los inputs cómo los quiero.
        double[][] TrainingInputs = ExtractInputs(TrainingData);
        double[][] TrainingTargets = ExtractTargets(TrainingData);

        INeuralNetwork Network = new StudyCaseNeuralNetwork();

        Network.Train(TrainingInputs, TrainingTargets, 100, 0.25);

        (double[] Data, int Expected)[] SampleData =
            [
                ([.4035, .7300], 1),
                ([.4035, .650], 0),
                ([.523, .712], 1),
                ([.655, .435], 0),
                ([.347, .617], 0),
                ([.459, .745], 1),
                ([.359, .659], 0),
                ([.712, .523], 0),
                ([.435, .655], 0),
                ([.959, .959], 1),
                ([.459, .730], 1),
            ];

        foreach(var Item in SampleData)
        {
            Console.Write($"[{string.Join(", ", Item.Data)}], Expected/Predicted: ");
            Console.Write($"{Item.Expected}, ");
            var Predicted = Network.Predict(Item.Data)[0];

            Console.WriteLine($"{(Predicted >= .5 ? 1 : 0)}  ({Predicted})");
        }
    }

    private static double[][] ExtractInputs(IEnumerable<StudyData> data) =>
        data.Select(s => new double[]
        {
            s.StudyHours,
            s.SleepHours,
        }).ToArray();

    private static double[][] ExtractTargets(IEnumerable<StudyData> data) =>
        data.Select(s => new double[]
        {
            s.Expected
        }).ToArray();

    static (IEnumerable<StudyData> TrainingData, IEnumerable<StudyData> TestData) GetTranAndTestData(StudyData[] data)
    {
        Random Rnd = new();
        var ShuffleData = data.OrderBy(x => Rnd.Next()).ToList();
        int TrainSize = (int)(ShuffleData.Count * 0.7);
        var TrainingData = ShuffleData.Take(TrainSize).ToList();
        var TestData = ShuffleData.Skip(TrainSize).ToList();

        return (TrainingData, TestData);
    }


    static StudyData[] GetFileData()
    {
        string FileName = "C:\\NeuralNetwork\\NeuralNetwork\\ConsoleApp\\StudyCase\\Assets\\StudyData.csv";
        var RawData = CsvReader.Read(FileName);

        List<StudyData> Data = new();
        foreach (var Item in RawData)
        {
            Data.Add(new StudyData(
                double.Parse(Item[0]),
                double.Parse(Item[1]),
                double.Parse(Item[2])
            ));
        }

        return Data.ToArray();
    }


}

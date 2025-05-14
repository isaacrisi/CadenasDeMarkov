using System;
using System.Collections.Generic;
using System.Linq;

class ChapmanKolmogorov
{
    public static (double[,], List<string>) ConvertToMatrix(Dictionary<string, Dictionary<string, double>> transitions)
    {
        var allWords = transitions.Keys
            .Union(transitions.SelectMany(kv => kv.Value.Keys))
            .Distinct()
            .OrderBy(w => w)
            .ToList();

        int size = allWords.Count;
        var matrix = new double[size, size];

        var wordToIndex = allWords.Select((w, i) => (w, i)).ToDictionary(x => x.w, x => x.i);

        foreach (var fromWord in transitions.Keys)
        {
            foreach (var toWord in transitions[fromWord].Keys)
            {
                int i = wordToIndex[fromWord];
                int j = wordToIndex[toWord];
                matrix[i, j] = transitions[fromWord][toWord];
            }
        }

        return (matrix, allWords);
    }

    public static double[,] MultiplyMatrices(double[,] A, double[,] B)
    {
        int size = A.GetLength(0);
        var result = new double[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                for (int k = 0; k < size; k++)
                    result[i, j] += A[i, k] * B[k, j];

        return result;
    }

    public static double[,] PowerMatrix(double[,] matrix, int n)
    {
        int size = matrix.GetLength(0);
        var result = new double[size, size];

        for (int i = 0; i < size; i++)
            result[i, i] = 1.0;

        for (int p = 0; p < n; p++)
        {
            result = MultiplyMatrices(result, matrix);
        }

        return result;
    }

    public static void ShowProbabilities(double[,] Pn, List<string> words, string fromWord)
    {
        var wordToIndex = words.Select((w, i) => (w, i)).ToDictionary(x => x.w, x => x.i);

        if (!wordToIndex.ContainsKey(fromWord))
        {
            Console.WriteLine($"La palabra '{fromWord}' no está en el vocabulario.");
            return;
        }

        int index = wordToIndex[fromWord];

        Console.WriteLine($"Probabilidades a n pasos desde '{fromWord}':");

        foreach (var (word, i) in words.Select((w, i) => (w, i)))
        {
            if (Pn[index, i] > 0.0001)
                Console.WriteLine($"  -> {word}: {Pn[index, i]:0.0000}");
        }
    }

    public static double GetProbabilityAtNSteps(Dictionary<string, Dictionary<string, double>> transitions, string fromWord, string toWord, int n)
    {
        var (matrix, words) = ConvertToMatrix(transitions);

        var wordToIndex = words.Select((w, i) => (w, i)).ToDictionary(x => x.w, x => x.i);

        if (!wordToIndex.ContainsKey(fromWord) || !wordToIndex.ContainsKey(toWord))
        {
            Console.WriteLine($"Palabras no encontradas en el vocabulario.");
            return 0.0;
        }

        var Pn = PowerMatrix(matrix, n);

        int fromIndex = wordToIndex[fromWord];
        int toIndex = wordToIndex[toWord];

        return Pn[fromIndex, toIndex];
    }

}

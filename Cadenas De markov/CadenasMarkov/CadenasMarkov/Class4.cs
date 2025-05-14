using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class InitialDistribution
{
    public static Dictionary<string, int> CalculateInitialFrequencies(IEnumerable<string> texts)
    {
        var initialFrequencies = new Dictionary<string, int>();

        foreach (var text in texts)
        {
            var words = Preprocess(text);

            if (words.Count > 0)
            {
                string firstWord = words[0];

                if (!initialFrequencies.ContainsKey(firstWord))
                    initialFrequencies[firstWord] = 0;

                initialFrequencies[firstWord]++;
            }
        }

        return initialFrequencies;
    }

    public static Dictionary<string, double> CalculateInitialProbabilities(Dictionary<string, int> frequencies)
    {
        int total = frequencies.Values.Sum();
        return frequencies.ToDictionary(kvp => kvp.Key, kvp => Math.Round((double)kvp.Value / total, 4));
    }

    private static List<string> Preprocess(string text)
    {
        text = text.ToLowerInvariant();
        text = Regex.Replace(text, "[áàä]", "a");
        text = Regex.Replace(text, "[éèë]", "e");
        text = Regex.Replace(text, "[íìï]", "i");
        text = Regex.Replace(text, "[óòö]", "o");
        text = Regex.Replace(text, "[úùü]", "u");
        text = Regex.Replace(text, @"[^\w\s]", ""); // quitar puntuación

        return text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static void ExportInitialDistributionToCsv(Dictionary<string, double> initialProbabilities, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Palabra;Probabilidad");

            foreach (var kvp in initialProbabilities.OrderByDescending(p => p.Value))
            {
                writer.WriteLine($"{kvp.Key};{kvp.Value.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture)}");
            }
        }

        Console.WriteLine($"Distribución inicial exportada a: {filePath}");
    }
}

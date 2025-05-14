using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class MarkovModel
{
    private Dictionary<string, Dictionary<string, int>> frequencyMatrix = new();
    public Dictionary<string, Dictionary<string, double>> transitionMatrix = new();

    public void Train(IEnumerable<string> texts)
    {
        foreach (var text in texts)
        {
            var words = Preprocess(text);

            for (int i = 0; i < words.Count - 1; i++)
            {
                string current = words[i];
                string next = words[i + 1];

                if (!frequencyMatrix.ContainsKey(current))
                    frequencyMatrix[current] = new Dictionary<string, int>();

                if (!frequencyMatrix[current].ContainsKey(next))
                    frequencyMatrix[current][next] = 0;

                frequencyMatrix[current][next]++;
            }
        }

        BuildTransitionMatrix();
    }

    private void BuildTransitionMatrix()
    {
        foreach (var fromWord in frequencyMatrix.Keys)
        {
            int totalTransitions = frequencyMatrix[fromWord].Values.Sum();

            transitionMatrix[fromWord] = new Dictionary<string, double>();

            foreach (var toWord in frequencyMatrix[fromWord].Keys)
            {
                double probability = (double)frequencyMatrix[fromWord][toWord] / totalTransitions;
                transitionMatrix[fromWord][toWord] = Math.Round(probability, 4); // Redondeo opcional
            }
        }
    }

    private List<string> Preprocess(string text)
    {
        text = text.ToLowerInvariant();
        text = Regex.Replace(text, "[áàä]", "a");
        text = Regex.Replace(text, "[éèë]", "e");
        text = Regex.Replace(text, "[íìï]", "i");
        text = Regex.Replace(text, "[óòö]", "o");
        text = Regex.Replace(text, "[úùü]", "u");
        text = Regex.Replace(text, @"[^\w\s]", ""); // Quitar puntuación

        return text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}

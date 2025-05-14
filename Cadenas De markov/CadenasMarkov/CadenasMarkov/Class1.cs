using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class MarkovChain
{
    private Dictionary<string, Dictionary<string, int>> transitions = new();
    private Random rng = new();

    public void Train(string text)
    {
        var words = Preprocess(text);

        for (int i = 0; i < words.Count - 1; i++)
        {
            string current = words[i];
            string next = words[i + 1];

            if (!transitions.ContainsKey(current))
                transitions[current] = new Dictionary<string, int>();

            if (!transitions[current].ContainsKey(next))
                transitions[current][next] = 0;

            transitions[current][next]++;
        }
    }

    public string Generate(string startWord, int length = 50)
    {
        List<string> result = new() { startWord };

        string current = startWord;
        for (int i = 0; i < length - 1; i++)
        {
            if (!transitions.ContainsKey(current))
                break;

            current = ChooseNext(transitions[current]);
            result.Add(current);
        }

        return string.Join(" ", result);
    }

    private string ChooseNext(Dictionary<string, int> options)
    {
        int total = options.Values.Sum();
        int choice = rng.Next(total);

        foreach (var pair in options)
        {
            choice -= pair.Value;
            if (choice < 0)
                return pair.Key;
        }

        return options.Keys.First(); // fallback
    }

    private List<string> Preprocess(string text)
    {
        text = text.ToLowerInvariant();
        text = Regex.Replace(text, "[áàä]", "a");
        text = Regex.Replace(text, "[éèë]", "e");
        text = Regex.Replace(text, "[íìï]", "i");
        text = Regex.Replace(text, "[óòö]", "o");
        text = Regex.Replace(text, "[úùü]", "u");
        text = Regex.Replace(text, @"[^\w\s]", ""); // remove punctuation

        return text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}

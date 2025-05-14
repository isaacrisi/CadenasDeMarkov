using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Exporter
{
    public static void ExportTransitionMatrixToCsv(Dictionary<string, Dictionary<string, double>> transitionMatrix, string filePath)
    {
        // Obtener todas las palabras únicas (from y to) para crear las filas y columnas de la matriz
        var allWords = transitionMatrix.Keys
            .Union(transitionMatrix.SelectMany(kv => kv.Value.Keys))
            .Distinct()
            .OrderBy(w => w)
            .ToList();

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Escribir encabezado (columnas)
            writer.Write(",");
            writer.WriteLine(string.Join(",", allWords));

            // Escribir filas
            foreach (var fromWord in allWords)
            {
                writer.Write(fromWord);

                foreach (var toWord in allWords)
                {
                    double probability = 0.0;

                    if (transitionMatrix.ContainsKey(fromWord) && transitionMatrix[fromWord].ContainsKey(toWord))
                    {
                        probability = transitionMatrix[fromWord][toWord];
                    }

                    writer.Write($",{probability.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture)}");
                }

                writer.WriteLine();
            }
        }

        Console.WriteLine($"Matriz de transición exportada correctamente a: {filePath}");
    }
}

class Program
{
    static void Main()
    {
        MarkovChain chain = new();

        foreach (string file in Directory.GetFiles("Corpus"))
        {
            string text = File.ReadAllText(file);
            chain.Train(text);
        }

        Console.WriteLine("Texto generado:");
        Console.WriteLine(chain.Generate("la", 60));

        // Leer todos los textos del corpus
        var corpusTexts = Directory.GetFiles("Corpus")
                                   .Select(File.ReadAllText);

        // Entrenar el modelo de Markov
        MarkovModel model = new();
        model.Train(corpusTexts);

        // Ahora tienes la matriz de transiciones:
        var transitionMatrix = model.transitionMatrix;

        var communicationClasses = CommunicationClasses.FindCommunicationClasses(transitionMatrix);


        // Mostrar ejemplo por consola
        foreach (var fromWord in transitionMatrix.Keys)
        {
            Console.WriteLine($"Desde '{fromWord}':");
            foreach (var toWord in transitionMatrix[fromWord])
            {
                Console.WriteLine($"  -> '{toWord.Key}': {toWord.Value}");
            }

        }
        
        // Exportar la matriz de transición a CSV
        Exporter.ExportTransitionMatrixToCsv(transitionMatrix, "matriz_markov.csv");

        // Calcular frecuencias iniciales
        var frequencies = InitialDistribution.CalculateInitialFrequencies(corpusTexts);

        // Calcular probabilidades iniciales
        var initialProbabilities = InitialDistribution.CalculateInitialProbabilities(frequencies);

        // Exportar a CSV (usando ; para Excel LATAM)
        InitialDistribution.ExportInitialDistributionToCsv(initialProbabilities, "probabilidad_inicial.csv");

        // Mostrar las clases de comunicación encontradas
        int index = 1;
        foreach (var communicationClass in communicationClasses)
        {
            Console.WriteLine($"Clase {index}: {string.Join(", ", communicationClass)}");
            index++;
        }
        double prob = ChapmanKolmogorov.GetProbabilityAtNSteps(transitionMatrix, "ella", "camina", 1);

        Console.WriteLine($"Probabilidad de ir de 'ella' a 'camina' en 1 pasos: {prob:0.0000}");


    }
}

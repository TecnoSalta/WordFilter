namespace Counter.Strategies;

public class SimpleFindStrategy : IFindStrategy
{
    public IEnumerable<string> Find(IEnumerable<string> wordStream, HashSet<string> wordsInMatrix)
    {
        if (wordStream == null || !wordStream.Any())
            return [];

        // Filtro: solo palabras que existen EXACTAMENTE en la matriz
        var validWords = wordStream
            .Where(word => !string.IsNullOrEmpty(word) && wordsInMatrix.Contains(word))
            .ToList();

        if (!validWords.Any())
            return [];

        // Contar frecuencia REAL (cada aparición en el stream cuenta)
        var frequency = validWords
            .GroupBy(word => word, StringComparer.OrdinalIgnoreCase)
            .Select(group => new { Word = group.Key, Count = group.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Word)
            .Take(10)
            .Select(x => x.Word);

        return frequency;
    }
}
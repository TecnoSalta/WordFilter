using System.Runtime.InteropServices;

namespace Counter.Strategies;

public class OptimizedFindStrategy : IFindStrategy
{
    public IEnumerable<string> Find(IEnumerable<string> wordStream, HashSet<string> wordsInMatrix)
    {
        if (wordStream == null) return [];

        var frequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        int processed = 0;

        foreach (var word in wordStream)
        {
            processed++;
            
            if (string.IsNullOrEmpty(word) || !wordsInMatrix.Contains(word)) continue;

            ref int count = ref CollectionsMarshal.GetValueRefOrAddDefault(frequency, word, out bool exists);
            count++;

            // Monitoring para streams muy grandes
            if (processed % 1000000 == 0)
            {
                Console.WriteLine($"Processed {processed} words, unique matches: {frequency.Count}");
            }
        }

        return GetTop10Efficiently(frequency);
    }

    private static IEnumerable<string> GetTop10Efficiently(Dictionary<string, int> frequency)
    {
        return frequency
            .OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => kvp.Key)
            .Take(10)
            .Select(kvp => kvp.Key);
    }
}

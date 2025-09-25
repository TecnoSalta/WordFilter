namespace Counter;

public class SimpleFindStrategy : IFindStrategy
{
    public IEnumerable<string> Find(IEnumerable<string> wordStream, HashSet<string> wordsInMatrix)
    {
        if (wordStream == null)
            return Enumerable.Empty<string>();

        return wordStream
            .Where(word => !string.IsNullOrEmpty(word) && wordsInMatrix.Contains(word))
            .GroupBy(word => word, StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .Take(10)
            .Select(group => group.Key);
    }
}
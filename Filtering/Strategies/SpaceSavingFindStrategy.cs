using System;
using System.Collections.Generic;
using System.Linq;

namespace Counter.Strategies;

public class SpaceSavingFindStrategy : IFindStrategy
{
    public IEnumerable<string> Find(IEnumerable<string> wordStream, HashSet<string> wordsInMatrix)
    {
        if (wordStream == null)
            return [];

        return wordStream
            .Where(word => !string.IsNullOrEmpty(word) && wordsInMatrix.Contains(word))
            .GroupBy(word => word, StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .Take(10)
            .Select(group => group.Key);
    }
}
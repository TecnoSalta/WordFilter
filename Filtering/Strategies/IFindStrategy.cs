namespace Counter.Strategies;

public interface IFindStrategy
{
    IEnumerable<string> Find(IEnumerable<string> wordStream, HashSet<string> wordsInMatrix);
}

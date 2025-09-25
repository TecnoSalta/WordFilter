namespace Counter;

public interface IFindStrategy
{
    IEnumerable<string> Find(IEnumerable<string> wordStream, HashSet<string> wordsInMatrix);
}

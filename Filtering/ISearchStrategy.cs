namespace Filtering
{
    public interface ISearchStrategy
    {
        IEnumerable<string> ExtractWords(char[,] matrix);
    }
}
namespace TestWordFilter;

public static class TestData
{
    public static readonly IEnumerable<string> MatrixWords = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k"];
    
    public static readonly IEnumerable<string> WordStreamFrequency = ["a", "a", "a", "b", "b", "c", "d"];
    
    public static readonly HashSet<string> WordsInMatrix = new(MatrixWords, StringComparer.OrdinalIgnoreCase);
    
    public static readonly HashSet<string> WordsInMatrixForFrequencyTest = new(["a", "b", "c", "d"], StringComparer.OrdinalIgnoreCase);
}

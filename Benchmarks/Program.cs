using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Counter;

public class WordFinderBenchmarks
{
    private WordFinder _wordFinder;
    private IEnumerable<string> _smallWordStream;
    private IEnumerable<string> _largeWordStream;
    private IEnumerable<string> _matrix;

    [GlobalSetup]
    public void Setup()
    {
        // Configuración que se ejecuta una vez antes de todas las mediciones
        // Aquí inicializas tu WordFinder y los wordstreams de prueba.

        _matrix = new[]
        {
            "abcde",
            "fghij",
            "klmno",
            "pqrst",
            "uvwxy"
        };
        _wordFinder = new WordFinder(_matrix);

        _smallWordStream = new[] { "abc", "fgh", "klm", "pqr", "uvw" };

        var largeStreamList = new List<string>();
        for (int i = 0; i < 5000; i++) // aumentar para mas palabras
        {
            largeStreamList.Add("abc"); // Debe existir
            largeStreamList.Add("xyz"); // No existe
            largeStreamList.Add("fgh");
        }
        _largeWordStream = largeStreamList;
    }

    [Benchmark]
    public IEnumerable<string> Find_SmallStream()
    {
        return _wordFinder.Find(_smallWordStream);
    }

    [Benchmark]
    public IEnumerable<string> Find_LargeStream()
    {
        return _wordFinder.Find(_largeWordStream);
    }

    [Benchmark]
    public WordFinder Constructor_SmallMatrix()
    {
        return new WordFinder(_matrix);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<WordFinderBenchmarks>();
    }
}
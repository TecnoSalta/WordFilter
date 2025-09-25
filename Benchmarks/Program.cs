using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Counter;
using Counter.Strategies;

namespace Benchmarks;

[MemoryDiagnoser]
public class WordFinderBenchmarks
{
    private WordFinder _wordFinder32x32;
    private WordFinder _wordFinder64x64;
    private IEnumerable<string> _wordStream;

    [GlobalSetup]
    public void Setup()
    {
        var matrix32x32 = GenerateMatrix(32);
        var matrix64x64 = GenerateMatrix(64);

        _wordFinder32x32 = new WordFinder(matrix32x32);
        _wordFinder64x64 = new WordFinder(matrix64x64);

        // Generate 1 million words to simulate large word stream scenario
        _wordStream = GenerateWordStream(1000000);
    }

    private IEnumerable<string> GenerateMatrix(int size)
    {
        var random = new Random(42);
        var matrix = new string[size];
        for (int i = 0; i < size; i++)
        {
            matrix[i] = new string(Enumerable.Range(0, size)
                .Select(_ => (char)random.Next('a', 'z' + 1)).ToArray());
        }
        return matrix;
    }

    private IEnumerable<string> GenerateWordStream(int wordCount)
    {
        var random = new Random(42);
        return Enumerable.Range(0, wordCount)
            .Select(_ =>
            {
                int length = random.Next(3, 10);
                return new string(Enumerable.Range(0, length)
                    .Select(__ => (char)random.Next('a', 'z' + 1)).ToArray());
            });
    }


    [Benchmark]
    public void Find_32x32Optimized() => _wordFinder32x32.Find(_wordStream);

    [Benchmark]
    public void Find_64x64Optimized() => _wordFinder64x64.Find(_wordStream);

    [Benchmark]
    public void Find_32x32SimpleFind() {

        _wordFinder32x32.SetStrategy(new SimpleFindStrategy());
        _wordFinder32x32.Find(_wordStream);

    }

    [Benchmark]
    public void Find_64x64SimpleFind()
    {
        _wordFinder64x64.SetStrategy(new Counter.Strategies.SimpleFindStrategy());
        _wordFinder64x64.Find(_wordStream);
    }




    [Benchmark]
    public void Find_32x32SpaceSavingFind() {
        _wordFinder32x32.SetStrategy(new SpaceSavingFindStrategy());
        _wordFinder32x32.Find(_wordStream);
    }

    [Benchmark]
    public void Find_64x64SpaceSavingFind()
    {
        _wordFinder64x64.SetStrategy(new SpaceSavingFindStrategy());
        _wordFinder64x64.Find(_wordStream);
    }


    // Uncomment to benchmark internal matrix extraction performance
    /*
    [Benchmark]
    public void ExtractAllWords_32x32() => _wordFinder32x32.BenchmarkExtraction();

    [Benchmark]
    public void ExtractAllWords_64x64() => _wordFinder64x64.BenchmarkExtraction();
    */
}

public class Program
{
    /// <summary>
    /// Entry point for benchmark execution
    /// Runs all benchmark methods and generates performance report
    /// </summary>
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<WordFinderBenchmarks>();
    }
}
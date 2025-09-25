using Counter.Strategies;
using System.Collections;
using System.Text;

namespace Counter;

public class WordFinder
{
    private readonly char[,] _charMatrix;
    private readonly int _rows;
    private readonly int _cols;
    private readonly HashSet<string> _matrixWords;
    private IFindStrategy _findStrategy;

    public WordFinder(IEnumerable<string> matrix, IFindStrategy? findStrategy = null)
    {
        ValidateMatrix(matrix);

        _rows = matrix.Count();
        _cols = matrix.First().Length;

        _charMatrix = new char[_rows, _cols];
        InitializeMatrix(matrix);

        _matrixWords = ExtractCompleteWordsFromMatrix();
        _findStrategy = findStrategy ?? new OptimizedFindStrategy();
    }

    public WordFinder(IEnumerable matrix)
        : this(matrix?.Cast<string>() ?? throw new ArgumentException("Matrix cannot be null"),
              new OptimizedFindStrategy())
    {
    }

    public void SetStrategy(IFindStrategy findStrategy)
    {
        _findStrategy = findStrategy ?? throw new ArgumentNullException(nameof(findStrategy));
    }

    public IEnumerable<string> Find(IEnumerable<string> wordStream)
    {
        if (wordStream == null)
            return [];

        return _findStrategy.Find(wordStream, _matrixWords);
    }

    public IEnumerable Find(IEnumerable wordStream)
    {
        if (wordStream == null)
            return Enumerable.Empty<string>();

        return Find(wordStream.Cast<string>());
    }

    private static void ValidateMatrix(IEnumerable<string> matrix)
    {
        if (matrix == null || !matrix.Any())
            throw new ArgumentException("Matrix cannot be null or empty");

        var rows = matrix.Count();
        var cols = matrix.First().Length;

        if (rows > 64 || cols > 64)
            throw new ArgumentException("Matrix dimensions cannot exceed 64x64");

        if (matrix.Any(row => row?.Length != cols))
            throw new ArgumentException("All matrix rows must have the same length");

        if (matrix.Any(row => row == null))
            throw new ArgumentException("Matrix rows cannot be null");
    }

    private void InitializeMatrix(IEnumerable<string> matrix)
    {
        int rowIndex = 0;
        foreach (var row in matrix)
        {
            for (int colIndex = 0; colIndex < row.Length; colIndex++)
            {
                _charMatrix[rowIndex, colIndex] = char.ToUpperInvariant(row[colIndex]);
            }
            rowIndex++;
        }
    }

    private HashSet<string> ExtractCompleteWordsFromMatrix()
    {
        var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        ExtractHorizontalWords(words);

        ExtractVerticalWords(words);

        return words;
    }

    private void ExtractHorizontalWords(HashSet<string> words)
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int start = 0; start < _cols; start++)
            {
                var sb = new StringBuilder();
                for (int j = start; j < _cols; j++)
                {
                    sb.Append(_charMatrix[i, j]);
                    words.Add(sb.ToString()); 
                }
            }
        }
    }

    private void ExtractVerticalWords(HashSet<string> words)
    {
        for (int j = 0; j < _cols; j++)
        {
            // Extraer TODOS los substrings de cada columna
            for (int start = 0; start < _rows; start++)
            {
                var sb = new StringBuilder();
                for (int i = start; i < _rows; i++)
                {
                    sb.Append(_charMatrix[i, j]);
                    words.Add(sb.ToString()); // Agregar cada substring
                }
            }
        }
    }

    public void PrintMatrixWords()
    {
        Console.WriteLine("Palabras encontradas en la matriz:");
        foreach (var word in _matrixWords.OrderBy(w => w))
        {
            Console.WriteLine($"- {word}");
        }
    }
    //NO BORRAR es para Benchmark
    public void BenchmarkExtraction()
    {
        var words = new HashSet<string>();
        ExtractHorizontalWords(words);
        ExtractVerticalWords(words);
    }


}
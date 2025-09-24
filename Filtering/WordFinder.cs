using System.Text;

namespace Counter;

public class WordFinder
{
    private readonly char[,] _charMatrix;
    private readonly int _rows;
    private readonly int _cols;
    private readonly HashSet<string> _wordsInMatrix;

    public WordFinder(IEnumerable<string> matrix)
    {
        if (matrix == null || !matrix.Any())
            throw new ArgumentException("Matrix cannot be null or empty");

        _rows = matrix.Count();
        _cols = matrix.First().Length;

        if (_rows > 64 || _cols > 64)
            throw new ArgumentException("Matrix dimensions cannot exceed 64x64");

        if (matrix.Any(row => row.Length != _cols))
            throw new ArgumentException("All matrix rows must have the same length");

        _charMatrix = new char[_rows, _cols];
        int currentRow = 0;
        foreach (var row in matrix)
        {
            for (int i = 0; i < row.Length; i++)
            {
                _charMatrix[currentRow, i] = row[i];
            }
            currentRow++;
        }

        _wordsInMatrix = ExtractAllWordsFromMatrix();
    }

    public IEnumerable<string> Find(IEnumerable<string> wordStream)
    {
        // TODO: Extract all possible words from matrix (horizontal + vertical)
        // TODO: Use HashSet for O(1) lookups
        // TODO: Count frequency in stream (each word only once per stream)
        // TODO: Return top 10 by frequency

        if (wordStream == null)
            throw new ArgumentException("Wordstream cannot be null", nameof(wordStream));

        if (!wordStream.Any())
            return Enumerable.Empty<string>();

        var foundWords = _wordsInMatrix.Intersect(wordStream);
        if (!foundWords.Any())
            return Enumerable.Empty<string>();

        var frequency = new Dictionary<string, int>();
        foreach (var word in foundWords)
        {
            frequency[word] = 1;
        }

        return frequency.OrderByDescending(kvp => kvp.Value)
                        .Take(10)
                        .Select(kvp => kvp.Key);
    }

    private HashSet<string> ExtractAllWordsFromMatrix()
    {
        var words = new HashSet<string>();
        ExtractHorizontalWords(words);
        ExtractVerticalWords(words);
        return words;
    }

    private void ExtractHorizontalWords(HashSet<string> words)
    {
        for (int i = 0; i < _rows; i++)
        {
            var sb = new StringBuilder();
            for (int j = 0; j < _cols; j++)
            {
                sb.Append(_charMatrix[i, j]);
            }
            words.Add(sb.ToString());
        }
    }

    private void ExtractVerticalWords(HashSet<string> words)
    {
        for (int j = 0; j < _cols; j++)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _rows; i++)
            {
                sb.Append(_charMatrix[i, j]);
            }
            words.Add(sb.ToString());
        }
    }

    public void BenchmarkExtraction()
    {
        var words = new HashSet<string>();
        ExtractHorizontalWords(words);   
        ExtractVerticalWords(words);    
    }
}
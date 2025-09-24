namespace Counter;

public class WordFinder
{
    private readonly IEnumerable<string> _matrix;

    public WordFinder(IEnumerable<string> matrix)
    {
        // TODO: Store preprocessed data for efficient searching
        //TODO : Validate matrix (not null, not empty, rectangular)
        //TODO: Max size constraints (e.g., max 64x64)

        if (matrix == null)
            throw new ArgumentException("Matrix cannot be null");

        if (!matrix.Any())
            throw new ArgumentException("Matrix cannot be empty");
        // Validar que todas las filas tengan la misma longitud
        var firstRowLength = matrix.First().Length;
        if (matrix.Any(row => row.Length != firstRowLength))
            throw new ArgumentException("All matrix rows must have the same length");

        _matrix = matrix;
    }


    public IEnumerable<string> Find(IEnumerable<string> wordStream)
    {
        // TODO: Validate wordStream (not null)
        // TODO: Handle empty wordStream → return empty
        // TODO: Extract all possible words from matrix (horizontal + vertical)
        // TODO: Use HashSet for O(1) lookups
        // TODO: Count frequency in stream (each word only once per stream)
        // TODO: Return top 10 by frequency

        if (wordStream == null)
            throw new ArgumentException("Wordstream cannot be null", nameof(wordStream));


        return Enumerable.Empty<string>();



    }

    // TODO: Helper methods for preprocessing matrix
    private HashSet<string> ExtractAllWordsFromMatrix()
    {
        // TODO: Extract horizontal words
        // TODO: Extract vertical words  
        // TODO: Return unique set of all possible words
        throw new NotImplementedException();
    }

    // TODO: Performance optimization methods
    private bool IsValidMatrix(IEnumerable<string> matrix)
    {
        // TODO: Validate matrix constraints
        throw new NotImplementedException();
    }
}
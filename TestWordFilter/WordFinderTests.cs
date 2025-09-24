using Counter;
using Xunit;

public class WordFinderTests
{
    // Constructor validations
    [Fact]
    public void Constructor_WithValidMatrix_InitializesSuccessfully()
    {
        // Arrange
        var matrix = new[] { "abcd", "efgh", "ijkl" };

        // Act
        var finder = new WordFinder(matrix);

        // Assert
        Assert.NotNull(finder);
    }

    [Fact]
    public void Constructor_WithNullMatrix_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new WordFinder(null));
    }

    [Fact]
    public void Constructor_WithEmptyMatrix_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new WordFinder(Array.Empty<string>()));
    }

    [Fact]
    public void Constructor_WithIrregularMatrix_ThrowsArgumentException()
    {
        // Arrange
        var matrix = new[] { "abc", "defg" }; // Different lengths

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
    }

    //Test 2: Horizontal word finding
    [Fact]
    public void Find_WordPresentHorizontally_ReturnsWord()
    {
        // Arrange
        var matrix = new[] { "abcd", "efgh", "ijkl" };
        var wordStream = new[] { "abcd", "efgh", "nonexistent" };
        var finder = new WordFinder(matrix);

        // Act
        var result = finder.Find(wordStream);

        // Assert
        Assert.Contains("abcd", result);
        Assert.Contains("efgh", result);
        Assert.DoesNotContain("nonexistent", result);
    }

    //Test 3: Vertical word finding
    [Fact]
    public void Find_WordPresentVertically_ReturnsWord()
    {
        // Arrange
        var matrix = new[] { "abcd", "efgh", "ijkl" };
        var wordStream = new[] { "aei", "bfj", "cgk" };
        var finder = new WordFinder(matrix);

        // Act
        var result = finder.Find(wordStream);

        // Assert
        Assert.Contains("aei", result);
        Assert.Contains("bfj", result);
        Assert.Contains("cgk", result);
    }

    //Test 4: Handle duplicates in stream (count only once)
    [Fact]
    public void Find_DuplicateWordsInStream_CountsEachWordOnce()
    {
        // Arrange
        var matrix = new[] { "abc", "def", "ghi" };
        var wordStream = new[] { "abc", "abc", "def", "def", "def" }; // abc appears 2x, def 3x
        var finder = new WordFinder(matrix);

        // Act
        var result = finder.Find(wordStream).ToList();

        // Assert
        // Both should appear only once in results,
        // but def should come first if we sort by frequency
        Assert.Equal(2, result.Count);
        // The order will depend on implementation (top 10 by frequency)
    }

    //Test 5: Handle duplicates in matrix (find only once)
    [Fact]
    public void Find_WordAppearsMultipleTimesInMatrix_CountsEachWordOnce()
    {
        // Arrange
        var matrix = new[] { "abc", "abc", "def" }; // "abc" appears twice horizontally
        var wordStream = new[] { "abc" };
        var finder = new WordFinder(matrix);

        // Act
        var result = finder.Find(wordStream);

        // Assert
        // Should count "abc" only once regardless of matrix appearances
        var resultList = result.ToList();
        Assert.Single(resultList);
        Assert.Contains("abc", resultList);
    }

    //Test 6: Return max 10 results
    [Fact]
    public void Find_MoreThan10WordsFound_ReturnsOnlyTop10()
    {
        // Arrange
        var matrix = new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k" };
        var wordStream = Enumerable.Range(0, 15).Select(i => ((char)('a' + i)).ToString()).ToArray();
        var finder = new WordFinder(matrix);

        // Act
        var result = finder.Find(wordStream);

        // Assert
        Assert.Equal(10, result.Count());
    }

    //Test 7: Edge cases
    [Fact]
    public void Find_EmptyWordStream_ReturnsEmpty()
    {
        // Arrange
        var matrix = new[] { "abc", "def" };
        var wordStream = Array.Empty<string>();
        var finder = new WordFinder(matrix);

        // Act
        var result = finder.Find(wordStream);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Find_NoWordsFound_ReturnsEmpty()
    {
        // Arrange
        var matrix = new[] { "abc", "def" };
        var wordStream = new[] { "xyz", "nonexistent" };
        var finder = new WordFinder(matrix);

        // Act
        var result = finder.Find(wordStream);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Find_NullWordStream_ThrowsArgumentException()
    {
        // Arrange
        var matrix = new[] { "abc", "def" };
        var finder = new WordFinder(matrix);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => finder.Find(null));
    }

    //Test 8: Performance test for large stream
    [Fact]
    public void Find_LargeWordStream_CompletesInReasonableTime()
    {
        // Arrange
        var matrix = new[] { "abc", "def", "ghi" };
        var largeWordStream = Enumerable.Range(0, 10000).Select(i => i % 2 == 0 ? "abc" : "nonexistent").ToArray();
        var finder = new WordFinder(matrix);

        // Act
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var result = finder.Find(largeWordStream);
        stopwatch.Stop();

        // Assert
        Assert.True(stopwatch.ElapsedMilliseconds < 1000, "Performance test failed - took too long");
        Assert.Contains("abc", result);
    }

    //Test 9: Case sensitivity (assuming case-sensitive)
    [Fact]
    public void Find_CaseSensitiveMatching_RespectsCase()
    {
        // Arrange
        var matrix = new[] { "Abc", "def" };
        var wordStream = new[] { "Abc", "abc" };
        var finder = new WordFinder(matrix);

        // Act
        var result = finder.Find(wordStream);

        // Assert
        Assert.Contains("Abc", result);
        Assert.DoesNotContain("abc", result);
    }

    //Test 10: Word ordering by frequency
    [Fact]
    public void Find_WordsOrderedByFrequencyDescending()
    {
        // Arrange
        var matrix = new[] { "a", "b", "c", "d" };
        var wordStream = new[] { "a", "a", "a", "b", "b", "c", "d" }; // a:3, b:2, c:1, d:1
        var finder = new WordFinder(matrix);

        // Act
        var result = finder.Find(wordStream).ToList();

        // Assert
        Assert.Equal("a", result[0]);
        Assert.Equal("b", result[1]);
        // c and d order might vary since they have same frequency
    }
}
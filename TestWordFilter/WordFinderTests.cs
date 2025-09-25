using Counter;
using Counter.Strategies;
using System.Collections;

namespace TestWordFilter
{
    public class WordFinderTests
    {
        // Constructor validations
        [Fact]
        public void Constructor_WithValidMatrix_InitializesSuccessfully()
        {
            // Arrange
            var matrix = new[] { "abcd", "efgh", "ijkl" };

            // Act
            var finder = new WordFinder((IEnumerable)matrix);

            // Assert
            Assert.NotNull(finder);
        }

        [Fact]
        public void Constructor_WithNullMatrix_ThrowsArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new WordFinder((IEnumerable)null));
        }

        [Fact]
        public void Constructor_WithIrregularMatrix_ThrowsArgumentException()
        {
            // Arrange
            var matrix = new[] { "abc", "defg" }; // Different lengths

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new WordFinder((IEnumerable)matrix));
        }

        //Test 2: Horizontal word finding
        [Fact]
        public void Find_WordPresentHorizontally_ReturnsWord()
        {
            // Arrange
            var matrix = new[] { "abcd", "efgh", "ijkl" };
            var wordStream = new[] { "abcd", "efgh", "nonexistent" };
            var finder = new WordFinder((IEnumerable)matrix);

            // Act
            var result = finder.Find((IEnumerable)wordStream);

            // Assert
            Assert.Contains("abcd", result.Cast<string>());
            Assert.Contains("efgh", result.Cast<string>());
            Assert.DoesNotContain("nonexistent", result.Cast<string>());
        }

        //Test 3: Vertical word finding
        [Fact]
        public void Find_WordPresentVertically_ReturnsWord()
        {
            // Arrange
            var matrix = new[] { "abcd", "efgh", "ijkl" };
            var wordStream = new[] { "aei", "bfj", "cgk" };
            var finder = new WordFinder((IEnumerable)matrix);

            // Act
            var result = finder.Find((IEnumerable)wordStream);

            // Assert
            Assert.Contains("aei", result.Cast<string>());
            Assert.Contains("bfj", result.Cast<string>());
            Assert.Contains("cgk", result.Cast<string>());
        }

        //Test 4: Handle duplicates in stream
        [Fact]
        public void Find_DuplicateWordsInStream_CountsFrequencyCorrectly()
        {
            // Arrange
            var matrix = new[] { "abc", "def", "ghi" };
            var wordStream = new[] { "abc", "abc", "def", "def", "def" }; // abc appears 2x, def 3x
            var finder = new WordFinder((IEnumerable)matrix);

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert
            // 'def' is most frequent, so it should be first.
            Assert.Equal(2, result.Count);
            Assert.Equal("def", result[0]);
            Assert.Equal("abc", result[1]);
        }

        //Test 5: Handle duplicates in matrix (find only once)
        [Fact]
        public void Find_WordAppearsMultipleTimesInMatrix_CountsEachWordOnce()
        {
            // Arrange
            var matrix = new[] { "abc", "abc", "def" }; // "abc" appears twice horizontally
            var wordStream = new[] { "abc" };
            var finder = new WordFinder((IEnumerable)matrix);

            // Act
            var result = finder.Find((IEnumerable)wordStream);

            // Assert
            // Should count "abc" only once regardless of matrix appearances
            var resultList = result.Cast<string>().ToList();
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
            var finder = new WordFinder((IEnumerable)matrix);

            // Act
            var result = finder.Find((IEnumerable)wordStream);

            // Assert
            Assert.Equal(10, result.Cast<string>().Count());
        }

        

        [Fact]
        public void Find_NoWordsFound_ReturnsEmpty()
        {
            // Arrange
            var matrix = new[] { "abc", "def" };
            var wordStream = new[] { "xyz", "nonexistent" };
            var finder = new WordFinder((IEnumerable)matrix);

            // Act
            var result = finder.Find((IEnumerable)wordStream);

            // Assert
            Assert.Empty(result.Cast<string>());
        }

        //Test 8: Performance test for large stream
        [Fact]
        public void Find_LargeWordStream_CompletesInReasonableTime()
        {
            // Arrange
            var matrix = new[] { "abc", "def", "ghi" };
            var largeWordStream = Enumerable.Range(0, 10000).Select(i => i % 2 == 0 ? "abc" : "nonexistent").ToArray();
            var finder = new WordFinder((IEnumerable)matrix);

            // Act
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var result = finder.Find((IEnumerable)largeWordStream);
            stopwatch.Stop();

            // Assert
            Assert.True(stopwatch.ElapsedMilliseconds < 1000, "Performance test failed - took too long");
            Assert.Contains("abc", result.Cast<string>());
        }

        //Test 9: Case sensitivity
        [Fact]
        public void Find_CaseInsensitiveMatching_WorksAsExpected()
        {
            // Arrange
            var matrix = new[] { "Abc", "def" };
            var wordStream = new[] { "Abc", "abc", "dEf" };
            var finder = new WordFinder((IEnumerable)matrix);

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert
            Assert.Contains("Abc", result, StringComparer.OrdinalIgnoreCase);
            Assert.Contains("dEf", result, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(2, result.Count);
        }

        //Test 10: Word ordering by frequency
        [Fact]
        public void Find_WordsOrderedByFrequencyDescending()
        {
            // Arrange
            var matrix = new[] { "c", "b", "a", "d" }; // Reordered to break coincidental pass
            var wordStream = new[] { "a", "a", "a", "b", "b", "c", "d" }; // a:3, b:2, c:1, d:1
            var finder = new WordFinder((IEnumerable)matrix);

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert
            Assert.Equal(4, result.Count); // Ensure all words are found
            Assert.Equal("a", result[0]); // 'a' should be first (most frequent)
            Assert.Equal("b", result[1]); // 'b' should be second
                                          // c and d order can vary, so just check for presence
            Assert.Contains("c", result.GetRange(2, 2));
            Assert.Contains("d", result.GetRange(2, 2));
        }

        // Test for strategy setting
        [Fact]
        public void WordFinder_CanChangeStrategy()
        {
            // Arrange
            var matrix = new[] { "a", "b" };
            var wordStream = new[] { "a", "a", "b" };
            var finder = new WordFinder(matrix, new SimpleFindStrategy()); // Start with simple

            // Act
            var result1 = finder.Find(wordStream).ToList();

            finder.SetStrategy(new OptimizedFindStrategy());
            var result2 = finder.Find(wordStream).ToList();

            // Assert
            Assert.Equal("a", result1[0]);
            Assert.Equal("a", result2[0]);
            Assert.Equal(result1, result2);
        }

        [Fact]
        public void Find_ExampleFromReadme_ReturnsCorrectResult()
        {
            // Arrange
            var matrix = new[] { "cold", "wind", "hotx" };
            var wordStream = new[] { "cold", "cold", "wind", "hot", "cold", "heat" };
            var finder = new WordFinder((IEnumerable)matrix);

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("cold", result[0]);
            Assert.Contains("wind", result);
            Assert.Contains("hot", result);
            Assert.DoesNotContain("heat", result);
        }

        [Fact]
        public void SimpleFind_ExampleFromReadme_ReturnsCorrectResult()
        {
            // Arrange
            var matrix = new[] { "Cold", "wind", "hotx" };
            var wordStream = new[] { "cold", "cold", "wind", "hot", "cold", "heat" };
            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());
            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("cold", result[0]);
            Assert.Contains("wind", result);
            Assert.Contains("hot", result);
            Assert.DoesNotContain("heat", result);
        }

    }
}
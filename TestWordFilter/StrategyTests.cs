using Counter;

namespace TestWordFilter
{
    public class WordFinderTests
    {
        [Fact]
        public void Constructor_ValidMatrix_InitializesCorrectly()
        {
            // Arrange
            var matrix = new[] { "cold", "wind", "heat" };

            // Act
            var wordFinder = new WordFinder(matrix);

            // Assert
            Assert.NotNull(wordFinder);
        }

        [Fact]
        public void Find_WordsInMatrixExistInWordStream_ReturnsWords()
        {
            // Arrange
            var matrix = new[] { "cold", "wind", "heat" };
            var wordStream = new[] { "cold", "cold", "wind", "hot", "cold", "heat" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.Contains("cold", result);
            Assert.Contains("wind", result);
        }

        [Fact]
        public void Find_EmptyWordStream_ReturnsEmpty()
        {
            // Arrange
            var matrix = new[] { "cold", "wind", "heat" };
            var wordStream = new string[0];
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Find_NullWordStream_ReturnsEmpty()
        {
            // Arrange
            var matrix = new[] { "cold", "wind", "heat" };
            var wordStream = (IEnumerable<string>)null;
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.Empty(result);
        }

    }
}
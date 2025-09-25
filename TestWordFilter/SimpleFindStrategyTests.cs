using Counter;
using Counter.Strategies;
using System.Collections;

namespace TestWordFilter
{
    public class SimpleFindStrategyTests
    {
        [Fact]
        public void ExampleFromRequirements_ColdWindHot()
        {
            // Arrange: Ejemplo exacto de los requisitos
            var matrix = new[]
            {
                "cold",
                "wind",
                "hotx"
            };

            var wordStream = new[] { "cold", "cold", "wind", "hot", "cold", "heat" };

            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert: Según requisitos:
            // - "heat" no existe en matriz → excluida
            // - "cold" aparece 3 veces en stream pero cuenta como 1 (porque existe)
            // - "wind" aparece 1 vez en stream → cuenta como 1 (porque existe)
            // - "hot" aparece 1 vez en stream → cuenta como 1 (porque existe)
            // Todas tienen frecuencia 1 (porque duplicados no cuentan), orden alfabético
            Assert.Equal(3, result.Count);

            // Como todas tienen frecuencia 1, el orden podría ser alfabético
            // o dependiente de la implementación
            Assert.Contains("cold", result);
            Assert.Contains("wind", result);
            Assert.Contains("hot", result);
        }

        [Fact]
        public void WordsCountedOnlyOncePerStream()
        {
            // Arrange: Matriz 3x3
            var matrix = new[] { "abc", "def", "ghi" };

            // "abc" aparece 100 veces pero solo cuenta como 1 (porque existe)
            // "def" aparece 1 vez → cuenta como 1 (porque existe)
            // "xyz" aparece 50 veces → cuenta como 0 (no existe)
            var wordStream = Enumerable.Repeat("abc", 100)
                .Concat(new[] { "def" })
                .Concat(Enumerable.Repeat("xyz", 50))
                .ToArray();

            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert: Ambas palabras tienen frecuencia 1
            Assert.Equal(2, result.Count);
            Assert.Contains("abc", result);
            Assert.Contains("def", result);
        }

        [Fact]
        public void Top10MostRepeatedWords()
        {
            // Arrange: Matriz con palabras de la A a la J
            var matrix = new[]
            {
                "abcdefghij",  // horizontal: a, b, c, d, e, f, g, h, i, j
                "xxxxxxxxxx",
                "xxxxxxxxxx"
            };

            // Stream: 'a' aparece 15 veces, 'b' 14, ... 'j' 6 veces
            var wordStream = new List<string>();
            for (int i = 0; i < 15; i++) wordStream.Add("a");
            for (int i = 0; i < 14; i++) wordStream.Add("b");
            for (int i = 0; i < 13; i++) wordStream.Add("c");
            for (int i = 0; i < 12; i++) wordStream.Add("d");
            for (int i = 0; i < 11; i++) wordStream.Add("e");
            for (int i = 0; i < 10; i++) wordStream.Add("f");
            for (int i = 0; i < 9; i++) wordStream.Add("g");
            for (int i = 0; i < 8; i++) wordStream.Add("h");
            for (int i = 0; i < 7; i++) wordStream.Add("i");
            for (int i = 0; i < 6; i++) wordStream.Add("j");
            for (int i = 0; i < 5; i++) wordStream.Add("k"); // No existe en matriz

            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert: Debería devolver top 10 (a-j), todas con frecuencia 1
            // Ordenadas por frecuencia original (a:15, b:14, ..., j:6)
            Assert.Equal(10, result.Count);
            Assert.Equal("a", result[0]); // Mayor frecuencia en stream
            Assert.Equal("b", result[1]);
            Assert.Equal("j", result[9]);  // Menor frecuencia de las top 10
        }

        [Fact]
        public void WordsNotFoundInMatrix_ExcludedFromResults()
        {
            // Arrange: Matriz solo con "found"
            var matrix = new[] { "found" };

            var wordStream = new[] { "found", "notfound", "found", "notfound", "found" };

            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert: Solo "found" existe en matriz
            Assert.Single(result);
            Assert.Equal("found", result[0]);
        }

        [Fact]
        public void EmptyMatrix_ReturnsEmptySet()
        {
            // Arrange: Matriz vacía
            var matrix = new string[0];
            var wordStream = new[] { "word1", "word2", "word3" };

            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert: Ninguna palabra puede encontrarse en matriz vacía
            Assert.Empty(result);
        }

        [Fact]
        public void EmptyWordStream_ReturnsEmptySet()
        {
            // Arrange: Stream vacío
            var matrix = new[] { "word" };
            var wordStream = new string[0];

            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert: No hay palabras para buscar
            Assert.Empty(result);
        }

        [Fact]
        public void MaximumSizeMatrix_64x64()
        {
            // Arrange: Matriz máxima permitida
            var matrix = GenerateSquareMatrix(64, "test");
            var wordStream = new[] { "test", "notfound" };

            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert: Solo "test" existe
            Assert.Single(result);
            Assert.Equal("test", result[0]);
        }

        [Fact]
        public void WordsInHorizontalAndVertical()
        {
            // Arrange: "word" aparece horizontal y verticalmente
            var matrix = new[]
            {
                "word",  // horizontal
                "oxxx",
                "rxxx",
                "dxxx"
            };

            var wordStream = new[] { "word", "word", "word" };

            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert: "word" existe (aunque aparezca múltiples veces en matriz, solo cuenta 1 en stream)
            Assert.Single(result);
            Assert.Equal("word", result[0]);
        }

        [Fact]
        public void DuplicateWordsInStream_CountedOnce()
        {
            // Arrange
            var matrix = new[] { "hello" };

            // "hello" aparece 100 veces en stream pero solo cuenta como 1
            var wordStream = Enumerable.Repeat("hello", 100).ToArray();

            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("hello", result[0]);
        }

        [Fact]
        public void MoreThan10Words_ReturnsTop10()
        {
            // Arrange: Matriz con palabras 1-15
            var matrix = new[] { "123456789101112131415" };

            var wordStream = new List<string>();
            for (int i = 1; i <= 15; i++)
            {
                // La palabra i aparece i veces (la 15 aparece 15 veces, etc.)
                for (int j = 0; j < i; j++)
                {
                    wordStream.Add(i.ToString());
                }
            }

            var finder = new WordFinder((IEnumerable)matrix);
            finder.SetStrategy(new SimpleFindStrategy());

            // Act
            var result = finder.Find((IEnumerable)wordStream).Cast<string>().ToList();

            // Assert: Top 10 por frecuencia (15, 14, 13, ..., 6)
            Assert.Equal(10, result.Count);
            Assert.Equal("15", result[0]); // Mayor frecuencia
            Assert.Equal("14", result[1]);
            Assert.Equal("6", result[9]);  // Menor frecuencia de las top 10
        }

        private string[] GenerateSquareMatrix(int size, string wordToInclude)
        {
            var matrix = new string[size];
            for (int i = 0; i < size; i++)
            {
                var chars = new char[size];
                for (int j = 0; j < size; j++)
                {
                    chars[j] = 'x';
                }
                matrix[i] = new string(chars);
            }

            // Insertar palabra en primera fila si cabe
            if (size > 0 && wordToInclude.Length <= size)
            {
                var firstRow = matrix[0].ToCharArray();
                for (int j = 0; j < wordToInclude.Length; j++)
                {
                    firstRow[j] = wordToInclude[j];
                }
                matrix[0] = new string(firstRow);
            }

            return matrix;
        }
    }
}
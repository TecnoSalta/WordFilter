using Counter;
using System;
using System.Linq;
using Xunit;

namespace TestWordFilter;

public class StrategyTests
{
    [Theory]
    [InlineData(typeof(SimpleFindStrategy))]
    [InlineData(typeof(SpaceSavingFindStrategy))]
    [InlineData(typeof(OptimizedFindStrategy))]
    public void Find_WordsOrderedByFrequencyDescending(Type strategyType)
    {
        // Arrange
        var strategy = (IFindStrategy)Activator.CreateInstance(strategyType);

        // Act
        var result = strategy.Find(TestData.WordStreamFrequency, TestData.WordsInMatrixForFrequencyTest).ToList();

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Equal("a", result[0]);
        Assert.Equal("b", result[1]);
        Assert.Contains("c", result.GetRange(2, 2));
        Assert.Contains("d", result.GetRange(2, 2));
    }

    [Theory]
    [InlineData(typeof(SimpleFindStrategy))]
    [InlineData(typeof(SpaceSavingFindStrategy))]
    [InlineData(typeof(OptimizedFindStrategy))]
    public void Find_EmptyStream_ReturnsEmpty(Type strategyType)
    {
        // Arrange
        var strategy = (IFindStrategy)Activator.CreateInstance(strategyType);
        var wordStream = Array.Empty<string>();

        // Act
        var result = strategy.Find(wordStream, TestData.WordsInMatrix);

        // Assert
        Assert.Empty(result);
    }
    
    [Theory]
    [InlineData(typeof(SimpleFindStrategy))]
    [InlineData(typeof(SpaceSavingFindStrategy))]
    [InlineData(typeof(OptimizedFindStrategy))]
    public void Find_NullStream_ReturnsEmpty(Type strategyType)
    {
        // Arrange
        var strategy = (IFindStrategy)Activator.CreateInstance(strategyType);

        // Act
        var result = strategy.Find(null, TestData.WordsInMatrix);

        // Assert
        Assert.Empty(result);
    }
    
    [Theory]
    [InlineData(typeof(SimpleFindStrategy))]
    [InlineData(typeof(SpaceSavingFindStrategy))]
    [InlineData(typeof(OptimizedFindStrategy))]
    public void Find_MoreThan10WordsFound_ReturnsOnlyTop10(Type strategyType)
    {
        // Arrange
        var strategy = (IFindStrategy)Activator.CreateInstance(strategyType);
        var wordStream = new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "a", "b", "c", "d", "e" };

        // Act
        var result = strategy.Find(wordStream, TestData.WordsInMatrix).ToList();

        // Assert
        Assert.Equal(10, result.Count);
        Assert.Equal("a", result[0]);
        Assert.Equal("b", result[1]);
        Assert.Equal("c", result[2]);
        Assert.Equal("d", result[3]);
        Assert.Equal("e", result[4]);
    }
}

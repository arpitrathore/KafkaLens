namespace KafkaLens.ViewModels.Tests;

public class OpenedClusterViewModelTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Auto")]
    [InlineData("Unknown")]
    public void NormalizeFormatterName_WhenNullOrWhitespaceOrUnknown_ShouldReturnUnknown(string? formatterName)
    {
        // Arrange
        var allowedNames = new List<string> { "Unknown", "Text", "JSON" };

        // Act
        var result = OpenedClusterViewModel.NormalizeFormatterName(formatterName, allowedNames);

        // Assert
        Assert.Equal("Unknown", result);
    }

    [Fact]
    public void NormalizeFormatterName_WhenAllowed_ShouldReturnSameName()
    {
        // Arrange
        var allowedNames = new List<string> { "Unknown", "Text", "JSON" };

        // Act
        var result = OpenedClusterViewModel.NormalizeFormatterName("JSON", allowedNames);

        // Assert
        Assert.Equal("JSON", result);
    }

    [Fact]
    public void NormalizeFormatterName_WhenNotAllowed_ShouldReturnAuto()
    {
        // Arrange
        var allowedNames = new List<string> { "Unknown", "Text", "JSON" };

        // Act
        var result = OpenedClusterViewModel.NormalizeFormatterName("XML", allowedNames);

        // Assert
        Assert.Equal("Unknown", result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Auto")]
    [InlineData("Unknown")]
    public void CanApplyFormatterToLoadedMessages_WhenNullOrWhitespaceOrUnknown_ShouldReturnFalse(string? formatterName)
    {
        // Arrange
        var allowedNames = new List<string> { "Unknown", "Text", "JSON" };

        // Act
        var result = OpenedClusterViewModel.CanApplyFormatterToLoadedMessages(formatterName, allowedNames);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanApplyFormatterToLoadedMessages_WhenValidAndAllowed_ShouldReturnTrue()
    {
        // Arrange
        var allowedNames = new List<string> { "Unknown", "Text", "JSON" };

        // Act
        var result = OpenedClusterViewModel.CanApplyFormatterToLoadedMessages("JSON", allowedNames);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanApplyFormatterToLoadedMessages_WhenValidButNotAllowed_ShouldReturnFalse()
    {
        // Arrange
        var allowedNames = new List<string> { "Unknown", "Text", "JSON" };

        // Act
        var result = OpenedClusterViewModel.CanApplyFormatterToLoadedMessages("XML", allowedNames);

        // Assert
        Assert.False(result);
    }
}

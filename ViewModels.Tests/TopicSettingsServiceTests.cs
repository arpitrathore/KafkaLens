using System.IO;

namespace KafkaLens.ViewModels.Tests;

public class TopicSettingsServiceTests : IDisposable
{
    private readonly string tempFilePath = Path.Combine(Path.GetTempPath(), $"topic_settings_test_{Guid.NewGuid()}.json");

    public void Dispose()
    {
        if (File.Exists(tempFilePath))
        {
            File.Delete(tempFilePath);
        }
    }

    [Fact]
    public void GetSettings_WhenNoSettingsExist_ShouldReturnDefaults()
    {
        // Arrange
        var service = new TopicSettingsService(tempFilePath);

        // Act
        var settings = service.GetSettings("cluster-1", "topic-1");

        // Assert
        Assert.Null(settings.KeyFormatter);
        Assert.Null(settings.ValueFormatter);
    }

    [Fact]
    public void SetSettings_ShouldPersistAndRetrieve()
    {
        // Arrange
        var service = new TopicSettingsService(tempFilePath);
        var settings = new TopicSettings
        {
            KeyFormatter = "Text",
            ValueFormatter = "JSON"
        };

        // Act
        service.SetSettings("cluster-1", "topic-1", settings);
        var retrieved = service.GetSettings("cluster-1", "topic-1");

        // Assert
        Assert.Equal("Text", retrieved.KeyFormatter);
        Assert.Equal("JSON", retrieved.ValueFormatter);
    }

    [Fact]
    public void SetSettings_WithApplyToAllClusters_ShouldApplyGlobally()
    {
        // Arrange
        var service = new TopicSettingsService(tempFilePath);
        var settings = new TopicSettings
        {
            KeyFormatter = "Text",
            ValueFormatter = "JSON"
        };

        // Act
        service.SetSettings("cluster-1", "topic-1", settings, applyToAllClusters: true);
        var retrievedFromOtherCluster = service.GetSettings("cluster-2", "topic-1");

        // Assert
        Assert.Equal("Text", retrievedFromOtherCluster.KeyFormatter);
        Assert.Equal("JSON", retrievedFromOtherCluster.ValueFormatter);
    }

    [Fact]
    public void SetSettings_ClusterSpecific_ShouldOverrideGlobal()
    {
        // Arrange
        var service = new TopicSettingsService(tempFilePath);
        var globalSettings = new TopicSettings { KeyFormatter = "Text", ValueFormatter = "JSON" };
        var clusterSettings = new TopicSettings { KeyFormatter = "Int32", ValueFormatter = "Text" };

        // Act
        service.SetSettings("cluster-1", "topic-1", globalSettings, applyToAllClusters: true);
        service.SetSettings("cluster-2", "topic-1", clusterSettings);

        // Assert
        var fromCluster2 = service.GetSettings("cluster-2", "topic-1");
        Assert.Equal("Int32", fromCluster2.KeyFormatter);
        Assert.Equal("Text", fromCluster2.ValueFormatter);

        var fromCluster3 = service.GetSettings("cluster-3", "topic-1");
        Assert.Equal("Text", fromCluster3.KeyFormatter);
        Assert.Equal("JSON", fromCluster3.ValueFormatter);
    }

    [Fact]
    public void SetSettings_ShouldPersistToFile()
    {
        // Arrange
        var settings = new TopicSettings { KeyFormatter = "Text", ValueFormatter = "JSON" };

        // Act
        var service1 = new TopicSettingsService(tempFilePath);
        service1.SetSettings("cluster-1", "topic-1", settings);

        var service2 = new TopicSettingsService(tempFilePath);
        var retrieved = service2.GetSettings("cluster-1", "topic-1");

        // Assert
        Assert.Equal("Text", retrieved.KeyFormatter);
        Assert.Equal("JSON", retrieved.ValueFormatter);
    }

    [Fact]
    public void SetSettings_WhenUnknown_ShouldNotPersistTopicEntry()
    {
        // Arrange
        var service = new TopicSettingsService(tempFilePath);
        var unknown = new TopicSettings
        {
            KeyFormatter = "Unknown",
            ValueFormatter = "Unknown"
        };

        // Act
        service.SetSettings("cluster-1", "topic-1", unknown);
        var retrieved = service.GetSettings("cluster-1", "topic-1");
        var json = File.ReadAllText(tempFilePath);

        // Assert
        Assert.Null(retrieved.KeyFormatter);
        Assert.Null(retrieved.ValueFormatter);
        Assert.DoesNotContain("topic-1", json);
    }

    [Fact]
    public void GetSettings_ReturnsCopy_NotReference()
    {
        // Arrange
        var service = new TopicSettingsService(tempFilePath);
        var settings = new TopicSettings { KeyFormatter = "Text", ValueFormatter = "JSON" };
        service.SetSettings("cluster-1", "topic-1", settings);

        // Act
        var retrieved1 = service.GetSettings("cluster-1", "topic-1");
        retrieved1.KeyFormatter = "Modified";
        var retrieved2 = service.GetSettings("cluster-1", "topic-1");

        // Assert
        Assert.Equal("Text", retrieved2.KeyFormatter);
    }
}

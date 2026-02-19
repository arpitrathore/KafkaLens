using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace KafkaLens.ViewModels;

public class SettingsService : ISettingsService
{
    private readonly string filePath;
    private JObject settings = new();

    public SettingsService(string filePath)
    {
        this.filePath = filePath;
        Load();
    }

    private void Load()
    {
        if (File.Exists(filePath))
        {
            try
            {
                var json = File.ReadAllText(filePath);
                settings = JsonConvert.DeserializeObject<JObject>(json) ?? new JObject();
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Failed to load settings from {FilePath}", filePath);
                settings = new JObject();
            }
        }
    }

    private void Save()
    {
        try
        {
            var json = settings.ToString(Newtonsoft.Json.Formatting.Indented);
            var directory = Path.GetDirectoryName(filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex, "Failed to save settings to {FilePath}", filePath);
        }
    }

    public string? GetValue(string key)
    {
        if (!settings.TryGetValue(key, out var value))
        {
            return null;
        }

        return value.Type switch
        {
            JTokenType.String => value.Value<string>(),
            JTokenType.Array => string.Join(",", value.Values<string>().Where(v => !string.IsNullOrWhiteSpace(v))),
            JTokenType.Null => null,
            _ => value.ToString()
        };
    }

    public void SetValue(string key, string value)
    {
        settings[key] = JValue.CreateString(value);
        Save();
    }
}

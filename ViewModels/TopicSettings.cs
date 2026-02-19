namespace KafkaLens.ViewModels;

public class TopicSettings
{
    [Newtonsoft.Json.JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string? KeyFormatter { get; set; }

    [Newtonsoft.Json.JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string? ValueFormatter { get; set; }
}

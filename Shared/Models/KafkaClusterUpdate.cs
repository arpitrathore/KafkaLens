namespace KafkaLens.Shared.Models;

public class KafkaClusterUpdate(string name, string address)
{
    public string Name { get; set; } = name;
    public string Address { get; set; } = address;
}
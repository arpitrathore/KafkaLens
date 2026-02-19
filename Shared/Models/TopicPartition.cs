namespace KafkaLens.Shared.Models;

public class TopicPartition(string topic, int partition)
{
    public string Topic { get; set; } = topic;

    public int Partition{ get; } = partition;
}
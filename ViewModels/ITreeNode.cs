using System.Collections.ObjectModel;

namespace KafkaLens.ViewModels;

public interface ITreeNode
{
    enum NodeType
    {
        Cluster,
        Topic,
        Partition,
        None
    }
    string Name { get; }
    NodeType Type { get; }
    bool IsExpanded { get; set; }
    bool IsSelected { get; set; }
    ObservableCollection<ITreeNode> Children { get; }
}
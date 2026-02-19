using KafkaLens.Shared.Entities;

namespace KafkaLens.ViewModels;

public partial class ClusterInfoViewModel(ClusterInfo info) : ConnectionViewModelBase
{
    public ClusterInfo Info { get; } = info;

    public string Name => Info.Name;
    public string Address => Info.Address;
    public string Id => Info.Id;
}

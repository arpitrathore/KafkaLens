using CommunityToolkit.Mvvm.ComponentModel;
using KafkaLens.Clients.Entities;

namespace KafkaLens.ViewModels;

public partial class ClientInfoViewModel(ClientInfo info) : ConnectionViewModelBase
{
    public ClientInfo Info { get; private set; } = info;

    [ObservableProperty]
    private string name = info.Name;
    
    [ObservableProperty]
    private string address = info.Address;
    
    public string Id => Info.Id;
    
    [ObservableProperty]
    private string protocol = info.Protocol;
    
    public void UpdateInfo(ClientInfo info)
    {
        Info = info;
        Name = info.Name;
        Address = info.Address;
        Protocol = info.Protocol;
    }
}

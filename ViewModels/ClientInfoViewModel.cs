using CommunityToolkit.Mvvm.ComponentModel;
using KafkaLens.Clients.Entities;

namespace KafkaLens.ViewModels;

public partial class ClientInfoViewModel : ConnectionViewModelBase
{
    public ClientInfo Info { get; private set; }

    public ClientInfoViewModel(ClientInfo info)
    {
        Info = info;
        name = info.Name;
        address = info.Address;
        protocol = info.Protocol;
    }

    [ObservableProperty]
    private string name;
    
    [ObservableProperty]
    private string address;
    
    public string Id => Info.Id;
    
    [ObservableProperty]
    private string protocol;
    
    public void UpdateInfo(ClientInfo info)
    {
        Info = info;
        Name = info.Name;
        Address = info.Address;
        Protocol = info.Protocol;
    }
}

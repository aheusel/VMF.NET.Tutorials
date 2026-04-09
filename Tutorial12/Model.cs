using VMF.NET.Runtime;
using VMF.NET.Runtime.Attributes;

namespace Tutorial12.VmfModel;

[VmfModel]
[VmfEquals]
public partial interface IStore
{
    string? Id { get; set; }

    [Contains("IItem.Store")]
    VList<IItem> Items { get; }
}

[VmfModel]
[VmfEquals]
public partial interface IItem
{
    string? Id { get; set; }

    [Container("IStore.Items")]
    IStore? Store { get; }
}

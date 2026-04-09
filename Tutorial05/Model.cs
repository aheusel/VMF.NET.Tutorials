using VMF.NET.Runtime.Attributes;

namespace Tutorial05.VmfModel;

[VmfModel]
public partial interface INode
{
    string? Name { get; set; }
    int Id { get; set; }
}

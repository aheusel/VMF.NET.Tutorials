using VMF.NET.Runtime.Attributes;

namespace Tutorial02.VmfModel;

[VmfModel]
public partial interface IParent
{
    string? Name { get; set; }
}

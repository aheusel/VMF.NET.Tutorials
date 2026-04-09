using VMF.NET.Runtime.Attributes;

namespace Tutorial01.VmfModel;

[VmfModel]
public partial interface IParent
{
    string? Name { get; set; }
}

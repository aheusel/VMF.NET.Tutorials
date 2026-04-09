using VMF.NET.Runtime.Attributes;

namespace Tutorial07.VmfModel;

[VmfModel]
[Immutable]
public partial interface IImmutableObject
{
    int Value { get; }
}

[VmfModel]
public partial interface IMutableObject
{
    int Value { get; set; }
}

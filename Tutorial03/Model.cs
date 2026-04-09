using VMF.NET.Runtime.Attributes;

namespace Tutorial03.VmfModel;

[VmfModel]
public partial interface IParent
{
    [Contains("IChild.Parent")]
    IChild? Child { get; set; }

    string? Name { get; set; }
}

public partial interface IChild
{
    [Container("IParent.Child")]
    IParent? Parent { get; }

    int Value { get; set; }
}

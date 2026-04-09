using VMF.NET.Runtime.Attributes;

namespace Tutorial11.VmfModel;

[VmfModel]
public partial interface INode
{
    string? Id { get; set; }

    [VmfAnnotation("input", Key = "api")]
    INode? A { get; set; }

    [VmfAnnotation("input", Key = "api")]
    INode? B { get; set; }

    [VmfAnnotation("output", Key = "api")]
    INode? C { get; set; }
}

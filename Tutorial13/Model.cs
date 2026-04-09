using VMF.NET.Runtime.Attributes;

namespace Tutorial13.VmfModel;

[VmfModel]
[VmfAnnotation("info about type A", Key = "my-key")]
public partial interface IA
{
    string? Name { get; set; }
    IA? Child { get; set; }
}

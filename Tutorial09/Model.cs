using VMF.NET.Runtime.Attributes;

namespace Tutorial09.VmfModel;

[VmfModel]
public partial interface IObjectWithDefaultValues
{
    [VmfDefaultValue("23")]
    int Value { get; set; }

    [VmfDefaultValue("\"my name\"")]
    string? Name { get; set; }
}

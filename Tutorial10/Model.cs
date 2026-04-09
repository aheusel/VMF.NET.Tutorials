using VMF.NET.Runtime.Attributes;

namespace Tutorial10.VmfModel;

[VmfModel]
[VmfEquals]
public partial interface IObjectToCompare
{
    string? Id { get; set; }
    int Data { get; set; }
}

[VmfModel]
[VmfEquals]
public partial interface IObjectToCompareId
{
    string? Id { get; set; }

    [IgnoreEquals]
    int Data { get; set; }
}

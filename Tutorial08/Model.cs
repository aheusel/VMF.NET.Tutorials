using VMF.NET.Runtime.Attributes;

namespace Tutorial08.VmfModel;

[VmfModel]
[DelegateTo(typeof(Tutorial08.CustomBehavior))]
public partial interface IObjectWithCustomBehavior
{
    int A { get; set; }
    int B { get; set; }

    [DelegateTo(typeof(Tutorial08.CustomBehavior))]
    int ComputeSum();
}

using VMF.NET.Runtime.Attributes;

namespace Tutorial06.VmfModel;

[VmfModel]
public partial interface INode
{
    [PropertyOrder(0)]
    string? Name { get; set; }

    [PropertyOrder(1)]
    bool Visible { get; set; }

    [PropertyOrder(4)]
    INode? Child1 { get; set; }

    [PropertyOrder(3)]
    INode? Child2 { get; set; }

    [PropertyOrder(2)]
    INode? Child3 { get; set; }
}

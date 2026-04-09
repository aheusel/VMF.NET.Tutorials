using VMF.NET.Runtime;
using VMF.NET.Runtime.Attributes;

namespace Tutorial04.VmfModel;

[VmfModel]
public partial interface INode
{
    string? Name { get; set; }

    [Contains("INode.Parent")]
    VList<INode> Children { get; }

    [Container("INode.Children")]
    INode? Parent { get; }
}

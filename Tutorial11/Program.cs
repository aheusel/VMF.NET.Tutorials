using VMF.NET.Runtime;
using Tutorial11.VmfModel;

// create nodes
var n = INode.NewBuilder().WithId("node:n").Build();
var a = INode.NewBuilder().WithId("node:a").Build();
var b = INode.NewBuilder().WithId("node:b").Build();
var c = INode.NewBuilder().WithId("node:c").Build();

// set a,b,c
n.A = a;
n.B = b;
n.C = c;

// predicate to filter inputs
bool IsInput(VmfProperty p) =>
    p.AnnotationByKey("api")?.Value == "input";

// predicate to filter outputs
bool IsOutput(VmfProperty p) =>
    p.AnnotationByKey("api")?.Value == "output";

// print inputs & outputs
Console.WriteLine("Inputs and Outputs:");

// query inputs:
foreach (var p in n.Vmf().Reflect().Properties().Where(IsInput))
{
    Console.WriteLine($"-> input  param '{p.Name}' -> node: {((INode)p.Get()!).Id}");
}

// query outputs:
foreach (var p in n.Vmf().Reflect().Properties().Where(IsOutput))
{
    Console.WriteLine($"-> output param '{p.Name}' -> node: {((INode)p.Get()!).Id}");
}

// expected output:
// -> input  param 'A' -> node: node:a
// -> input  param 'B' -> node: node:b
// -> output param 'C' -> node: node:c

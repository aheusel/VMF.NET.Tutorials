# VMF.NET Tutorial 11

[HOME](../README.md) [NEXT ->](../Tutorial12/README.md)

## Annotation Support

### What you will learn

In this tutorial you will learn how to

- annotate models
- use annotations to query elements

### Annotate Models

For the following experiments, we use the model shown below:

```csharp
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
```

The model consists of an `INode` entity with `Id` and three additional node properties `A`, `B`, `C`. These properties are annotated with the `[VmfAnnotation]` attribute. Each annotation consists of a key and a value. The key defines the category of the annotation, while the value contains the annotation information. This can be a simple string, such as `"input"` or `"output"` but could also be more structured information that can be parsed by an annotation analyzer. Annotations allow us to define custom semantics, i.e., we can query an object for logical inputs or outputs (just an example). This might be relevant for a representative visualization of nodes and their relations to other nodes.

First, we create four node instances and set the properties `A`, `B`, `C`:

```csharp
// create nodes
var n = INode.NewBuilder().WithId("node:n").Build();
var a = INode.NewBuilder().WithId("node:a").Build();
var b = INode.NewBuilder().WithId("node:b").Build();
var c = INode.NewBuilder().WithId("node:c").Build();

// set a,b,c
n.A = a;
n.B = b;
n.C = c;
```

### Query Elements by Annotations

As an example we query properties of a node via annotations. To accomplish that, we create the corresponding predicates which form our custom vocabulary we will use in combination with LINQ:

```csharp
// predicate to filter inputs
bool IsInput(VmfProperty p) =>
    p.AnnotationByKey("api")?.Value == "input";

// predicate to filter outputs
bool IsOutput(VmfProperty p) =>
    p.AnnotationByKey("api")?.Value == "output";
```

Now we can easily query inputs and outputs:

```csharp
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
```

The expected output looks like this:

```
-> input  param 'A' -> node: node:a
-> input  param 'B' -> node: node:b
-> output param 'C' -> node: node:c
```

## Conclusion

Congrats, you have successfully annotated properties and used them to query inputs and outputs.

To run the code, use `dotnet run --project Tutorial11`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial12/README.md)

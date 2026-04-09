# VMF.NET Tutorial 5

[HOME](../README.md) [NEXT ->](../Tutorial06/README.md)

## Using the Builder API

### What you will learn

In this tutorial you will learn how to

- create instances with the Builder API
- copy state via the Builder API

### Instantiating Objects

For this tutorial we are going to use the following model:

```csharp
using VMF.NET.Runtime.Attributes;

namespace Tutorial05.VmfModel;

[VmfModel]
public partial interface INode
{
    string? Name { get; set; }
    int Id { get; set; }
}
```

Instead of just instantiating objects via `NewInstance()` we can create a builder, set the desired properties and build the instance:

```csharp
// create a new node instance via builder
var node1 = INode.NewBuilder()
    .WithName("my node") // set the name
    .WithId(3)           // set the id
    .Build();
```

This is a convenient and compact way to declare properties before the actual instantiation. But there are even more applications of the builder API. It can apply state from one instance to another:

```csharp
// create a second node via NewInstance
var node2 = INode.NewInstance();

// use the builder to apply state from node1 to node2
INode.NewBuilder().ApplyFrom(node1).ApplyTo(node2);

// check whether properties have been applied correctly
Console.WriteLine($"> node1.Name == node2.Name: {node1.Name == node2.Name}");
Console.WriteLine($"> node1.Id == node2.Id:     {node1.Id == node2.Id}");
```

## Conclusion

Congrats, you have successfully used the Builder API to instantiate your domain objects.

To run the code, use `dotnet run --project Tutorial05`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial06/README.md)

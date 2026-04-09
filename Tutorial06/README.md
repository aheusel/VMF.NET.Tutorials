# VMF.NET Tutorial 6

[HOME](../README.md) [NEXT ->](../Tutorial07/README.md)

## Object Graph Traversal & Custom Property Order

### What you will learn

In this tutorial you will learn how to

- traverse object graphs via streams
- declare a property order that defines the order in which properties appear in the stream

### Declaring the Property Order

The model we are going to use consists of an `INode` entity with five properties. With the `[PropertyOrder(0)]` attribute we can define the index of each property, i.e., the order in which properties are processed. Here's the code of the full model:

```csharp
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
```

### Setting up the Object Graph

To set up the object graph, we create a root node and three children which we assign after creating all four instances:

```csharp
// create a new Node instance (this is our root)
var root = INode.NewBuilder().WithName("root").WithVisible(true).Build();

// create three children
var child1 = INode.NewBuilder().WithName("child 1").WithVisible(true).Build();
var child2 = INode.NewBuilder().WithName("child 2").WithVisible(true).Build();
var child3 = INode.NewBuilder().WithName("child 3").WithVisible(true).Build();

// and add them to the root node
root.Child1 = child1;
root.Child2 = child2;
root.Child3 = child3;
```

Now we can traverse the object graph. As with all VMF related functionality, we do this by using the `Vmf()` API. More specifically, we obtain a stream via `root.Vmf().Content().Stream<INode>()`. Printing the traversed node names can be done via:

```csharp
root.Vmf().Content().Stream<INode>().ToList().ForEach(
    node => Console.WriteLine($"-> node: {node.Name}")
);
```

Usually, the expected output would be:

```
-> node: root
-> node: child 1
-> node: child 2
-> node: child 3
```

But with the custom property order, we get the following result:

```
-> node: root
-> node: child 3
-> node: child 2
-> node: child 1
```

Note that only model-type properties are visited. That is why the `Name` and `Visible` properties are omitted. The method `node.Vmf().Reflect().Properties()` gives access to the complete list of properties of a node object. The list is ordered, i.e., properties will be in the same order as specified via the `[PropertyOrder]` attribute.

```csharp
// if we want to access all properties of a Node object:
Console.WriteLine("\nProperties of root: ");
foreach (var p in root.Vmf().Reflect().Properties())
{
    Console.WriteLine($"-> prop: {p.Name}");
}
```

### Filter Nodes

We can easily declare nodes as invisible and use LINQ's `Where()` to filter them during the object graph traversal:

```csharp
// child 2 should be invisible
child2.Visible = false;

// we use the simple predicate Where(n => n.Visible) to filter invisible instances
root.Vmf().Content().Stream<INode>()
    .Where(n => n.Visible)
    .ToList()
    .ForEach(node => Console.WriteLine($"-> node: {node.Name}"));
```

This should give us the following output:

```
-> node: root
-> node: child 3
-> node: child 1
```

## Conclusion

Congrats, you have successfully declared a custom property order and used streams to traverse object graphs.

To run the code, use `dotnet run --project Tutorial06`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial07/README.md)

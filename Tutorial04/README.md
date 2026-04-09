# VMF.NET Tutorial 4

[HOME](../README.md) [NEXT ->](../Tutorial05/README.md)

## Undo/Redo

### What you will learn

In this tutorial you will learn how to

- clone an object graph
- use the undo/redo API

### The Model

We use a model that declares a simple tree structure:

```csharp
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
```

### The Code

First we create an `INode` instance and enable change recording for using the undo/redo API:

```csharp
// create a new root instance
var root = INode.NewInstance();

// start change recorder for undo
root.Vmf().Changes().Start();
```

As always, we can add a change listener:

```csharp
// register change listener
root.Vmf().Changes().AddListener(evt =>
{
    Console.WriteLine($"evt: {evt.PropertyName}");

    if (evt.PropertyChange is { } pc)
    {
        Console.WriteLine($"  -> oldValue: {pc.OldValue}");
        Console.WriteLine($"  -> newValue: {pc.NewValue}");
    }
    else if (evt.ListChange is { } lc)
    {
        Console.WriteLine($"  -> {lc}");
    }
});
```

Now we add a child node and introduce some changes:

```csharp
// cause a change by setting the name of root
root.Name = "#1";

// create a new child
var child1 = INode.NewInstance();

// add the child to the root
root.Children.Add(child1);

// cause a change by setting the name property of child 1
child1.Name = "#2";
```

Okay, now it's time to clone the `root` instance so we can check later whether undoing changes has an effect:

```csharp
// create a deep clone of root
var rootClone = root.Vmf().Content().DeepCopy<INode>();
```

We can check whether cloning was successful. `root` and `rootClone` should be equal but not identical:

```csharp
// ensure that rootClone and root are content-equal ...
Console.WriteLine($"root eq rootClone: {root.Vmf().Content().ContentEquals(rootClone)}");

// ... but not identical
Console.WriteLine($"root != rootClone: {!ReferenceEquals(root, rootClone)}");

// use automatically generated ToString() method
Console.WriteLine($" -> root:      {root}");
Console.WriteLine($" -> rootClone: {rootClone}");
```

To check how many changes were recorded, we just access the collection that holds all changes:

```csharp
// show number of changes
Console.WriteLine($"#changes: {root.Vmf().Changes().All().Count}\n");
```

To undo the changes we just have to reverse the order of the change list:

```csharp
// invert change order ...
var changesToRevert = root.Vmf().Changes().All().Reverse().ToList();
```

and undo the changes:

```csharp
// ... and undo all changes
foreach (var c in changesToRevert)
{
    Console.WriteLine("-------- undo change: --------");
    c.Undo();
}
```

The change listener will recognize the undo actions as changes as well. So watch the output for those changes.

The `root` object should be empty. The `rootClone`, however, should contain all changes. Here's how to check that:

```csharp
// after undo we compare the clone and the empty root (they are not equal)
// we expect the root to be empty (all changes are reverted)
Console.WriteLine("--------");
Console.WriteLine($"root eq clone: {root.Vmf().Content().ContentEquals(rootClone)}");
Console.WriteLine($" -> root:      {root}");
Console.WriteLine($" -> rootClone: {rootClone}");
```

## Conclusion

Congrats, you have successfully used the VMF.NET undo/redo API.

To run the code, use `dotnet run --project Tutorial04`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial05/README.md)

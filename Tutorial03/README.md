# VMF.NET Tutorial 3

[HOME](../README.md) [NEXT ->](../Tutorial03b/README.md)

## Containment References

### What you will learn

In this tutorial you will learn how to

- declare containment references

### Declaring Containment References

For this tutorial we use an updated version of the model definition. Instead of just a single interface, we use an `IParent` and an `IChild` interface and declare a containment reference.

```csharp
using VMF.NET.Runtime.Attributes;

namespace Tutorial03.VmfModel;

[VmfModel]
public partial interface IParent
{
    [Contains("IChild.Parent")]
    IChild? Child { get; set; }

    string? Name { get; set; }
}

public partial interface IChild
{
    [Container("IParent.Child")]
    IParent? Parent { get; }

    int Value { get; set; }
}
```

In the above code sample the `IParent` interface declares that it contains an `IChild` object. Thus, we use the `[Contains("IChild.Parent")]` attribute and declare the `Parent` property of the `IChild` interface as the opposite. In the `IChild` interface we use the `[Container("IParent.Child")]` attribute to indicate that the containing property in `IParent` is the `Child` property.

Now let's instantiate an `IParent`, an `IChild` and add the child to the parent.

```csharp
var parent = IParent.NewInstance();

parent.Name = "Parent 1";

// register change listener
parent.Vmf().Changes().AddListener(evt =>
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

var child1 = IChild.NewInstance();

// add the child to the parent
parent.Child = child1;
```

Now we can check the `Parent` property:

```csharp
// containment references make it possible: the child automatically knows its parent
Console.WriteLine($"my parent: {child1.Parent!.Name}");
```

If we make changes to `child1` we will get notified by the change listener even though we didn't add it to `child1` explicitly.

```csharp
// cause a change by setting the value property of child 1
// child 1 is implicitly observed
child1.Value = 42;
```

Since a containment reference cannot be shared between multiple containers, adding the `child1` instance to another `IParent` should remove `child1` from `parent`. Let's try this:

```csharp
// now we create a second parent
var parent2 = IParent.NewInstance();
parent2.Name = "Parent 2";

// adding child 1 to parent2 has several interesting effects
// 1. child1 is removed from parent1 (check change notification output)
// 2. parent of child1 is now parent2
parent2.Child = child1;
```

If we check the output from the change listener we can clearly see that `parent.Child` has been updated automatically. Instead of `child1` it returns `null` since `child1` belongs to `parent2` now.

`child1.Parent` should now return `Parent 2` as its parent:

```csharp
// containment references make it possible: the child automatically knows its new parent
Console.WriteLine($"my new parent: {child1.Parent!.Name}");
```

## More on Containments

VMF.NET also supports list containments (one to many) which can be expressed as follows:

```csharp
using VMF.NET.Runtime;
using VMF.NET.Runtime.Attributes;

public partial interface IParent
{
    [Contains("IChild.Parent")]
    VList<IChild> Children { get; }
}

public partial interface IChild
{
    [Container("IParent.Children")]
    IParent? Parent { get; }
}
```

Now, we can add a child to a parent via `parent.Children.Add(child)`.

If the parent has more than one containment property, we cannot set the opposite as usual. Consider the following model:

```csharp
public partial interface IParent
{
    [Contains("IChild.Parent")]
    IChild? Child1 { get; set; }

    [Contains("IChild.Parent")]
    IChild? Child2 { get; set; }
}

public partial interface IChild
{
    [Container] // unclear which opposite to specify?
    IParent? Parent { get; }
}
```

Setting the `IChild.Parent` property does not have the effect of setting the opposite correctly. This is due to the fact that the opposite is unknown. Hence, the `IChild.Parent` property is read-only, i.e., it has no setter.

It is also possible to declare containments without an opposite property:

```csharp
public partial interface IParent
{
    [Contains]
    IChild? Child1 { get; set; }

    [Contains]
    IChild? Child2 { get; set; }
}

public partial interface IChild
{

}
```

In this case the container of `IChild` is only known to the implementation of the `IChild` interface.

## Conclusion

Congrats, you have successfully declared your first containment reference and learned which forms of containment are supported by VMF.NET.

To run the code, use `dotnet run --project Tutorial03`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial03b/README.md)

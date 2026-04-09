# VMF.NET Tutorial 2

[HOME](../README.md) [NEXT ->](../Tutorial03/README.md)

## Using the Change Notification API

### What you will learn

In this tutorial you will learn how to

- use the change notification API to listen to property changes

### Defining the Model

For this tutorial we reuse the model from [Tutorial 1](../Tutorial01/README.md):

```csharp
using VMF.NET.Runtime.Attributes;

namespace Tutorial02.VmfModel;

[VmfModel]
public partial interface IParent
{
    string? Name { get; set; }
}
```

### Listening to Changes

First, we create a new instance of `IParent`. Now we can register a change listener. Notice how all VMF related API can be accessed via the `Vmf()` method. Here's the code:

```csharp
using Tutorial02.VmfModel;

// create a new parent instance
var parent = IParent.NewInstance();

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

// cause a change by setting the name of parent
parent.Name = "Parent 1";

// cause another change
parent.Name = "Parent 2";
```

After registering the listener, we can make some changes to the `Name` property (see `parent.Name = ...` assignments). The output will look like this:

```
evt: Name
  -> oldValue:
  -> newValue: Parent 1
evt: Name
  -> oldValue: Parent 1
  -> newValue: Parent 2
```

This is exactly what we expected. The first change indicates that property `Name` was previously `null` and is now set to "Parent 1". The second change indicates that the `Name` property was previously set to "Parent 1" and is now set to "Parent 2".

## Conclusion

Congrats, you have learned how to use the change notification API.

To run the code, use `dotnet run --project Tutorial02`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial03/README.md)

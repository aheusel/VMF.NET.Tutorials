# VMF.NET Tutorial 12

[HOME](../README.md) [NEXT ->](../Tutorial13/README.md)

## Cloning (Deep Copy & Shallow Copy)

### What you will learn

In this tutorial you will learn how

- to clone object graphs
- shallow copies are different from deep copies

### The Model

For the following experiments, we use the model shown below:

```csharp
using VMF.NET.Runtime;
using VMF.NET.Runtime.Attributes;

namespace Tutorial12.VmfModel;

[VmfModel]
[VmfEquals]
public partial interface IStore
{
    string? Id { get; set; }

    [Contains("IItem.Store")]
    VList<IItem> Items { get; }
}

[VmfModel]
[VmfEquals]
public partial interface IItem
{
    string? Id { get; set; }

    [Container("IStore.Items")]
    IStore? Store { get; }
}
```

First, we will create a store with two items:

```csharp
// first, we create a store with two items
var store = IStore.NewBuilder().WithId("my store").Build();
var item1 = IItem.NewBuilder().WithId("my item 1").Build();
var item2 = IItem.NewBuilder().WithId("my item 2").Build();
store.Items.Add(item1);
store.Items.Add(item2);

// print our store
Console.WriteLine(store);
```

Now, we will create a deep copy and a shallow copy and change their ids. We compare both with the original and conclude that both should be different from the original, i.e., the original should stay untouched:

```csharp
// now we create a deep copy
var deepCopy = store.Vmf().Content().DeepCopy<IStore>();
// and a shallow copy
var shallowCopy = store.Vmf().Content().ShallowCopy<IStore>();

// if we change the id both copies should differ from the original
deepCopy.Id = "deep copy";
shallowCopy.Id = "shallow copy";

Console.WriteLine("----------------------------------------");
Console.WriteLine(" > Equality Test after Id Change");
Console.WriteLine("----------------------------------------");
Console.WriteLine($"store.Equals(deepCopy)    -> {store.Equals(deepCopy)}");
Console.WriteLine($"store.Equals(shallowCopy) -> {store.Equals(shallowCopy)}");
```

So far, shallow copies and deep copies showed identical behavior. If we change the id of an item, we will see that they behave differently. Before we continue, we reset the ids of both copies:

```csharp
// now revert the id of both copies to the original:
deepCopy.Id = store.Id;
shallowCopy.Id = store.Id;
```

Now, we can change the id of the first item of the deep copy:

```csharp
// deep copy: changing an item only affects the copy
deepCopy.Items[0].Id = "my new id 1";

Console.WriteLine("----------------------------------------");
Console.WriteLine(" > Deep Copy Test");
Console.WriteLine("----------------------------------------");
Console.WriteLine("#### Original     ####");
Console.WriteLine(store);
Console.WriteLine("#### Deep Copy    ####");
Console.WriteLine(deepCopy);
```

The deep copy should show an updated item list. But the original store should remain unchanged.

Shallow copies behave differently. While they are a separate instance, they share the references of the original. Here's what happens if we repeat the experiment with the shallow copy:

```csharp
// shallow copy: changing an item affects the original too
shallowCopy.Items[0].Id = "!!! my new id 1 !!!";

Console.WriteLine("----------------------------------------");
Console.WriteLine(" > Shallow Copy Test");
Console.WriteLine("----------------------------------------");
Console.WriteLine("#### Original     ####");
Console.WriteLine(store);
Console.WriteLine("#### Shallow Copy ####");
Console.WriteLine(shallowCopy);
```

This change has also been applied to the original store. It shows the same item list as the shallow copy.

## Conclusion

Congrats, you have successfully cloned object graphs and learned how deep copies are different from shallow copies.

To run the code, use `dotnet run --project Tutorial12`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial13/README.md)

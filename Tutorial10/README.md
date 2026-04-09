# VMF.NET Tutorial 10

[HOME](../README.md) [NEXT ->](../Tutorial11/README.md)

## Equals & HashCode

### What you will learn

In this tutorial you will learn how to

- use `Equals()` & `GetHashCode()`
- ignore properties for `Equals()`

### Using Equals

For the following experiments, we use the model shown below:

```csharp
using VMF.NET.Runtime.Attributes;

namespace Tutorial10.VmfModel;

[VmfModel]
[VmfEquals]
public partial interface IObjectToCompare
{
    string? Id { get; set; }
    int Data { get; set; }
}
```

To tell VMF.NET to provide the `IObjectToCompare` type with its implementation of `Equals()` and `GetHashCode()`, we annotate it with `[VmfEquals]`.

Now let's compare three instances of `IObjectToCompare`:

```csharp
var obj1 = IObjectToCompare.NewBuilder().WithId("id:0").WithData(7).Build();
var obj2 = IObjectToCompare.NewBuilder().WithId("id:0").WithData(7).Build();
var obj3 = IObjectToCompare.NewBuilder().WithId("id:0").WithData(8).Build();

Console.WriteLine($"Object 1 == Object 2 -> {obj1.Equals(obj2)}");
Console.WriteLine($"Object 1 == Object 3 -> {obj1.Equals(obj3)}");
```

The result shows that objects `obj1` and `obj2` are equal, but `obj1` and `obj3` are not.

VMF.NET provides a fully functional implementation of the `Equals()` method. That is, each property is taken into consideration. In our case that means that `obj1.Equals(obj2) == true` and `obj1.Equals(obj3) == false`.

### Ignoring Properties for Equals

To demonstrate the effect of `[IgnoreEquals]` we define another entity `IObjectToCompareId` that ignores the `Data` property in the `Equals()` method provided by VMF.NET. Just like before, we annotate `IObjectToCompareId` with `[VmfEquals]`.

```csharp
[VmfModel]
[VmfEquals]
public partial interface IObjectToCompareId
{
    string? Id { get; set; }

    [IgnoreEquals]
    int Data { get; set; }
}
```

If we try out the same comparison as before but using `IObjectToCompareId` instead of the previous `IObjectToCompare`:

```csharp
var objId1 = IObjectToCompareId.NewBuilder().WithId("id:0").WithData(7).Build();
var objId2 = IObjectToCompareId.NewBuilder().WithId("id:0").WithData(7).Build();
var objId3 = IObjectToCompareId.NewBuilder().WithId("id:0").WithData(8).Build();

Console.WriteLine("--");

Console.WriteLine($"Object 1 == Object 2 -> {objId1.Equals(objId2)}");
Console.WriteLine($"Object 1 == Object 3 -> {objId1.Equals(objId3)}");
```

In this case the objects are all equal to each other because only the `Id` properties are compared.

## HashCode

`GetHashCode()` is based on the `Equals()` method. That means that `[IgnoreEquals]` also has an effect on the implementation of the `GetHashCode()` method. Although it is possible to override `GetHashCode()` and `Equals()` it is not recommended because breaking the contracts causes serious problems with collections etc.

## Interesting to Know

If the `[VmfEquals]` attribute is not specified then the default `Object.Equals(object o)` and `Object.GetHashCode()` implementations are used. The VMF.NET specific implementation is always accessible via `vObj.Vmf().Content().ContentEquals(other)` and `vObj.Vmf().Content().ContentHashCode()` for a model object `vObj`.

## Conclusion

Congrats, you have successfully used VMF.NET's `Equals()` and `GetHashCode()` implementations.

To run the code, use `dotnet run --project Tutorial10`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial11/README.md)

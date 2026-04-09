# VMF.NET Tutorial 7

[HOME](../README.md) [NEXT ->](../Tutorial08/README.md)

## Immutable Objects and ReadOnly API

### What you will learn

In this tutorial you will learn how to

- declare model entities as immutable
- use the ReadOnly API of mutable objects to prevent modifications

### Declaring Entities as Immutable

The model we are going to use consists of an `IMutableObject` entity and an `IImmutableObject` entity. Each of them has a `Value` property. With the `[Immutable]` attribute we can declare an entity as immutable, i.e., the state can only be defined during initialization. Changes are not possible.

```csharp
using VMF.NET.Runtime.Attributes;

namespace Tutorial07.VmfModel;

[VmfModel]
[Immutable]
public partial interface IImmutableObject
{
    int Value { get; }
}

[VmfModel]
public partial interface IMutableObject
{
    int Value { get; set; }
}
```

### Instantiating Immutable Objects

We can instantiate immutable objects by using the builder pattern:

```csharp
// the only way to set the 'Value' property for an immutable object is to
// use the builder pattern. there is no setter method for the 'Value' property
var immutableObject = IImmutableObject.NewBuilder().WithValue(12).Build();
```

Changing the state after instantiation is not possible:

```csharp
// does not compile because we cannot change the initial state of immutable objects
// immutableObject.Value = 12;
```

VMF.NET also ensures that property types of immutables are immutable as well. Otherwise, it would be possible to indirectly change the state of immutable objects.

Just for comparison, this is how mutable objects can be initialized and modified:

```csharp
// the mutable object works exactly as expected. we can create the instance
// and set the 'Value' property afterwards.
var mutableObject = IMutableObject.NewInstance();
mutableObject.Value = 12;
```

Sometimes we want to protect mutable objects from being changed. In this case, we can use the read-only API. VMF.NET generates a read-only counterpart for each model type. We can easily access it by calling the `AsReadOnly()` method:

```csharp
// to prevent that receivers of mutable objects can change their state, VMF.NET
// generates a read-only counterpart for each model type:
IReadOnlyMutableObject readOnlyMutable = mutableObject.AsReadOnly();

// this will not compile:
// readOnlyMutable.Value = 12;
```

It is still possible to register listeners and react to changes since it is just a read-only view of a mutable object. This only guarantees that the consumer of a read-only instance cannot make any changes. But it can still see them. To get a modifiable copy from a read-only instance, use deep copy:

```csharp
// to get a modifiable copy, use deep copy:
var mutableCopy = mutableObject.Vmf().Content().DeepCopy<IMutableObject>();
Console.WriteLine($"Modifiable copy value: {mutableCopy.Value}");
```

## Interesting to Know

- Calling `AsReadOnly()` is a no-op on immutables; immutables do not have ReadOnly siblings
- `DeepCopy()` on immutables uses shared immutable references

## Conclusion

Congrats, you have successfully declared an immutable model entity and used the read-only API of mutable objects.

To run the code, use `dotnet run --project Tutorial07`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial08/README.md)

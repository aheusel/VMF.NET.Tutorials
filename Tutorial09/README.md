# VMF.NET Tutorial 9

[HOME](../README.md) [NEXT ->](../Tutorial10/README.md)

## Custom Default Values for Properties

### What you will learn

In this tutorial you will learn how to

- define default values
- check if properties are set or unset

### Declaring Default Values

VMF.NET properties can have default values. A default value is the initial state of a property. In general, properties can be set and unset. A property is defined as unset if it is equal to its default value. Usually, the default value is just `null` (or the type's default). But sometimes it is necessary to define a custom value as default. This can be achieved with the `[VmfDefaultValue("the value")]` attribute.

The model below declares the entity `IObjectWithDefaultValues` with two properties `Value` and `Name`. The `Value` property has the default value `23` and `Name` has the default value `"my name"`. Here's the corresponding source code:

```csharp
using VMF.NET.Runtime.Attributes;

namespace Tutorial09.VmfModel;

[VmfModel]
public partial interface IObjectWithDefaultValues
{
    [VmfDefaultValue("23")]
    int Value { get; set; }

    [VmfDefaultValue("\"my name\"")]
    string? Name { get; set; }
}
```

If we create an instance, its properties should already be set to the custom defaults:

```csharp
// first, we create an object
var obj = IObjectWithDefaultValues.NewInstance();

// now we can get the default values
Console.WriteLine($"Value: {obj.Value}");
Console.WriteLine($"Name:  {obj.Name}");
```

### Checking whether Properties are set or unset

We can check whether the properties are set or unset:

```csharp
// we use p.IsSet to check if the property is set
string PropertySetOrUnset(string propName)
{
    var p = obj.Vmf().Reflect().PropertyByName(propName);
    return p != null ? p.IsSet.ToString() : "<not available>";
}

// we expect both properties to be unset (False)
Console.WriteLine($"Value is set:  {PropertySetOrUnset("Value")}");
Console.WriteLine($"Name is set:   {PropertySetOrUnset("Name")}");
```

We expect the properties to be unset (`False`). If we change the `Name` property, it should be set:

```csharp
// if we set a property to a different value we expect it to be set (True)
obj.Name = "another name";
Console.WriteLine($"Value is set:  {PropertySetOrUnset("Value")}");
Console.WriteLine($"Name is set:   {PropertySetOrUnset("Name")}");
```

If you run this code it should show that `Name` is set (`True`).

To revert back to the initial state, we can call the `Unset()` method:

```csharp
// unset name property, name should be shown as unset (False)
obj.Vmf().Reflect().PropertyByName("Name")?.Unset();
Console.WriteLine($"Value is set:  {PropertySetOrUnset("Value")}");
Console.WriteLine($"Name is set:   {PropertySetOrUnset("Name")}");
```

## Conclusion

Congrats, you have successfully defined custom default values for properties.

To run the code, use `dotnet run --project Tutorial09`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial10/README.md)

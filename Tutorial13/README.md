# VMF.NET Tutorial 13

[HOME](../README.md) [NEXT ->](../Tutorial14/README.md)

## The Reflection API

### What you will learn

In this tutorial you will learn

- what reflection is
- how to use the VMF.NET reflection for querying type information
- how to use the VMF.NET reflection to access and use properties reflectively
- how to use the reflection API for accessing annotations

There are also tutorials on [annotations](../Tutorial11/README.md) and [properties & default values](../Tutorial09/README.md).

### What is Reflection?

Reflection means that VMF.NET models can analyze and modify their own structures at runtime via API calls.

### Querying Type Information

Consider the following model:

```csharp
using VMF.NET.Runtime.Attributes;

namespace Tutorial13.VmfModel;

[VmfModel]
[VmfAnnotation("info about type A", Key = "my-key")]
public partial interface IA
{
    string? Name { get; set; }
    IA? Child { get; set; }
}
```

The first thing we will try is to get access to the type information and properties of type `IA`. We start by creating an instance and querying its properties:

```csharp
Console.WriteLine("--------------------");
Console.WriteLine("Properties of A:");
var a = IA.NewInstance();
foreach (var p in a.Vmf().Reflect().Properties())
{
    Console.WriteLine($" -> {p.Name}");
}
```

The output should show both properties defined in `IA`, `Name` as well as `Child`. Each property object can set and unset the property as well as register change listeners. To demonstrate that, we retrieve the property by name, register a listener and set the property via reflection, instead of using the API directly (`a.Name = "a name"`):

```csharp
a.Vmf().Reflect().PropertyByName("Name")?.AddChangeListener(c =>
{
    Console.WriteLine($"[CHANGE]: changed property 'Name': {a.Vmf().Reflect().PropertyByName("Name")?.Get()}");
});

a.Name = "my new name";
```

The terminal output should show the change we made reflectively.

The reflection API also gives us access to the annotations. Let's get the annotations of type `IA`:

```csharp
Console.WriteLine("Annotations of A:");
foreach (var ann in a.Vmf().Reflect().Annotations())
{
    Console.WriteLine($" -> {ann.Key}, {ann.Value}");
}
```

Finally, we can check whether a type is a model type or not. Let's revisit all properties and use the type objects of properties to query type name and whether it's a model type or not:

```csharp
Console.WriteLine("Properties of A:");
foreach (var p in a.Vmf().Reflect().Properties())
{
    Console.WriteLine($" -> property-name : {p.Name}");
    Console.WriteLine($"    type name     : {p.Type.Name}");
    Console.WriteLine($"    model-type    : {p.Type.IsModelType}");
}
```

If you run this code, you will see two properties in the output. Property `Name` is no model-type, but property `Child` is.

## Conclusion

Congrats, you have successfully used the reflection API to analyze a model's structure and to set properties reflectively.

To run the code, use `dotnet run --project Tutorial13`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial14/README.md)

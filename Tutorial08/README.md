# VMF.NET Tutorial 8

[HOME](../README.md) [NEXT ->](../Tutorial09/README.md)

## Custom Behavior & Delegation

### What you will learn

In this tutorial you will learn how to

- implement custom functionality
- use and implement the `IDelegatedBehavior<T>` interface

### Declaring a Delegation Method

Delegation methods are a convenient mechanism for providing custom functionality. Unlike other frameworks, VMF.NET restricts custom behavior and does not allow modification of generated code. This doesn't mean that custom functionality can't be implemented. But it ensures that custom code is strictly separated from the generated code.

This is the model we are going to use:

```csharp
using VMF.NET.Runtime.Attributes;

namespace Tutorial08.VmfModel;

[VmfModel]
[DelegateTo(typeof(Tutorial08.CustomBehavior))]
public partial interface IObjectWithCustomBehavior
{
    int A { get; set; }
    int B { get; set; }

    [DelegateTo(typeof(Tutorial08.CustomBehavior))]
    int ComputeSum();
}
```

`IObjectWithCustomBehavior` has two properties `A` and `B`. Additionally, it declares a method `int ComputeSum()` that takes no arguments and returns an `int`. The `[DelegateTo(typeof(...))]` attribute defines which delegation class is used to perform the actual computation.

This is how the delegation class looks like:

```csharp
using VMF.NET.Runtime;
using Tutorial08.VmfModel;

namespace Tutorial08;

public class CustomBehavior : IDelegatedBehavior<IObjectWithCustomBehavior>
{
    private IObjectWithCustomBehavior _caller = null!;

    public void SetCaller(IObjectWithCustomBehavior caller)
    {
        _caller = caller;
    }

    /// <summary>
    /// Delegated behavior. It is called whenever caller.ComputeSum() is called.
    /// This method computes and returns the sum of property 'A' and 'B'.
    /// </summary>
    public int ComputeSum()
    {
        return _caller.A + _caller.B;
    }

    public void OnObjectWithCustomBehaviorInstantiated()
    {
        Console.WriteLine("object instantiated");
    }
}
```

It implements `IDelegatedBehavior<IObjectWithCustomBehavior>` which declares one method, `SetCaller(T caller)`. This method is called during object initialization and gives us access to the caller that delegates to `CustomBehavior`. Then we just need to implement the custom behavior. In our case this is the method `int ComputeSum()`.

### Calling a Delegation Method

Now we can create an instance and try out our custom method:

```csharp
// first, we create an object
var obj = IObjectWithCustomBehavior.NewInstance();

// then we set properties A and B
obj.A = 2;
obj.B = 3;

// finally, we call our custom method and compute the sum of A + B
int sum = obj.ComputeSum();

Console.WriteLine($"Sum: {sum}");
```

### Custom Behavior during Instantiation

If custom behavior should be executed during instantiation, it is possible to annotate the interface directly:

```csharp
[DelegateTo(typeof(Tutorial08.CustomBehavior))]
public partial interface IObjectWithCustomBehavior
{

}
```

Then the delegation class just needs one additional method that is called during instantiation: `OnObjectWithCustomBehaviorInstantiated()`. The naming scheme is `On${ClassName}Instantiated()` where `${ClassName}` is the name of the model entity to be extended with custom behavior. Adding behavior during instantiation is convenient and allows to automatically register listeners to objects etc.

## Conclusion

Congrats, you have successfully implemented custom behavior.

To run the code, use `dotnet run --project Tutorial08`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial09/README.md)

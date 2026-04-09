# VMF.NET Tutorial 14

[HOME](../README.md)

## Custom Model Documentation

### What you will learn

In this tutorial you will learn

- how to document model entities and properties

### What is Custom Model Documentation

VMF.NET creates documentation for the generated API. In addition to that it is often required to add domain specific documentation. The `[Doc]` attribute adds XML doc comments to the generated API.

### How to Document a Model?

Consider the following model:

```csharp
using VMF.NET.Runtime.Attributes;

[VmfModel]
[Doc("Represents a finite state machine.")]
public partial interface IFSM
{
    [Doc("Name of this finite state machine.")]
    string? Name { get; set; }

    // ...
}
```

The `[Doc("...")]` attribute can be used to annotate model entities (interfaces) and properties. Here's the fully documented FSM model:

```csharp
using VMF.NET.Runtime;
using VMF.NET.Runtime.Attributes;

namespace Tutorial14.VmfModel;

/// <summary>Represents a finite state machine.</summary>
[VmfModel]
[Doc("Represents a finite state machine.")]
public partial interface IFSM
{
    [Doc("Name of this finite state machine.")]
    string? Name { get; set; }

    [Doc("The initial state of this finite state machine.")]
    IState? InitialState { get; set; }

    [Doc("The current state of this finite state machine.")]
    IState? CurrentState { get; set; }

    [Doc("The final state of this finite state machine.")]
    IState? FinalState { get; set; }

    [Doc("The complete states of this finite state machine.")]
    [Contains("IState.OwningFSM")]
    VList<IState> OwnedState { get; }
}

/// <summary>Represents the state of a FSM.</summary>
[VmfModel]
[Doc("Represents the state of a FSM.")]
public partial interface IState
{
    [Doc("Name of this state.")]
    string? Name { get; set; }

    [Doc("FSM this state belongs to.")]
    [Container("IFSM.OwnedState")]
    IFSM? OwningFSM { get; }

    [Doc("Outgoing transition of this state.")]
    [Contains("ITransition.Source")]
    ITransition? OutgoingTransition { get; set; }

    [Doc("Incoming transition of this state.")]
    [Contains("ITransition.Target")]
    ITransition? IncomingTransition { get; set; }
}

/// <summary>Transition between two states.</summary>
[VmfModel]
[Doc("Transition between two states.")]
public partial interface ITransition
{
    [Doc("Source state.")]
    [Container("IState.OutgoingTransition")]
    IState? Source { get; }

    [Doc("Target state.")]
    [Container("IState.IncomingTransition")]
    IState? Target { get; }

    [Doc("Action that is executed when applying this transition.")]
    IAction? Action { get; set; }
}

/// <summary>Action that can be executed by the FSM.</summary>
[VmfModel]
[Doc("Action that can be executed by the FSM.")]
public partial interface IAction
{
    string? Name { get; set; }
}
```

The generated code will include XML doc comments derived from the `[Doc]` annotations, making them visible in your IDE's IntelliSense/autocompletion:

```csharp
// explore the API with your favourite IDE and see the custom documentation
var fsm = IFSM.NewInstance();
fsm.Name = "My FSM";

Console.WriteLine($"Created FSM: {fsm.Name}");
Console.WriteLine("Check the generated code in your IDE to see @Doc annotations as XML doc comments.");
```

## Conclusion

Congrats, you have successfully used the `[Doc]` attribute to document a VMF.NET model.

To run the code, use `dotnet run --project Tutorial14`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md)

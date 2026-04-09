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

using Tutorial07.VmfModel;

// the only way to set the 'Value' property for an immutable object is to
// use the builder pattern. there is no setter method for the 'Value' property
var immutableObject = IImmutableObject.NewBuilder().WithValue(12).Build();

// does not compile because we cannot change the initial state of immutable objects
// immutableObject.Value = 12;

Console.WriteLine($"Immutable value: {immutableObject.Value}");

// the mutable object works exactly as expected. we can create the instance
// and set the 'Value' property afterwards.
var mutableObject = IMutableObject.NewInstance();
mutableObject.Value = 12;

// to prevent that receivers of mutable objects can change their state, VMF
// generates a read-only counterpart for each model type:
IReadOnlyMutableObject readOnlyMutable = mutableObject.AsReadOnly();

// this will not compile:
// readOnlyMutable.Value = 12;

Console.WriteLine($"Read-only value: {readOnlyMutable.Value}");

// to get a modifiable copy, use deep copy:
var mutableCopy = mutableObject.Vmf().Content().DeepCopy<IMutableObject>();
Console.WriteLine($"Modifiable copy value: {mutableCopy.Value}");

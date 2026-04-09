using Tutorial14.VmfModel;

// explore the API with your favourite IDE and see the custom documentation
var fsm = IFSM.NewInstance();
fsm.Name = "My FSM";

Console.WriteLine($"Created FSM: {fsm.Name}");
Console.WriteLine("Check the generated code in your IDE to see @Doc annotations as XML doc comments.");

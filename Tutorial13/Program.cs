using Tutorial13.VmfModel;

Console.WriteLine("--------------------");
Console.WriteLine("Properties of A:");
var a = IA.NewInstance();
foreach (var p in a.Vmf().Reflect().Properties())
{
    Console.WriteLine($" -> {p.Name}");
}

Console.WriteLine("--------------------");

a.Vmf().Reflect().PropertyByName("Name")?.AddChangeListener(c =>
{
    Console.WriteLine($"[CHANGE]: changed property 'Name': {a.Vmf().Reflect().PropertyByName("Name")?.Get()}");
});

a.Name = "my new name";

Console.WriteLine("--------------------");

Console.WriteLine("Annotations of A:");
foreach (var ann in a.Vmf().Reflect().Annotations())
{
    Console.WriteLine($" -> {ann.Key}, {ann.Value}");
}

Console.WriteLine("--------------------");

Console.WriteLine("Properties of A:");
foreach (var p in a.Vmf().Reflect().Properties())
{
    Console.WriteLine($" -> property-name : {p.Name}");
    Console.WriteLine($"    type name     : {p.Type.Name}");
    Console.WriteLine($"    model-type    : {p.Type.IsModelType}");
}

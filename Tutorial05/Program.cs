using Tutorial05.VmfModel;

// create a new node instance via builder
var node1 = INode.NewBuilder()
    .WithName("my node") // set the name
    .WithId(3)           // set the id
    .Build();

// create a second node via NewInstance
var node2 = INode.NewInstance();

// use the builder to apply state from node1 to node2
INode.NewBuilder().ApplyFrom(node1).ApplyTo(node2);

// check whether properties have been applied correctly
Console.WriteLine($"> node1.Name == node2.Name: {node1.Name == node2.Name}");
Console.WriteLine($"> node1.Id == node2.Id:     {node1.Id == node2.Id}");

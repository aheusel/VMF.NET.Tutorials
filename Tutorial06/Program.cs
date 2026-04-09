using Tutorial06.VmfModel;

// create a new Node instance (this is our root)
var root = INode.NewBuilder().WithName("root").WithVisible(true).Build();

// create three children
var child1 = INode.NewBuilder().WithName("child 1").WithVisible(true).Build();
var child2 = INode.NewBuilder().WithName("child 2").WithVisible(true).Build();
var child3 = INode.NewBuilder().WithName("child 3").WithVisible(true).Build();

// and add them to the root node
root.Child1 = child1;
root.Child2 = child2;
root.Child3 = child3;

Console.WriteLine("Object Graph: ");

// traverse our object graph
root.Vmf().Content().Stream<INode>().ToList().ForEach(
    node => Console.WriteLine($"-> node: {node.Name}")
);

// with our custom property order (see model), child3 comes before child2 before child1
// -> node: root
// -> node: child 3
// -> node: child 2
// -> node: child 1

// if we want to access all properties of a Node object:
Console.WriteLine("\nProperties of root: ");
foreach (var p in root.Vmf().Reflect().Properties())
{
    Console.WriteLine($"-> prop: {p.Name}");
}

// now we can easily declare nodes as invisible and filter them out:
child2.Visible = false;

Console.WriteLine("\nObject Graph without 'child 2': ");
root.Vmf().Content().Stream<INode>()
    .Where(n => n.Visible)
    .ToList()
    .ForEach(node => Console.WriteLine($"-> node: {node.Name}"));

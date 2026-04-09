using Tutorial04.VmfModel;

// create a new root instance
var root = INode.NewInstance();

// start change recorder for undo
root.Vmf().Changes().Start();

// register change listener
root.Vmf().Changes().AddListener(evt =>
{
    Console.WriteLine($"evt: {evt.PropertyName}");

    if (evt.PropertyChange is { } pc)
    {
        Console.WriteLine($"  -> oldValue: {pc.OldValue}");
        Console.WriteLine($"  -> newValue: {pc.NewValue}");
    }
    else if (evt.ListChange is { } lc)
    {
        Console.WriteLine($"  -> {lc}");
    }
});

// cause a change by setting the name of root
root.Name = "#1";

Console.WriteLine("--------");

// create a new child
var child1 = INode.NewInstance();

// add the child to the root
root.Children.Add(child1);

Console.WriteLine("--------");

// cause a change by setting the name property of child 1
child1.Name = "#2";

Console.WriteLine("--------");

// create a deep clone of root
var rootClone = root.Vmf().Content().DeepCopy<INode>();

// ensure that rootClone and root are content-equal ...
Console.WriteLine($"root eq rootClone: {root.Vmf().Content().ContentEquals(rootClone)}");

// ... but not identical
Console.WriteLine($"root != rootClone: {!ReferenceEquals(root, rootClone)}");

// use automatically generated ToString() method
Console.WriteLine($" -> root:      {root}");
Console.WriteLine($" -> rootClone: {rootClone}");

Console.WriteLine("--------");

// show number of changes
Console.WriteLine($"#changes: {root.Vmf().Changes().All().Count}\n");

// invert change order ...
var changesToRevert = root.Vmf().Changes().All().Reverse().ToList();

// ... and undo all changes
foreach (var c in changesToRevert)
{
    Console.WriteLine("-------- undo change: --------");
    c.Undo();
}

// after undo we compare the clone and the empty root (they are not equal)
// we expect the root to be empty (all changes are reverted)
Console.WriteLine("--------");
Console.WriteLine($"root eq clone: {root.Vmf().Content().ContentEquals(rootClone)}");
Console.WriteLine($" -> root:      {root}");
Console.WriteLine($" -> rootClone: {rootClone}");

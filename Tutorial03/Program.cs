using Tutorial03.VmfModel;

// create a new parent instance
var parent = IParent.NewInstance();

// register change listener
parent.Vmf().Changes().AddListener(evt =>
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

// cause a change by setting the name of parent
parent.Name = "Parent 1";

Console.WriteLine("--------");

// create a new child
var child1 = IChild.NewInstance();

// add the child to the parent
parent.Child = child1;

Console.WriteLine("--------");

// containment references make it possible: the child automatically knows its parent
Console.WriteLine($"my parent: {child1.Parent!.Name}");

Console.WriteLine("--------");

// cause a change by setting the value property of child 1
// child 1 is implicitly observed
child1.Value = 42;

Console.WriteLine("--------");

// now we create a second parent
var parent2 = IParent.NewInstance();
parent2.Name = "Parent 2";

// adding child 1 to parent2 has several interesting effects
// 1. child1 is removed from parent1 (check change notification output)
// 2. parent of child1 is now parent2
parent2.Child = child1;
Console.WriteLine("--------");

// containment references make it possible: the child automatically knows its new parent
Console.WriteLine($"my new parent: {child1.Parent!.Name}");

Console.WriteLine("--------");

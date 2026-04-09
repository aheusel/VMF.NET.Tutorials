using Tutorial02.VmfModel;

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

// cause another change
parent.Name = "Parent 2";

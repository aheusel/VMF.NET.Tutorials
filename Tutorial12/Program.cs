using Tutorial12.VmfModel;

// first, we create a store with two items
var store = IStore.NewBuilder().WithId("my store").Build();
var item1 = IItem.NewBuilder().WithId("my item 1").Build();
var item2 = IItem.NewBuilder().WithId("my item 2").Build();
store.Items.Add(item1);
store.Items.Add(item2);

// print our store
Console.WriteLine(store);

// now we create a deep copy
var deepCopy = store.Vmf().Content().DeepCopy<IStore>();
// and a shallow copy
var shallowCopy = store.Vmf().Content().ShallowCopy<IStore>();

// if we change the id both copies should differ from the original
deepCopy.Id = "deep copy";
shallowCopy.Id = "shallow copy";

Console.WriteLine("----------------------------------------");
Console.WriteLine(" > Equality Test after Id Change");
Console.WriteLine("----------------------------------------");
Console.WriteLine($"store.Equals(deepCopy)    -> {store.Equals(deepCopy)}");
Console.WriteLine($"store.Equals(shallowCopy) -> {store.Equals(shallowCopy)}");

// now revert the id of both copies to the original:
deepCopy.Id = store.Id;
shallowCopy.Id = store.Id;

// deep copy: changing an item only affects the copy
deepCopy.Items[0].Id = "my new id 1";

Console.WriteLine("----------------------------------------");
Console.WriteLine(" > Deep Copy Test");
Console.WriteLine("----------------------------------------");
Console.WriteLine("#### Original     ####");
Console.WriteLine(store);
Console.WriteLine("#### Deep Copy    ####");
Console.WriteLine(deepCopy);

// shallow copy: changing an item affects the original too
shallowCopy.Items[0].Id = "!!! my new id 1 !!!";

Console.WriteLine("----------------------------------------");
Console.WriteLine(" > Shallow Copy Test");
Console.WriteLine("----------------------------------------");
Console.WriteLine("#### Original     ####");
Console.WriteLine(store);
Console.WriteLine("#### Shallow Copy ####");
Console.WriteLine(shallowCopy);

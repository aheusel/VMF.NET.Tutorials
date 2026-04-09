using Tutorial09.VmfModel;

// first, we create an object
var obj = IObjectWithDefaultValues.NewInstance();

// now we can get the default values
Console.WriteLine($"Value: {obj.Value}");
Console.WriteLine($"Name:  {obj.Name}");

// we use p.IsSet to check if the property is set
string PropertySetOrUnset(string propName)
{
    var p = obj.Vmf().Reflect().PropertyByName(propName);
    return p != null ? p.IsSet.ToString() : "<not available>";
}

// we expect both properties to be unset (False)
Console.WriteLine($"Value is set:  {PropertySetOrUnset("Value")}");
Console.WriteLine($"Name is set:   {PropertySetOrUnset("Name")}");

Console.WriteLine("--");

// if we set a property to a different value we expect it to be set (True)
obj.Name = "another name";
Console.WriteLine($"Value is set:  {PropertySetOrUnset("Value")}");
Console.WriteLine($"Name is set:   {PropertySetOrUnset("Name")}");

Console.WriteLine("--");

// unset name property, name should be shown as unset (False)
obj.Vmf().Reflect().PropertyByName("Name")?.Unset();
Console.WriteLine($"Value is set:  {PropertySetOrUnset("Value")}");
Console.WriteLine($"Name is set:   {PropertySetOrUnset("Name")}");

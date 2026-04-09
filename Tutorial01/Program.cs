using Tutorial01.VmfModel;

// create a new parent instance
var parent = IParent.NewInstance();

// set parent's name
parent.Name = "My Name";

// check that name is set
if ("My Name" == parent.Name)
{
    Console.WriteLine("> GOOD: name is correctly set");
}
else
{
    Console.WriteLine("> BAD: something went wrong :(");
}

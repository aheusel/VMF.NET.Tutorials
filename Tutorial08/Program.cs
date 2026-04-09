using Tutorial08.VmfModel;

// first, we create an object
var obj = IObjectWithCustomBehavior.NewInstance();

// then we set properties A and B
obj.A = 2;
obj.B = 3;

// finally, we call our custom method and compute the sum of A + B
int sum = obj.ComputeSum();

Console.WriteLine($"Sum: {sum}");

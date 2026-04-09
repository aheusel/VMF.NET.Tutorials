using Tutorial10.VmfModel;

// obj1 is equal to obj2 but obj1 is not equal to obj3
var obj1 = IObjectToCompare.NewBuilder().WithId("id:0").WithData(7).Build();
var obj2 = IObjectToCompare.NewBuilder().WithId("id:0").WithData(7).Build();
var obj3 = IObjectToCompare.NewBuilder().WithId("id:0").WithData(8).Build();

Console.WriteLine($"Object 1 == Object 2 -> {obj1.Equals(obj2)}");
Console.WriteLine($"Object 1 == Object 3 -> {obj1.Equals(obj3)}");

// we expect this output:
// Object 1 == Object 2 -> True
// Object 1 == Object 3 -> False

// in this case all objects are equal because only the 'Id' property is compared
var objId1 = IObjectToCompareId.NewBuilder().WithId("id:0").WithData(7).Build();
var objId2 = IObjectToCompareId.NewBuilder().WithId("id:0").WithData(7).Build();
var objId3 = IObjectToCompareId.NewBuilder().WithId("id:0").WithData(8).Build();

Console.WriteLine("--");

Console.WriteLine($"Object 1 == Object 2 -> {objId1.Equals(objId2)}");
Console.WriteLine($"Object 1 == Object 3 -> {objId1.Equals(objId3)}");

// we expect this output:
// Object 1 == Object 2 -> True
// Object 1 == Object 3 -> True

# VMF.NET Tutorial 1

[HOME](../README.md) [NEXT ->](../Tutorial02/README.md)

## Defining your First Model

### What you will learn

In this tutorial you will learn how to

- setup a .NET project for VMF.NET
- create a basic model
- use the generated implementation

### Setting up a .NET Project

VMF.NET uses a Roslyn Source Generator distributed as a NuGet package. When you reference the VMF.NET packages in your project, code generation happens automatically at build time. A typical `.csproj` looks like:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="VMF.NET.Runtime" />
    <PackageReference Include="VMF.NET.SourceGenerator" />
  </ItemGroup>
</Project>
```

In our first example we want to generate code for a very basic model. It just consists of one interface `IParent` with a single string property `Name`. Here's how we can define the model as a C# interface:

```csharp
using VMF.NET.Runtime.Attributes;

namespace Tutorial01.VmfModel;

[VmfModel]
public partial interface IParent
{
    string? Name { get; set; }
}
```

The source directories of our tutorial project look like this:

```
Tutorial01
├── Model.cs        (model definition)
├── Program.cs      (application entry point)
└── Tutorial01.csproj
```

### Running the Code Generator

After we created our first model definition we are ready to run the code generator. The source generator runs automatically when you build the project:

```
dotnet build Tutorial01
```

### Using the Code

To use the code, reference the generated types from your regular C# code, e.g., in `Program.cs`:

```csharp
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
```

## Running the Tutorial

### Requirements

- .NET 10 SDK or later
- Internet connection (NuGet packages are downloaded automatically)

### IDE

Open the `VMF.NET.Tutorials` solution in your favourite IDE (tested with Visual Studio 2022 and JetBrains Rider) and run the `Tutorial01` project.

### Command Line

Navigate to the solution root and enter the following command:

    dotnet run --project Tutorial01

## Conclusion

Congrats, you have successfully created your first VMF.NET model.

[HOME](../README.md) [NEXT ->](../Tutorial02/README.md)

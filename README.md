# VMF.NET Tutorials

C# port of the [VMF-Tutorials](https://github.com/miho/VMF-Tutorials) for [VMF.NET](https://github.com/aheusel/VMF.NET).

## Prerequisites

- .NET 10 SDK or later

## Running a Tutorial

```bash
dotnet run --project Tutorial01
```

## Tutorials

| Tutorial | Topic | Description |
|---|---|---|
| [Tutorial 01](Tutorial01/) | First Model | Define a basic model interface with a single property |
| [Tutorial 02](Tutorial02/) | Change Notification | Listen to property changes via `Vmf().Changes().AddListener()` |
| [Tutorial 03](Tutorial03/) | Containment | Parent-child relationships with `[Contains]` and `[Container]` |
| [Tutorial 03b](Tutorial03b/) | Cross References | Many-to-many relationships with `[Refers]` |
| [Tutorial 04](Tutorial04/) | Undo/Redo | Record changes and revert them via `Change.Undo()` |
| [Tutorial 05](Tutorial05/) | Builder API | Fluent object construction with `NewBuilder().WithName(...).Build()` |
| [Tutorial 06](Tutorial06/) | Graph Traversal | Object graph iteration with `[PropertyOrder]` and `Stream<T>()` |
| [Tutorial 07](Tutorial07/) | Immutable & ReadOnly | `[Immutable]` types and `AsReadOnly()` wrappers |
| [Tutorial 08](Tutorial08/) | Delegation | Custom behavior via `[DelegateTo]` and `IDelegatedBehavior<T>` |
| [Tutorial 09](Tutorial09/) | Default Values | `[VmfDefaultValue]` for initial property values |
| [Tutorial 10](Tutorial10/) | Equals & HashCode | `[VmfEquals]` and `[IgnoreEquals]` for content equality |
| [Tutorial 11](Tutorial11/) | Annotations | `[VmfAnnotation]` metadata queryable via reflection |
| [Tutorial 12](Tutorial12/) | Cloning | `DeepCopy()` vs `ShallowCopy()` behavior |
| [Tutorial 13](Tutorial13/) | Reflection API | Runtime introspection of properties, types, and annotations |
| [Tutorial 14](Tutorial14/) | Documentation | `[Doc]` annotations for generated XML doc comments |

## Key Differences from Java VMF

| Java VMF | VMF.NET |
|---|---|
| `interface Parent { String getName(); }` | `public partial interface IParent { string? Name { get; set; } }` |
| `Parent.newInstance()` | `IParent.NewInstance()` |
| `parent.setName("x")` | `parent.Name = "x"` |
| `parent.vmf().changes()` | `parent.Vmf().Changes()` |
| `@Contains(opposite="parent")` | `[Contains("IChild.Parent")]` |
| `@Container(opposite="child")` | `[Container("IParent.Child")]` |
| `@Refers(opposite="books")` | `[Refers("IBook.Authors")]` |
| `@Immutable` | `[Immutable]` |
| `@DefaultValue(value="23")` | `[VmfDefaultValue("23")]` |
| `@VMFEquals` | `[VmfEquals]` |
| `@Annotation(key="k",value="v")` | `[VmfAnnotation("v", Key = "k")]` |
| `@DelegateTo(className="...")` | `[DelegateTo(typeof(...))]` |
| `@Doc("...")` | `[Doc("...")]` |
| Gradle/Maven plugin | NuGet source generator (automatic) |

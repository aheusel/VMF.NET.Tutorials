# VMF.NET Tutorial 3b

[HOME](../README.md) [NEXT ->](../Tutorial04/README.md)

## Cross References

### What you will learn

In this tutorial you will learn how to

- declare cross references

### Declaring Cross References

For this tutorial we use the following model definition:

```csharp
using VMF.NET.Runtime;
using VMF.NET.Runtime.Attributes;

namespace Tutorial03b.VmfModel;

[VmfModel]
public partial interface IBook
{
    string? Title { get; set; }

    [Refers("IAuthor.Books")]
    VList<IAuthor> Authors { get; }
}

[VmfModel]
public partial interface IAuthor
{
    string? Name { get; set; }

    [Refers("IBook.Authors")]
    VList<IBook> Books { get; }
}
```

In the above code sample the `IBook` interface declares that it references one or many `IAuthor` objects. Thus, we use the `[Refers("IAuthor.Books")]` attribute and declare the `Books` property of the `IAuthor` interface as the opposite. In the `IAuthor` interface we use the `[Refers("IBook.Authors")]` attribute to indicate that the referenced property in `IBook` is the `Authors` property.

Now let's instantiate an `IBook`, an `IAuthor` and add the author to the book's `Authors` list.

```csharp
// create a book instance
var book = IBook.NewBuilder().WithTitle("My Book").Build();

// and an author. we reference the book.
var author = IAuthor.NewBuilder().WithName("The Author").WithBooks(book).Build();
```

Since we declared a cross reference we expect the book to reference the author. Now we can check the `Authors` property:

```csharp
// cross references make it possible: the book also references the author
if (book.Authors.Count == 1)
{
    Console.WriteLine($"#referenced-author: {book.Authors[0]}");
}
else
{
    throw new Exception("We referenced the book and therefore, the book has to reference the author.");
}
```

## Conclusion

Congrats, you have successfully declared your first cross reference.

To run the code, use `dotnet run --project Tutorial03b`. See [Tutorial 1](../Tutorial01/README.md#running-the-tutorial) for general setup instructions.

[HOME](../README.md) [NEXT ->](../Tutorial04/README.md)

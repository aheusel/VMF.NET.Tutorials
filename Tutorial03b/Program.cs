using Tutorial03b.VmfModel;

// create a book instance
var book = IBook.NewBuilder().WithTitle("My Book").Build();

// and an author. we reference the book.
var author = IAuthor.NewBuilder().WithName("The Author").WithBooks(book).Build();

// the book also references the author
if (book.Authors.Count == 1)
{
    Console.WriteLine($"#referenced-author: {book.Authors[0]}");
}
else
{
    throw new Exception("We referenced the book and therefore, the book has to reference the author.");
}

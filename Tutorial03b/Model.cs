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

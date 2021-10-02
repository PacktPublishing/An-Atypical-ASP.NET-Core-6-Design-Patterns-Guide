using System.Collections.Generic;

namespace DIP.Data
{
    public interface IBookReader
    {
        IEnumerable<Book> Books { get; }
        Book Find(int bookId);
    }
}

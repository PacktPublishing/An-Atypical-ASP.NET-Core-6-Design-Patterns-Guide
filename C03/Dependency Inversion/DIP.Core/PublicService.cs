using DIP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIP.Core
{
    public class PublicService
    {
        public IBookReader _bookReader;

        public Task<IEnumerable<Book>> FindAllAsync()
        {
            return Task.FromResult(_bookReader.Books);
        }

        public Task<Book> FindAsync(int bookId)
        {
            var book = _bookReader.Find(bookId);
            return Task.FromResult(book);
        }
    }
}

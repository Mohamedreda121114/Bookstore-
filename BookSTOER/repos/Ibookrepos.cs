using BookSTOER.Model;

namespace BookSTOER.repos
{
    public interface Ibookrepos
    {
        public List<Book> GetBooks();
        public Book GetBook(int id);
        public Book AddBook(BookDTO book);

        public Book UpdateBook(Book book);
        public Book RemoveBook(int id);
    }
}

using System.Data.Common;
using BookSTOER.Data;
using BookSTOER.Model;
using BookSTOER.repos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookSTOER.NewFolder.repos
{
    public class BookRepos : Ibookrepos
    {
        private readonly dbcontext dbcontext;

        public BookRepos(dbcontext dbcontext )
        {
            this.dbcontext = dbcontext;
        }
        public Book AddBook(BookDTO book)
        {
            using var stream = new MemoryStream();
            if (book.image != null)
            {
                book.image.CopyTo(stream);

            }
            if (book.rate < 0 || book.rate >= 5)

            { }
            var books = new Book
            {
                Name = book.Name,
                image = stream.ToArray(),
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                rate = book.rate,
                Price = book.Price,
                Stock = book.Stock,
            };
             dbcontext.Set<Book>().Add(books);
             dbcontext.SaveChanges();
            return (books);

        }

        public Book GetBook(int id)
        {
            var b = dbcontext.Set<Book>().FirstOrDefault( b => b.Id == id );
            return b;
           
        }

        public List<Book> GetBooks()
        {
            return dbcontext.Set<Book>().ToList();
        }

        public Book RemoveBook(int id)
        {
            var e = dbcontext.Set<Book>().FirstOrDefault(e => e.Id == id);
            dbcontext.Set<Book>().Remove(e);
            dbcontext.SaveChanges();
            return e;
        }

        public Book UpdateBook(Book book)
        {
            var e = dbcontext.Set<Book>().FirstOrDefault();
            e.Author = book.Author;
            e.Description = book.Description;
            e.Price = book.Price;
            e.OrderItems = book.OrderItems;
            e.rate = book.rate;
            e.Name = book.Name;
            e.Stock = book.Stock;
            e.Title = book.Title;
            dbcontext.SaveChanges();
            return e;
        }
    }
}

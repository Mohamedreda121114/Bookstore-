using BookSTOER.Model;
using Microsoft.EntityFrameworkCore;

namespace BookSTOER.Data
{
    public class dbcontext : DbContext
    {
        public dbcontext( DbContextOptions dbContextOptions  ) : base( dbContextOptions )
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<orderItim>().ToTable("OrderItems");
        }
    }
}

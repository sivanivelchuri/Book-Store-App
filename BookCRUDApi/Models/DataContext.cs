using Microsoft.EntityFrameworkCore;

namespace BookCRUDApi.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<UserFavorites> UserFavorites { get; set; }
    }
}


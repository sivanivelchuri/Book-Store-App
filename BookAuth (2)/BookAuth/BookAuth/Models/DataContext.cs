using Microsoft.EntityFrameworkCore;

namespace BookAuth.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<UserRegister> userRegisters { get; set; }
    }
}

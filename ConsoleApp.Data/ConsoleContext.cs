using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Data
{
    public class ConsoleContext : DbContext
    {
        public ConsoleContext(DbContextOptions<ConsoleContext> options) : base(options)
        {
                
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlite("Data Source=nagato.db");
        // }

        public DbSet<User> Users { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
namespace PasswordMan
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "PasswordDb");
        }
        public DbSet<Password> Passwords { get; set; }
    }
}

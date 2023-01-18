using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
namespace Libirary.Models
{
    public class LibDBContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public LibDBContext(DbContextOptions<LibDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Book>(s => s.Property(u => u.BSN).HasDefaultValueSql("newsequentialid()"));
        }
    }
}

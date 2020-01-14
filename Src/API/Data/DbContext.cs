using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {
        }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }
        public DbSet<Capture> Capture { get; set; }
    }
}

using DispatchingModule.Model;
using Microsoft.EntityFrameworkCore;

namespace DispatchingModule.Data
{
    public class SqlContext : DbContext
    {     
        public SqlContext() : base()
        {
        }
        private const string connectionString = "Server=DTALP0253;Initial Catalog=Monitoring_1;Integrated Security=false;User Id=monitoring1;Password=Matryx12";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }
        public DbSet<Capture> Capture { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace ShipsBackEnd.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }

        public DbSet<Port> Ports { get; set; }

        public DatabaseContext(DbContextOptions options): base(options) { }


    }
}

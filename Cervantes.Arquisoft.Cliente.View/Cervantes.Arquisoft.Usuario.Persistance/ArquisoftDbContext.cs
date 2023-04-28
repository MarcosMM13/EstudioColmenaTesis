using Cervantes.Arquisoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cervantes.Arquisoft.Usuario.Data
{
    public class ArquisoftDbContext : DbContext
    {
        private readonly string connectionString;

        public ArquisoftDbContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);

        }

        DbSet<Client> Clients { get; set; }
    }
}

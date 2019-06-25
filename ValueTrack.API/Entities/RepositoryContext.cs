using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientRegion> ClientRegions { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using ContractorsAPI.Models;

namespace ContractorsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contractor> Contractors { get; set; }
    }
}
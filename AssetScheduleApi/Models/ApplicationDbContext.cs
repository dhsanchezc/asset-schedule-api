using AssetScheduleApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssetScheduleApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Asset> Assets { get; set; } = null!;
    }
}
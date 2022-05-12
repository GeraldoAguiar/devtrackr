using DEVTRACKR.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DEVTRACKR.API.Persistence;

public class DevTrackRContext : DbContext
{
    public DevTrackRContext(DbContextOptions<DevTrackRContext> options)
        : base(options)
    {
    }
    public DbSet<Package> Packages { get; set; }
    public DbSet<PackageUpdate> PackageUpdates { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Package>(x => {
            x.HasKey(x => x.Id);

            x.HasMany(x => x.Updates)
                .WithOne()
                .HasForeignKey(pu => pu.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<PackageUpdate>(x => {
            x.HasKey(x => x.Id);
        });
    }
}
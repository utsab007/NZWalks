using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed data for difficulties
            // easy medium hard
            var difficulties = new[]
            {
                new Difficulty {Id = Guid.Parse("a2f36d39-fc69-4464-aed6-79a3c8f62eb9"), Name = "Easy"},
                new Difficulty {Id = Guid.Parse("f718d1e2-40c7-4b34-bfc5-488b694c4b19"), Name = "Medium"},
                new Difficulty {Id = Guid.Parse("331238bb-bc64-447c-b29d-21b9998ec3cd"), Name = "Hard"}
            };
            // seed difficulties to database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // seed data for regions
            var regions = new[]
            {
                new Region
                {
                    Id = Guid.Parse("f1b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"),
                    Code = "NOR",
                    Name = "Northland",
                    RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/regions/northland/northland-landscape-2.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("f2b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"),
                    Code = "ACK",
                    Name = "Auckland",
                    RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/regions/auckland/auckland-landscape-1.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("f3b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"),
                    Code = "WAI",
                    Name = "Waikato",
                    RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/regions/waikato/waikato-landscape-1.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("f4b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"),
                    Code = "BOP",
                    Name = "Bay of Plenty",
                    RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/regions/bay-of-plenty/bay-of-plenty-landscape-1.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("f5b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"),
                    Code = "TAR",
                    Name = "Taranaki",
                    RegionImageUrl = "https://www.doc.govt.nz/globalassets/images/regions/taranaki/taranaki-landscape-1.jpg"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}

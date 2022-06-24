using DocService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Knowbase> Knowbases { get; set; }
        public DbSet<Doc> Docs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Knowbase>()
                .HasMany(p => p.Docs)
                .WithOne(p => p.Knowbase!)
                .HasForeignKey(p => p.KnowbaseId);

            modelBuilder
                .Entity<Doc>()
                .HasOne(p => p.Knowbase)
                .WithMany(p => p.Docs)
                .HasForeignKey(p => p.KnowbaseId);
        }
    }
}

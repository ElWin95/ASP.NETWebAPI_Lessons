using GenericRepoPractice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepoPractice.DataAccessLayer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id=1, Name="Teacher1"},
                new Teacher { Id=2, Name="Teacher2"},
                new Teacher { Id=3, Name="Teacher3"}
                );
            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "Group1", TeacherId = 1 },
                new Group { Id = 2, Name = "Group2", TeacherId = 2 },
                new Group { Id = 3, Name = "Group3", TeacherId = 3 }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RP7XMC_HFT_2022232.Models;

namespace RP7XMC_HFT_2022232.Repository
{
    public class CarDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Service> Services { get; set; }    
        public CarDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("Service");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasOne(t => t.Car)
                .WithMany(t => t.Brands)
                .HasForeignKey(t => t.CarId);

            modelBuilder.Entity<Brand>()
                .HasOne(t => t.Service)
                .WithMany(t => t.Brands)
                .HasForeignKey(t => t.ServiceId);

            modelBuilder.Entity<Brand>().HasData(
                new Brand {BrandId = 1, BrandName = "BMW",CarId =1 ,ServiceId =1}
                );
            modelBuilder.Entity<Car>().HasData(
                new Car { CarId = 1, CarName = "E60"}
                );
            modelBuilder.Entity<Service>().HasData(
                new Service { ServiceId = 1, ServiceName = "BMW_Budeapest" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}

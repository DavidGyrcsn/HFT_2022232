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
            //base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasOne(t => t.Car)
                //.WithMany(t => t.Brands)
                .WithOne(t=>t.Brand)
                //.HasMany(t => t.Cars)
                //.WithOne(t => t.Brand)
                .HasForeignKey<Car>(t => t.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Brand>()
                .HasOne(t => t.Service)
                .WithMany(t => t.Brands)
                //.HasMany(t => t.Services)
                //.WithOne(t => t.Brand)
                .HasForeignKey(t => t.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Car>().HasData(new Car[] {
                new Car() { CarId = 1, CarName = "E60",BrandId = 1},
                new Car() { CarId = 2, CarName = "S class" ,BrandId = 2},
                new Car() { CarId = 3, CarName = "A8" ,BrandId = 3},
                new Car() { CarId = 4, CarName = "E39" ,BrandId = 5},
                new Car (){ CarId = 5, CarName = "Omega" ,BrandId = 4}
                });
            modelBuilder.Entity<Service>().HasData(new Service[] {
                new Service() { ServiceId = 1, ServiceName = "BMW_Budeapest" },
                new Service() { ServiceId = 2, ServiceName = "opel_szervíz" },
                new Service() { ServiceId = 3, ServiceName = "Lakatos.kft" },
                new Service() { ServiceId = 4, ServiceName = "Benz_márkaszervíz" },
                new Service() { ServiceId = 5, ServiceName = "Renault-Magyarország" }
                });
            modelBuilder.Entity<Brand>().HasData(new Brand[]
                {
                new Brand() {BrandId = 1, BrandName = "BMW",CarId =1 ,ServiceId =1,MaintenanceCost = 200000},
                new Brand() { BrandId = 2, BrandName = "MercededsBenz", CarId = 2, ServiceId = 4, MaintenanceCost = 400000 },
                new Brand() { BrandId = 3, BrandName = "Audi", CarId = 3, ServiceId = 3, MaintenanceCost = 300000 },
                new Brand() { BrandId = 4, BrandName = "Opel", CarId = 5, ServiceId = 2 , MaintenanceCost = 50000 },
                new Brand() { BrandId = 5, BrandName = "BMW", CarId = 4, ServiceId = 1 , MaintenanceCost = 300000 }
                });

            //base.OnModelCreating(modelBuilder);
        }
    }
}

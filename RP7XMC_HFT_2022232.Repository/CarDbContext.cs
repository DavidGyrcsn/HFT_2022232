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
                new Brand {BrandId = 1, BrandName = "BMW",CarId =1 ,ServiceId =1},
                new Brand { BrandId = 2, BrandName = "MercededsBenz", CarId = 2, ServiceId = 4 },
                new Brand { BrandId = 3, BrandName = "Audi", CarId = 3, ServiceId = 3 },
                new Brand { BrandId = 4, BrandName = "Opel", CarId = 5, ServiceId = 2 },
                new Brand { BrandId = 1, BrandName = "BMW", CarId = 4, ServiceId = 1 }
                );
            modelBuilder.Entity<Car>().HasData(
                new Car { CarId = 1, CarName = "E60"},
                new Car { CarId = 2, CarName = "S class" },
                new Car { CarId = 3, CarName = "A8" },
                new Car { CarId = 4, CarName = "E39" },
                new Car { CarId = 5, CarName = "Omega" }
                );
            modelBuilder.Entity<Service>().HasData(
                new Service { ServiceId = 1, ServiceName = "BMW_Budeapest" },
                new Service { ServiceId = 2, ServiceName = "opel_szervíz" },
                new Service { ServiceId = 3, ServiceName = "Lakatos.kft" },
                new Service { ServiceId = 4, ServiceName = "Benz_márkaszervíz" },
                new Service { ServiceId = 5, ServiceName = "Renault-Magyarország" }
                ) ;

            base.OnModelCreating(modelBuilder);
        }
    }
}

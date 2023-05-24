using Moq;
using NUnit.Framework;
using RP7XMC_HFT_2022232.Logic;
using RP7XMC_HFT_2022232.Models;
using RP7XMC_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RP7XMC_HFT_2022232.Test
{
    [TestFixture]
    public class Test
    {
        BrandLogic brandLogic;
        CarLogic carLogic;
        ServiceLogic serviceLogic;
        Mock<IRepository<Brand>> mockBrandRepo;
        Mock<IRepository<Car>> mockCarRepo;
        Mock<IRepository<Service>> mockServiceRepo;

        [SetUp]
        public void Init()
        {
            mockBrandRepo = new Mock<IRepository<Brand>>();
            mockCarRepo = new Mock<IRepository<Car>>();
            mockServiceRepo = new Mock<IRepository<Service>>();

            mockBrandRepo.Setup(m => m.ReadAll()).Returns(new List<Brand>()
            {
                new Brand {BrandId = 1, BrandName = "BMW",CarId =1 ,ServiceId =1,MaintenanceCost = 200000},
                new Brand { BrandId = 2, BrandName = "MercededsBenz", CarId = 2, ServiceId = 4, MaintenanceCost = 400000 },
                new Brand { BrandId = 3, BrandName = "Audi", CarId = 3, ServiceId = 3, MaintenanceCost = 300000 },
                new Brand { BrandId = 4, BrandName = "Opel", CarId = 5, ServiceId = 2 , MaintenanceCost = 50000 },
                new Brand { BrandId = 1, BrandName = "BMW", CarId = 4, ServiceId = 1 , MaintenanceCost = 300000 }
            }.AsQueryable());
            mockCarRepo.Setup(m => m.ReadAll()).Returns(new List<Car>()
            {
                new Car { CarId = 1, CarName = "E60"},
                new Car { CarId = 2, CarName = "S class" },
                new Car { CarId = 3, CarName = "A8" },
                new Car { CarId = 4, CarName = "E39" },
                new Car { CarId = 5, CarName = "Omega" }
            }.AsQueryable());
            mockServiceRepo.Setup(m => m.ReadAll()).Returns(new List<Service>()
            {
                new Service { ServiceId = 1, ServiceName = "BMW_Budeapest" },
                new Service { ServiceId = 2, ServiceName = "opel_szervíz" },
                new Service { ServiceId = 3, ServiceName = "Lakatos.kft" },
                new Service { ServiceId = 4, ServiceName = "Benz_márkaszervíz" },
                new Service { ServiceId = 5, ServiceName = "Renault-Magyarország" }
            }.AsQueryable());

            brandLogic = new BrandLogic(mockBrandRepo.Object,mockCarRepo.Object,mockServiceRepo.Object);
            carLogic = new CarLogic(mockCarRepo.Object);
            serviceLogic = new ServiceLogic(mockServiceRepo.Object);
        }
    }
}

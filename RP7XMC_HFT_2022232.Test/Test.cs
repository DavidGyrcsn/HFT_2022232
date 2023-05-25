using Moq;
using NUnit.Framework;
using RP7XMC_HFT_2022232.Logic;
using RP7XMC_HFT_2022232.Models;
using RP7XMC_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
            carLogic = new CarLogic(mockBrandRepo.Object, mockCarRepo.Object, mockServiceRepo.Object);
            serviceLogic = new ServiceLogic(mockServiceRepo.Object);  
        }
        [Test]
        public void TestCreateBrand()
        {
            var brnd = new Brand() { BrandId = 1, BrandName = "BMW", CarId = 1, ServiceId = 1, MaintenanceCost = 200000 };
            //Act
            brandLogic.Create(brnd);
            //Assert
            mockBrandRepo.Verify(r => r.Create(brnd), Times.Once);
        }
        [Test]
        public void TestCreateCar()
        {
            var cr = new Car { CarId = 1, CarName = "E60" };
            //Act
            carLogic.Create(cr);
            //Assert
            mockCarRepo.Verify(r => r.Create(cr), Times.Once);
        }
        [Test]
        public void TestCreateService()
        {
            var ser = new Service { ServiceId = 5, ServiceName = "Renault-Magyarország" };
            //Act
            serviceLogic.Create(ser);
            //Assert
            mockServiceRepo.Verify(r => r.Create(ser), Times.Once);
        }
        [Test]
        public void TestBrandDelete()
        {
            // Act
            brandLogic.Delete(1);

            // Assert
            mockBrandRepo
                .Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void TestCarDelete()
        {
            // Act
            carLogic.Delete(1);

            // Assert
            mockCarRepo
                .Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void TestServiceDelete()
        {
            // Act
            serviceLogic.Delete(1);

            // Assert
            mockServiceRepo
                .Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void McUnder()
        {
            //Act
            int result = brandLogic.MCUnder(75000);
            //Assert
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void McAbowe()
        {
            //Act
            int result = brandLogic.MCAbowe(75000);
            //Assert
            Assert.That(result, Is.EqualTo(4));
        }
        //[Test]
        //[TestCase(new Object[] { "E60" })]
        public void CarReturnByBrand(string carname)
        {
            //Act
            var result = carLogic.CarReturnByBrand(carname);
            //Assert
            var expected = new List<Brand>()
            {
                new Brand {BrandId = 1, BrandName = "BMW",CarId =1 ,ServiceId =1,MaintenanceCost = 200000},
                new Brand { BrandId = 1, BrandName = "BMW", CarId = 4, ServiceId = 1 , MaintenanceCost = 300000 }
            };
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void HighestCost()
        {
            //Act
            var result = brandLogic.HighestCost();
            //Assert
            string[] expected = new string[] { "S class" };
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void LowestCost()
        {
            //Act
            var result = brandLogic.LowestCost();
            //Assert
            string[] expected = new string[] { "Omega" };
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void AverageCostForAllBrands()
        {
            //Act
            var result = brandLogic.AverageCostForAllBrands();
            //Assert
            string[] expected = new string[] { "250000" };
            Assert.That(result, Is.EqualTo(expected));
        }
        
       
    }
}

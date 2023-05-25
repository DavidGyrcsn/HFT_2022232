using RP7XMC_HFT_2022232.Models;
using RP7XMC_HFT_2022232.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RP7XMC_HFT_2022232.Logic
{
    public class BrandLogic : IBrandLogic
    {
        IRepository<Brand> repo;
        IRepository<Car> cars;
        IRepository<Service> services;

        public BrandLogic(IRepository<Brand> repo, IRepository<Car> cars,IRepository<Service> services)
        {
            this.repo = repo;
            this.cars = cars;   
            this.services = services;
        }

        public void Create(Brand item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Brand Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Brand> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Brand item)
        {
            this.repo.Update(item);
        }

        public IEnumerable<string> HighestCost()
        {
            var groupedfirstdata = (from brand in repo.ReadAll()

                                    orderby brand.MaintenanceCost descending
                                    select brand.CarId).Take(1).FirstOrDefault();

            return from car in cars.ReadAll() where car.CarId == groupedfirstdata select car.CarName;
        }

        public IEnumerable<string> LowestCost()
        {
            var groupedfirstdata = (from brand in repo.ReadAll()

                                    orderby brand.MaintenanceCost ascending
                                    select brand.CarId).Take(1).FirstOrDefault();

            return from car in cars.ReadAll() where car.CarId == groupedfirstdata select car.CarName;
        }

        public IEnumerable<string> AverageCostForAllBrands()
        {
            var brands = this.repo.ReadAll();
            double averageCost = brands.Average(t => t.MaintenanceCost);
            yield return averageCost.ToString();
        }

        public IEnumerable<string> AlphabeticOrder()
        {
            var groupedFirstData = (from brand in repo.ReadAll()
                                    orderby brand.BrandName ascending
                                    select brand.BrandName.ToLower());

            return from brand in repo.ReadAll()
                   where groupedFirstData.Contains(brand.BrandName.ToLower())
                   join car in cars.ReadAll() on brand.CarId equals car.CarId
                   select car.CarName;
        }
        public IEnumerable<int> MaintenanceCostUnder(int cost)
        {
            var brands = this.repo.ReadAll();

            int count = brands.Count(t => t.MaintenanceCost < cost);

            yield return count;
        }
        public int MCUnder(int cost)
        {
            return this.repo
                .ReadAll()
                .Where(t => t.MaintenanceCost < cost)
                .Count();
        }
       
    }
}

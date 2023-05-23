using RP7XMC_HFT_2022232.Models;
using RP7XMC_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP7XMC_HFT_2022232.Logic
{
    public class CarLogic : ICarLogic
    {
        IRepository<Car> repo;

        public CarLogic(IRepository<Car> repo)
        {
            this.repo = repo;
        }

        public void Create(Car item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Car Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Car> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Car item)
        {
            this.repo.Update(item);
        }
    }
}

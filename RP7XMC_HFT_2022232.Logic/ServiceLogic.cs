using RP7XMC_HFT_2022232.Models;
using RP7XMC_HFT_2022232.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP7XMC_HFT_2022232.Logic
{
    public class ServiceLogic : IServiceLogic
    {
        IRepository<Service> repo;

        public ServiceLogic(IRepository<Service> repo)
        {
            this.repo = repo;
        }

        public void Create(Service item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Service Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Service> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Service item)
        {
            this.repo.Update(item);
        }
    }
}

using RP7XMC_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP7XMC_HFT_2022232.Logic
{
    public interface IServiceLogic
    {
        IQueryable<Service> ReadAll();
        Service Read(int id);
        void Create(Service item);
        void Update(Service item);
        void Delete(int id);
    }
}

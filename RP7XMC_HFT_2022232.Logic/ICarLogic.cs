using RP7XMC_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP7XMC_HFT_2022232.Logic
{
    public interface ICarLogic
    {
        IQueryable<Car> ReadAll();
        Car Read(int id);
        void Create(Car item);
        void Update(Car item);
        void Delete(int id);
    }
}

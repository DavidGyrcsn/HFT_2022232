using RP7XMC_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP7XMC_HFT_2022232.Logic
{
    public interface IBrandLogic
    {
        IQueryable<Brand> ReadAll();
        Brand Read(int id);
        void Create(Brand item);
        void Update(Brand item);
        void Delete(int id);
        public IEnumerable<string> HighestCost();
        public IEnumerable<string> LowestCost();
        public IEnumerable<string> AverageCostForAllBrands();
        public IEnumerable<int> MaintenanceCostUnder(int cost);
        public IEnumerable<int> MaintenanceCostAbowe(int cost);
        public int MCUnder(int cost);
        public int MCAbowe(int cost);
    }
}

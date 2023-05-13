using RP7XMC_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP7XMC_HFT_2022232.Repository
{
    public class CarRepository : Repository<Car>, IRepository<Car>
    {
        public CarRepository(CarDbContext ctx) : base(ctx)
        {
        }

        public override Car Read(int id)
        {
            return ctx.Cars.FirstOrDefault(t => t.CarId == id);
        }

        public override void Update(Car item)
        {
            var old = Read(item.CarId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}

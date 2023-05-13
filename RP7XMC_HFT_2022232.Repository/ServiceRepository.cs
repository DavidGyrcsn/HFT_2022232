using RP7XMC_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP7XMC_HFT_2022232.Repository
{
    public class ServiceRepository : Repository<Service>, IRepository<Service>
    {
        public ServiceRepository(CarDbContext ctx) : base(ctx)
        {
        }

        public override Service Read(int id)
        {
            return ctx.Services.FirstOrDefault(t => t.ServiceId == id);
        }

        public override void Update(Service item)
        {
            var old = Read(item.ServiceId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}

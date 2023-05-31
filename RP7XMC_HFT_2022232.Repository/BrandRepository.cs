using RP7XMC_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP7XMC_HFT_2022232.Repository
{
    public class BrandRepository : Repository<Brand>, IRepository<Brand>
    {
        public BrandRepository(CarDbContext ctx) : base(ctx)
        {
        }

        public override Brand Read(int id)
        {
            return ctx.Brands.FirstOrDefault(t => t.BrandId == id);
        }

        public override void Update(Brand item)
        {
            var old = Read(item.BrandId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if(prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;

namespace VehicleFleetDb.Repository
{
    public class DriverRepository : Repository<Driver, int>, IRepository<Driver, int>
    {
        public DriverRepository(FleetDbContext ctx) : base(ctx)
        { }

        public override Driver Read(int id)
        {
            return this.ctx.Drivers.First(t => t.DriverId == id);
        }

        public override void Update(Driver item)
        {
            var old = Read(item.DriverId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}

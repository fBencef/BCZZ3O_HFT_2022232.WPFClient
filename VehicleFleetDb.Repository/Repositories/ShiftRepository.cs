using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;

namespace VehicleFleetDb.Repository
{
    public class ShiftRepository : Repository<Shift, int>, IRepository<Shift, int>
    {
        public ShiftRepository(FleetDbContext ctx) : base(ctx)
        { }

        public override Shift Read(int id)
        {
            return this.ctx.Shifts.First(t => t.ShiftId == id);
        }

        public override void Update(Shift item)
        {
            var old = Read(item.ShiftId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}

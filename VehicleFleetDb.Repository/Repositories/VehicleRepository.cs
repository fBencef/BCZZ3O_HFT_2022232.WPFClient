using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;

namespace VehicleFleetDb.Repository
{
    public class VehicleRepository : Repository<Vehicle, string>, IRepository<Vehicle, string>
    {
        public VehicleRepository(FleetDbContext ctx) : base(ctx)
        { }

        public override Vehicle Read(string id)
        {
            return this.ctx.Vehicles.First(t => t.Registration == id);
        }

        public override void Update(Vehicle item)
        {
            var old = Read(item.Registration);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleFleetDb.Repository
{
    public abstract class Repository<T, K> : IRepository<T, K> where T : class
    {
        protected FleetDbContext ctx;
        public Repository(FleetDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public void Delete(K id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }


        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }

        public abstract T Read(K id);
        public abstract void Update(T item);
    }
}

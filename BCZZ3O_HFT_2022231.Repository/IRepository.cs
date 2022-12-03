using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleFleetDb.Repository
{
    public interface IRepository<T, K> where T : class
    {
        IQueryable<T> ReadAll();
        T Read(K id);
        void Create(T item);
        void Update(T item);
        void Delete(K id);
    }
}

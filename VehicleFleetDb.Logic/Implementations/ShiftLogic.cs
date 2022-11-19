using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;
using VehicleFleetDb.Repository;

namespace VehicleFleetDb.Logic
{
    public class ShiftLogic
    {
        IRepository<Shift, int> repository;
        public ShiftLogic(IRepository<Shift, int> repository)
        {
            this.repository = repository;
        }

        public void Create(Shift item)
        {
            if (item.Line == null || item.Tour == null) throw new ArgumentException("Invalid shift.");
            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);
        }

        public Shift Read(int id)
        {
            var shift = this.repository.Read(id);
            if (shift == null) throw new ArgumentException("Shift could not be found.");
            return shift;
        }

        public IQueryable<Shift> ReadAll()
        {
            return this.repository.ReadAll();
        }

        public void Update(Shift item)
        {
            this.repository.Update(item);
        }

        //NON-CRUDs
        public Driver GetDriver(Shift item)
        {
            return item.Driver;
        }
    }
}

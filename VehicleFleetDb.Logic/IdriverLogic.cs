using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;

namespace VehicleFleetDb.Logic
{
    public interface IDriverLogic
    {
        void Create(Driver item);
        void Delete(int id);
        Driver Read(int id);
        IEnumerable<Driver> ReadAll();
        void Update(Driver item);

        //TODO NON-CRUDS
        public double? AvgDriverAge();
        public IQueryable<Shift> ShiftsOfDriverModified(string name);
    }
}

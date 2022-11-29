using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;

namespace VehicleFleetDb.Logic
{
    public interface IVehicleLogic
    {
        void Create(Vehicle item);
        void Delete(string registration);
        Vehicle Read(string registration);
        IEnumerable<Vehicle> ReadAll();
        void Update(Vehicle item);

        //NON-CRUDS
        public IQueryable<string> ListModels(string manufacturer);
        public IQueryable<Driver> ListDrivers(string registarton);
    }
}

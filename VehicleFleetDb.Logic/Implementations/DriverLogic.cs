using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;
using VehicleFleetDb.Repository;

namespace VehicleFleetDb.Logic
{
    public class DriverLogic : IDriverLogic
    {
        IRepository<Driver, int> repository;
        public DriverLogic(IRepository<Driver, int> repository)
        {
            this.repository = repository;
        }

        public void Create(Driver item)
        {
            if (item.Age < 21) throw new ArgumentException("Driver is too young.");
            if (item.Name.Contains(' ') || item.Name.Length < 3) throw new ArgumentException("Invalid driver name.");
            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);
        }

        public Driver Read(int id)
        {
            var driver = this.repository.Read(id);
            if (driver == null) throw new ArgumentException("Driver could not be found.");
            return driver;
        }
        public Driver Read(string name)
        {
            var driver = this.repository.ReadAll().Where(t => t.Name == name);
            if (driver.ToArray().Length == 0) throw new ArgumentException("Driver could not be found.");
            return driver.FirstOrDefault();
        }

        public IQueryable<Driver> ReadAll()
        {
            return this.repository.ReadAll();
        }

        public void Update(Driver item)
        {
            this.repository.Read(item.DriverId).Name = item.Name;
        }

        //NON-CRUDs
        public double? AvgDriverAge()
        {
            return this.repository.ReadAll().Average(t => t.Age);
        }

        public IQueryable<IEnumerable<int>>  ShiftsOfDriver(string name)
        {
            var result = this.repository.ReadAll().Where(t => t.Name == name).Select(t => t.Shifts);
            return result.Select(t => t.Select(t => t.ShiftId));
            //return .Select(t => t.Select(t => t.ShiftId));
        }

        IEnumerable<Driver> IDriverLogic.ReadAll()
        {
            return this.repository.ReadAll();
        }
    }
}

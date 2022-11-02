using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;
using VehicleFleetDb.Repository;

namespace VehicleFleetDb.Logic
{
    public class DriverLogic
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

        public IQueryable<Driver> ReadAll()
        {
            return this.repository.ReadAll();
        }

        public void Update(Driver item)
        {
            this.repository.Read(item.DriverId).Name = item.Name;
        }

        //TODO >> NON-CRUD METHODS
    }
}

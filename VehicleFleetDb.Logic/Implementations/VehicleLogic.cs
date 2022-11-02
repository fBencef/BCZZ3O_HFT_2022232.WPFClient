using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;
using VehicleFleetDb.Repository;

namespace VehicleFleetDb.Logic
{
    public class VehicleLogic
    {
        IRepository<Vehicle, string> repository;
        public VehicleLogic(IRepository<Vehicle, string> repository)
        {
            this.repository = repository;
        }

        public void Create(Vehicle item)
        {
            if (item.Registration.Length != 6) throw new ArgumentException("Invalid licence plate!");
            else this.repository.Create(item);
        }

        public void Delete(string id)
        {
            this.repository.Delete(id);
        }

        public Vehicle Read(string id)
        {
            var vehicle = this.repository.Read(id);
            if (vehicle == null) throw new ArgumentException("Vehicle could not be found.");
            return vehicle;
        }

        public IQueryable<Vehicle> ReadAll()
        {
            return this.repository.ReadAll();
        }

        public void Update(Vehicle item)
        {
            this.repository.Read(item.Registration).Registration = item.DisplayReg;
        }

        //TODO >> NON-CRUD METHODS
    }
}

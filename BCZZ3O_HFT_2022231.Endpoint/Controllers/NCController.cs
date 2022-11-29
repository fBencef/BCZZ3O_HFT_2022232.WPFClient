using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VehicleFleetDb.Logic;
using VehicleFleetDb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BCZZ3O_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NCController : ControllerBase
    {
        IVehicleLogic vehicleLogic;
        IDriverLogic driverLogic;
        IShiftLogic shiftLogic;

        public NCController(IVehicleLogic vLogic, IDriverLogic dLogic, IShiftLogic sLogic)
        {
            this.vehicleLogic = vLogic;
            this.driverLogic = dLogic;
            this.shiftLogic = sLogic;
        }

        [HttpGet]
        public double? AvgDriverAge()
        {
            return this.driverLogic.AvgDriverAge();
        }

        [HttpGet("{name}")]
        public IQueryable<Shift> ShiftsOfDriver(string name)
        { 
            return this.driverLogic.ShiftsOfDriverModified(name);
        }

        [HttpGet("{manufacturer}")]
        public IQueryable<string> ListModels(string manufacturer)
        {
            return this.vehicleLogic.ListModels(manufacturer);
        }
        [HttpGet("{registration}")]
        public IQueryable<Driver> ListDrivers(string registarton)
        {
            return this.vehicleLogic.ListDrivers(registarton);
        }

        [HttpGet("shiftId")]
        public Driver GetDriver(int shiftId)
        {
            return this.shiftLogic.GetDriver(shiftId);
        }
    }
}

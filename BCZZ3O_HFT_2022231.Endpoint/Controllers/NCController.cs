using BCZZ3O_HFT_2022231.Endpoint.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        IHubContext<SignalRHub> hub;

        public NCController(IVehicleLogic vLogic, IDriverLogic dLogic, IShiftLogic sLogic, IHubContext<SignalRHub> hub)
        {
            this.vehicleLogic = vLogic;
            this.driverLogic = dLogic;
            this.shiftLogic = sLogic;
            this.hub = hub;
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
        public IQueryable<Driver> ListDrivers(string registration)
        {
            return this.vehicleLogic.ListDrivers(registration);
        }

        [HttpGet("{shiftId}")]
        public Driver GetDriver(int shiftId)
        {
            return this.shiftLogic.GetDriver(shiftId);
        }

        [HttpGet("{line}")]
        public IQueryable<Vehicle> VehiclesOnLine(string line)
        {
            return this.shiftLogic.VehiclesOnLine(line);
        }

        [HttpGet("{line}")]
        public IQueryable<int> LengthOfVehiclesOnLine(string line)
        {
            return this.shiftLogic.LengthOfVehiclesOnLine(line);
        }
    }
}

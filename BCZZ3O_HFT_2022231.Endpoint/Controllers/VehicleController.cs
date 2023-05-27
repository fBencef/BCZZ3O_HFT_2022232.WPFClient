using BCZZ3O_HFT_2022231.Endpoint.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using VehicleFleetDb.Logic;
using VehicleFleetDb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BCZZ3O_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        IVehicleLogic logic;
        IHubContext<SignalRHub> hub;

        public VehicleController(IVehicleLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<VehicleController>
        [HttpGet]
        public IEnumerable<Vehicle> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<VehicleController>/5
        [HttpGet("{registration}")]
        public Vehicle Read(string registration)
        {
            return this.logic.Read(registration);
        }

        // POST api/<VehicleController>
        [HttpPost]
        public void Create([FromBody] Vehicle value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("VehicleCreated", value);
        }

        // PUT api/<VehicleController>/5
        [HttpPut]
        public void Update([FromBody] Vehicle value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("VehicleUpdated", value);
        }

        // DELETE api/<VehicleController>/5
        [HttpDelete("{registration}")]
        public void Delete(string registration)
        {
            var vehicleToDelete = this.logic.Read(registration);
            this.logic.Delete(registration);
            this.hub.Clients.All.SendAsync("VehicleDeleted", vehicleToDelete);
        }
    }
}

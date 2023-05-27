using BCZZ3O_HFT_2022231.Endpoint.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using VehicleFleetDb.Logic;
using VehicleFleetDb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BCZZ3O_HFT_2022231.Endpoint.Controllers
{    
    [Route("[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        IShiftLogic logic;
        IHubContext<SignalRHub> hub;

        public ShiftController(IShiftLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        // GET: api/<ShiftController>
        [HttpGet]
        public IEnumerable<Shift> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<ShiftController>/5
        [HttpGet("{id}")]
        public Shift Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<ShiftController>
        [HttpPost]
        public void Create([FromBody] Shift value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("ShiftCreated", value);
        }

        // PUT api/<ShiftController>/5
        [HttpPut]
        public void Update([FromBody] Shift value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("ShiftUpdated", value);
        }

        // DELETE api/<ShiftController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var shiftToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("ShiftDeleted", shiftToDelete);
        }
    }
}

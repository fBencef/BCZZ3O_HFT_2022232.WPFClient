using Microsoft.AspNetCore.Mvc;
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

        public VehicleController(IVehicleLogic logic)
        {
            this.logic = logic;
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
        }

        // PUT api/<VehicleController>/5
        [HttpPut]
        public void Update([FromBody] Vehicle value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<VehicleController>/5
        [HttpDelete("{registration}")]
        public void Delete(string registration)
        {
            this.logic.Delete(registration);
        }
    }
}

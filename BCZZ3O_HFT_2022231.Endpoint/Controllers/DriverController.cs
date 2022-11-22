using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VehicleFleetDb.Logic;
using VehicleFleetDb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BCZZ3O_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        IDriverLogic logic;

        public DriverController(IDriverLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<DriverController>
        [HttpGet]
        public IEnumerable<Driver> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<DriverController>/5
        [HttpGet("{id}")]
        public Driver Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<DriverController>
        [HttpPost]
        public void Create([FromBody] Driver value)
        {
            this.logic.Create(value);
        }

        // PUT api/<DriverController>/5
        [HttpPut]
        public void Update([FromBody] Driver value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<DriverController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}

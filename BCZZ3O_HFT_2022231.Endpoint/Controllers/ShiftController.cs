using Microsoft.AspNetCore.Mvc;
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

        public ShiftController(IShiftLogic logic)
        {
            this.logic = logic;
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
        }

        // PUT api/<ShiftController>/5
        [HttpPut]
        public void Update([FromBody] Shift value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<ShiftController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleFleetDb.Models
{
    public class Shift
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ShiftId { get; set; }
        [StringLength(240)]
        public string FromYard { get; set; }
        public DateTime Date { get; set; }
        [StringLength(4)]
        public string Line { get; set; }
        [StringLength(4)]
        public string Tour { get; set; }
        [ForeignKey(nameof(Driver))]
        public int DriverId { get; set; }
        [ForeignKey(nameof(Vehicle))]
        public string VehicleId { get; set; }

        public Shift()
        {

        }

        public Shift(string line)
        {
            string[] parts = line.Split('#');
            ShiftId = int.Parse(parts[0]);
            FromYard = parts[1];
            Date = DateTime.Parse(parts[2]);
            Line = parts[3];
            Tour = parts[4];
            DriverId = int.Parse(parts[5]);
            VehicleId = parts[6];
        }
    }
}

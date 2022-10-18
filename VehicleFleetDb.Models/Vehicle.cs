using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleFleetDb.Models
{
    public class Vehicle
    {
        [Key]
        [Required]
        [StringLength(6)]
        public string Registration { get; set; }
        [StringLength(240)]
        public string Manufacturer { get; set; }
        [StringLength(240)]
        public string Model { get; set; }
        [Range(10, 20)]
        public int Length { get; set; }
        public DateTime RegistrationDate { get; set; }

        public Vehicle()
        {

        }

        public Vehicle(string line)
        {
            string[] parts = line.Split('#');
            Registration = parts[0];
            Manufacturer = parts[1];
            Model = parts[2];
            Length = int.Parse(parts[3]);
            RegistrationDate = DateTime.Parse(parts[4]);
        }
    }
}

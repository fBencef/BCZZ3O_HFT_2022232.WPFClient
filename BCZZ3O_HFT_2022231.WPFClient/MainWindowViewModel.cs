using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;

namespace BCZZ3O_HFT_2022231.WPFClient
{
    public class MainWindowViewModel
    {
        public RestCollection<Vehicle> Vehicles { get; set; }
        public MainWindowViewModel()
        {
            Vehicles = new RestCollection<Vehicle>("http://localhost:47322", "vehicle"); 
        }
    }
}

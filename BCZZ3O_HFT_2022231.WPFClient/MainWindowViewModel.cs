using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VehicleFleetDb.Models;

namespace BCZZ3O_HFT_2022231.WPFClient
{
    public class MainWindowViewModel
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public RestCollection<Vehicle> Vehicles { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Vehicles = new RestCollection<Vehicle>("http://localhost:47322/", "vehicle");
            }
        }
    }
}

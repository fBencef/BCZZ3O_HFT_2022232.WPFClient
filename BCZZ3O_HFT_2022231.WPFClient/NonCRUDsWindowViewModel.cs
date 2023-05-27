using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using VehicleFleetDb.Models;
using System.Xml.Linq;
using System.Windows.Navigation;
using System.Collections.ObjectModel;

namespace BCZZ3O_HFT_2022231.WPFClient
{
    public class NonCRUDsWindowViewModel : ObservableRecipient
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public RestCollection<Shift> Shifts { get; set; }
        public RestCollection<Driver> Drivers { get; set; }
        public RestCollection<Vehicle> Vehicles { get; set; }
        static RestService Rest;

        public double AverageDriverAge { get; set; }
        public string SelectedDriverName { get; set; }
        public string SelectedManufactureName { get; set; }
        public string SelectedRegistration { get; set; }
        public string SelectedLine { get; set; }
        public ObservableCollection<Shift> ShiftsOfDriver { get; set; }
        public ObservableCollection<string> ModelsOfManufacturer { get; set; }
        public ObservableCollection<Driver> DriversOfVehicle { get; set; }
        public ObservableCollection<Vehicle> VehiclesResults { get; set; }



        public ICommand GetShiftsOfDriverCommand { get; set; }
        public ICommand GetModelsOfManufacturerCommand { get; set; }
        public ICommand GetDriversOfVehicleCommand { get; set; }
        public ICommand GetVehiclesOnALineCommand { get; set; }

        public NonCRUDsWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                Shifts = new RestCollection<Shift>("http://localhost:47322/", "shift", "hub");
                Drivers = new RestCollection<Driver>("http://localhost:47322/", "driver", "hub");
                Vehicles = new RestCollection<Vehicle>("http://localhost:47322/", "vehicle", "hub");
                Rest = new RestService("http://localhost:47322/");

                AverageDriverAge = Math.Round(Rest.GetSingle<double>("NC/AvgDriverAge"), 2);

                GetShiftsOfDriverCommand = new RelayCommand(() =>
                {
                    ShiftsOfDriver = Rest.GetObservable<Shift>($"NC/ShiftsOfDriver/{SelectedDriverName}");
                    ResultsWindow rw = new ResultsWindow(ShiftsOfDriver);
                    rw.Show();
                });

                GetModelsOfManufacturerCommand = new RelayCommand(() =>
                {
                    ModelsOfManufacturer = Rest.GetObservable<string>($"NC/ListModels/{SelectedManufactureName}");
                    ResultsWindow rw = new ResultsWindow(ModelsOfManufacturer);
                    rw.Show();
                });
                GetDriversOfVehicleCommand = new RelayCommand(() =>
                {
                    DriversOfVehicle = Rest.GetObservable<Driver>($"/NC/ListDrivers/{SelectedRegistration}");
                    ResultsWindow rw = new ResultsWindow(DriversOfVehicle);
                    rw.Show();
                });
                GetVehiclesOnALineCommand = new RelayCommand(() =>
                {
                    VehiclesResults = Rest.GetObservable<Vehicle>($"NC/VehiclesOnLine/{SelectedLine}");
                    ResultsWindow rw = new ResultsWindow(VehiclesResults);
                    rw.Show();
                });

                //GetLengthOfVehicles nincs, mert bele lett vonva a GetVehiclesbe
            }
        }
    }
}

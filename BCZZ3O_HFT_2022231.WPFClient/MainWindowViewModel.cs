using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VehicleFleetDb.Models;

namespace BCZZ3O_HFT_2022231.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
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
        private Vehicle selectedVehicle;

        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set { SetProperty(ref selectedVehicle, value);
                (DeleteVehicleCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateVehicleCommand { get; set; }
        public ICommand DeleteVehicleCommand { get; set; }
        public ICommand UpdateVehicleCommand { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Vehicles = new RestCollection<Vehicle>("http://localhost:47322/", "vehicle");

                CreateVehicleCommand = new RelayCommand(() =>
                {
                    Vehicles.Add(new Vehicle()
                    {
                        Registration = "AAA111",
                        DisplayReg = "AAA111",
                        Manufacturer = "aaa",
                        Model = "bbb",
                        Length = 12,
                        RegistrationDate = DateTime.Now
                    });
                });

            DeleteVehicleCommand = new RelayCommand(() =>
            {
                Vehicles.Delete<string>(SelectedVehicle.Registration);
            },
            () =>
            {
                return SelectedVehicle != null;
            }
            );
            }
        }
    }
}

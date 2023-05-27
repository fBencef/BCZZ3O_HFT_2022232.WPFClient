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

namespace BCZZ3O_HFT_2022231.WPFClient
{
    public class EditShiftWindowViewModel : ObservableRecipient
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
        private Shift selectedShift;

        public Shift SelectedShift
        {
            get { return selectedShift; }
            set
            {
                if (value != null)
                {
                    selectedShift = new Shift()
                    {
                        ShiftId = value.ShiftId,
                        FromYard = value.FromYard,
                        Date = value.Date,
                        Line = value.Line,
                        Tour = value.Tour,
                        DriverId = value.DriverId,
                        VehicleId = value.VehicleId,
                        Driver = value.Driver,
                        Vehicle = value.Vehicle
                    };
                }
                OnPropertyChanged();
                (DeleteShiftCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateShiftCommand { get; set; }
        public ICommand DeleteShiftCommand { get; set; }
        public ICommand UpdateShiftCommand { get; set; }

        public EditShiftWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                Shifts = new RestCollection<Shift>("http://localhost:47322/", "shift", "hub");
                Drivers = new RestCollection<Driver>("http://localhost:47322/", "driver", "hub");
                Vehicles = new RestCollection<Vehicle>("http://localhost:47322/", "vehicle", "hub");

                CreateShiftCommand = new RelayCommand(() =>
                {
                    Shifts.Add(new Shift()
                    {
                        ShiftId = SelectedShift.ShiftId,
                        FromYard = SelectedShift.FromYard,
                        Date = SelectedShift.Date,
                        Line = SelectedShift.Line,
                        Tour = SelectedShift.Tour,
                        DriverId = SelectedShift.DriverId,
                        VehicleId = SelectedShift.VehicleId,
                        Driver = Drivers.First(t => t.DriverId == SelectedShift.DriverId),
                        Vehicle = Vehicles.First(t => t.Registration == SelectedShift.VehicleId),
                    });
                });

                UpdateShiftCommand = new RelayCommand(() =>
                {
                    //NOT WORKING
                    Shifts.Update(SelectedShift);
                });

                DeleteShiftCommand = new RelayCommand(() =>
                {
                    Shifts.Delete<int>(SelectedShift.ShiftId);
                },
                () =>
                {
                    return SelectedShift != null;
                }
                );
            }
            SelectedShift = new Shift();
        }
    }
}

using Castle.Core.Internal;
using ConsoleTools;
using System;
using System.Linq;
using System.Transactions;
using VehicleFleetDb.Logic;
using VehicleFleetDb.Models;
using VehicleFleetDb.Repository;

namespace VehicleFleetDb.Client
{
    internal class Program
    {
        static VehicleLogic vehicleLogic;
        static DriverLogic driverLogic;
        static ShiftLogic shiftLogic;

        static void Create(string entity)
        {
            //DRIVER 
            if (entity == "Driver")
            {
                Console.WriteLine("Format\nNAME#AGE");
                string item = Console.ReadLine();
                driverLogic.Create(new Models.Driver(item));
                Console.WriteLine("Driver created succesfully.");
            }
            //VEHICLE
            if (entity == "Vehicle")
            {
                Console.WriteLine("Format\nREG#MANUFACT#MODEL#LENGTH#REG.DATE(YYYY-MM-DD)");
                string item = Console.ReadLine();
                vehicleLogic.Create(new Models.Vehicle(item));
                Console.WriteLine("Vehicle created succesfully.");
            }
            //SHIFT
            if (entity == "Shift")
            {
                Console.WriteLine("Format\nYARD#DATE(YYY-MM-DD)#LINE#TOUR#DRIVER ID#VEHICLE REG");
                string item = Console.ReadLine();
                shiftLogic.Create(new Models.Shift(item));
                Console.WriteLine("Shift created succesfully.");
            }
            WaitForReturn();

        }
        static void List(string entity)
        {
            //DRIVER 
            if (entity == "Driver")
            {
                var items = driverLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Age" + "\t" + "Name");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.DriverId}\t{item.Age}\t{item.Name}");
                }
            }
            //VEHICLE
            if (entity == "Vehicle")
            {
                var items = vehicleLogic.ReadAll();
                Console.WriteLine($"Reg.\tManufacturer\tModel\t\tLenght\tRegistration Date");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Registration}\t{item.Manufacturer}\t{item.Model}\t{item.Length} m\t{item.RegistrationDate}");
                }
            }
            //SHIFT
            if (entity == "Shift")
            {
                var items = shiftLogic.ReadAll();
                Console.WriteLine($"Id\tYard\t\tDate\t\tLine\tTour\tDriver\tVehicle");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.ShiftId}\t{item.FromYard}\t{item.Date.ToShortDateString()}\t{item.Line}\t{item.Tour}\t{item.DriverId}\t{item.VehicleId}");
                }
            }
            WaitForReturn();
        }
        static void Update(string entity)
        {
            //DRIVER 
            if (entity == "Driver")
            {
                Console.Write("Enter driver ID: ");
                int id = int.Parse(Console.ReadLine());
                var updt = driverLogic.Read(id);
                Console.Write($"New name of {updt.Name}: ");
                string newName = Console.ReadLine();
                updt.Name = newName;

                driverLogic.Update(updt);
            }
            //VEHICLE
            if (entity == "Vehicle")
            {
                Console.Write("Enter vehicle registration (ABC123): ");
                string reg = Console.ReadLine().ToUpper();
                var updt = vehicleLogic.Read(reg);
                Console.Write($"New registartion of {updt.Registration}: ");
                string newReg = Console.ReadLine().ToUpper();
                updt.DisplayReg = newReg;

                vehicleLogic.Update(updt);
            }
            //SHIFT
            if (entity == "Shift")
            {
                Console.Write("Enter shift ID: ");
                int id = int.Parse(Console.ReadLine());
                var updt = shiftLogic.Read(id);
                Console.Write($"New >line< of shift #{updt.ShiftId}: ");
                string newLine = Console.ReadLine();
                Console.Write($"New >tour< of shift #{updt.ShiftId}: ");
                string newTour = Console.ReadLine();
                updt.Line = newLine;
                updt.Tour = newTour;

                shiftLogic.Update(updt);
            }
        }
        static void Delete(string entity)
        {
            //DRIVER 
            if (entity == "Driver")
            {
                Console.Write("Enter driver ID: ");
                int id = int.Parse(Console.ReadLine());
                string name = driverLogic.Read(id).Name;
                driverLogic.Delete(id);
                Console.WriteLine($"\nDriver {name} deleted. Press any key to continue.");
                Console.ReadKey();

            }
            //VEHICLE
            if (entity == "Vehicle")
            {
                Console.Write("Enter registration (ABC123): ");
                string reg = Console.ReadLine().ToUpper();
                var vehicle = vehicleLogic.Read(reg);
                vehicleLogic.Delete(reg);
                Console.WriteLine($"\nVehicle {vehicle.Registration} deleted. Press any key to continue.");
                Console.ReadKey();
            }
            //SHIFT
            if (entity == "Shift")
            {
                Console.Write("Enter shift ID: ");
                int id = int.Parse(Console.ReadLine());
                var shift = shiftLogic.Read(id);
                shiftLogic.Delete(id);
                Console.WriteLine($"\nShift {shift.Line}/{shift.Tour} deleted. Press any key to continue.");
                Console.ReadKey();
            }
        }

        static void ShiftListWithDriverNames()
        {
            var items = shiftLogic.ReadAll();
            Console.WriteLine($"Id\tYard\t\tDate\t\tLine\tTour\tDriver Name\t\tVehicle");
            foreach (var item in items)
            {
                string name = shiftLogic.GetDriver(item).Name;
                Console.WriteLine($"{item.ShiftId}\t{item.FromYard}\t{item.Date.ToShortDateString()}\t{item.Line}\t{item.Tour}\t{name}\t\t{item.VehicleId}");
            }
            WaitForReturn();
        }
        static void DriverAvgAge()
        {
            Console.WriteLine($"The average age of drivers is: {Math.Round((double)driverLogic.AvgDriverAge(), 2)} years.");
            WaitForReturn();
        }
        static void DriverShiftsModified()
        {
            Console.Write("Enter driver's name: ");
            string name = Console.ReadLine();
            if (driverLogic.Read(name) != null)
            {
                var shifts = driverLogic.ShiftsOdDriverModified(name);
                if (!shifts.IsNullOrEmpty())
                {
                    Console.WriteLine($"Driver {name} has the following shifts: ");
                    foreach (var item in shifts)
                    {
                        Console.WriteLine($"{item.Line}/{item.Tour}");
                    }
                }
                else Console.WriteLine("This driver has no active shifts.");
            }
            WaitForReturn();
        }
        static void VehicleListModels()
        {
            Console.Write("Enter a manufacturer: ");
            string manufacturer = Console.ReadLine();
            var result = vehicleLogic.ListModels(manufacturer);
            if (result.ToArray().Length > 0)
            {
                Console.WriteLine($"\nThe manufacturer {manufacturer} has the following models in the fleet:");
                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
            }
            else Console.WriteLine($"\nThe manufacturer \"{manufacturer}\" does not exist or has no models in the fleet.");
            WaitForReturn();
        }

        private static void WaitForReturn()
        {
            Console.WriteLine("\nPress any key to return.");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            var ctx = new FleetDbContext();

            var vehicleRepo = new VehicleRepository(ctx);
            var driverRepo = new DriverRepository(ctx);
            var shiftRepo = new ShiftRepository(ctx);

            vehicleLogic = new VehicleLogic(vehicleRepo);
            driverLogic = new DriverLogic(driverRepo);
            shiftLogic = new ShiftLogic(shiftRepo);

            var vehicleSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Vehicle"))
                .Add("Create", () => Create("Vehicle"))
                .Add("Update", () => Update("Vehicle"))
                .Add("Delete", () => Delete("Vehicle"))
                .Add("List Models of a Manufacturer", () => VehicleListModels())
                .Add("Back", ConsoleMenu.Close);

            var driverSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Driver"))
                .Add("Create", () => Create("Driver"))
                .Add("Update", () => Update("Driver"))
                .Add("Delete", () => Delete("Driver"))
                .Add("Average Age", () => DriverAvgAge())
                .Add("Drives On...", () => DriverShiftsModified())
                .Add("Back", ConsoleMenu.Close);

            var shiftSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Shift"))
                .Add("Create", () => Create("Shift"))
                .Add("Update", () => Update("Shift"))
                .Add("Delete", () => Delete("Shift"))
                .Add("List With Driver Names", () => ShiftListWithDriverNames())
                .Add("Back", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Vehicles", () => vehicleSubMenu.Show())
                .Add("Drivers", () => driverSubMenu.Show())
                .Add("Shifts", () => shiftSubMenu.Show())
                .Add("Exit", () => Environment.Exit(0));

            menu.Show();
        }
    }
}

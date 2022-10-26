using ConsoleTools;
using System;
using System.Linq;
using VehicleFleetDb.Logic;
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

        }
        static void List(string entity)
        {
            //DRIVER 
            if (entity == "Driver")
            {
                var items = driverLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in items)
                {
                    Console.WriteLine(item.DriverId + "\t" + item.Name);
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

            Console.ReadLine();
        }
        static void Update(string entity)
        {
            //TODO
            Console.WriteLine(entity + "update");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            //TODO
            Console.WriteLine(entity + "delete");
            Console.ReadLine();
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
                .Add("Delete", () => Delete("Vehicle"))
                .Add("Update", () => Update("Vehicle"))
                .Add("Exit", () => ConsoleMenu.Close());

            var driverSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Driver"))
                .Add("Create", () => Create("Driver"))
                .Add("Delete", () => Delete("Driver"))
                .Add("Update", () => Update("Driver"))
                .Add("Exit", () => ConsoleMenu.Close());

            var shiftSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Shift"))
                .Add("Create", () => Create("Shift"))
                .Add("Delete", () => Delete("Shift"))
                .Add("Update", () => Update("Shift"))
                .Add("Exit", () => ConsoleMenu.Close());

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Vehicles", () => vehicleSubMenu.Show())
                .Add("Drivers", () => driverSubMenu.Show())
                .Add("Shifts", () => shiftSubMenu.Show())
                .Add("Exit", () => ConsoleMenu.Close());

            menu.Show();
        }
    }
}

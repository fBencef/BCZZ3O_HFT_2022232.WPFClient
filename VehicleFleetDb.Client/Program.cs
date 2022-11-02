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

        }
        static void List(string entity)
        {
            //DRIVER 
            if (entity == "Driver")
            {
                var items = driverLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Age" +"\t"+ "Name");
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
            Console.WriteLine("\nPress any key to return.");
            Console.ReadKey();
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

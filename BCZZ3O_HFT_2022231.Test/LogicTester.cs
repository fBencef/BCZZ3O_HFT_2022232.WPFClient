using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleFleetDb.Logic;
using VehicleFleetDb.Models;
using VehicleFleetDb.Repository;

namespace BCZZ3O_HFT_2022231.Test
{
    [TestFixture]
    public class LogicTester
    {
        VehicleLogic vehicleLogic;
        DriverLogic driverLogic;
        ShiftLogic shiftLogic;

        Mock<IRepository<Vehicle, string>> mockVehicleRepo;
        Mock<IRepository<Driver, int>> mockDriverRepo;
        Mock<IRepository<Shift, int>> mockShiftRepo;

        [SetUp]
        public void Init()
        {
            mockVehicleRepo = new Mock<IRepository<Vehicle, string>>();
            mockDriverRepo = new Mock<IRepository<Driver, int>>();
            mockShiftRepo = new Mock<IRepository<Shift, int>>();

            //data
            Vehicle veh1 = new Vehicle("AAA111#VehManu1#VehMake1#12#2014-05-01");
            Vehicle veh2 = new Vehicle("BBB222#VehManu1#VehMake2#12#2014-05-01");
            Vehicle veh3 = new Vehicle("CCC333#VehManu2#VehMake3#18#2014-05-01");
            Vehicle veh4 = new Vehicle("DDD444#VehManu2#VehMake4#18#2014-05-01");
            Driver dr1 = new Driver() { DriverId = 1, Name = "Driver1", Age = 30 };
            Driver dr2 = new Driver() { DriverId = 2, Name = "Driver2", Age = 35 };
            Driver dr3 = new Driver() { DriverId = 3, Name = "Driver3", Age = 40 };
            Driver dr4 = new Driver() { DriverId = 4, Name = "Driver4", Age = 45 };
            Shift sh1 = new Shift() { ShiftId = 1, FromYard = "Yard name", Date = DateTime.Parse("2022-11-02"), Line = "001", Tour = "F1", DriverId = 1, VehicleId = "AAA111" };
            Shift sh2 = new Shift() { ShiftId = 2, FromYard = "Yard name", Date = DateTime.Parse("2022-11-02"), Line = "001", Tour = "F2", DriverId = 2, VehicleId = "BBB222" };
            Shift sh3 = new Shift() { ShiftId = 3, FromYard = "Yard name", Date = DateTime.Parse("2022-11-02"), Line = "002", Tour = "F1", DriverId = 3, VehicleId = "CCC333" };
            Shift sh4 = new Shift() { ShiftId = 4, FromYard = "Yard name", Date = DateTime.Parse("2022-11-02"), Line = "003", Tour = "F1", DriverId = 4, VehicleId = "DDD444" };

            /*
             * Cannot assign to nav. props >> read only. Changed to RW for the test to work.
             * TODO: change it back to read only in the future >> solve mocking properly.
             * This is ugly.
             */
            //Connections
            //Shifts
            sh1.Vehicle = veh1;
            sh1.Driver = dr1;
            sh2.Vehicle = veh2;
            sh2.Driver = dr2;
            sh3.Vehicle = veh3;
            sh3.Driver = dr3;
            sh4.Vehicle = veh4;
            sh4.Driver = dr4;
            //Vehicles
            veh1.Shifts = new List<Shift>();
            veh2.Shifts = new List<Shift>();
            veh3.Shifts = new List<Shift>();
            veh4.Shifts = new List<Shift>();
            veh1.Shifts.Add(sh1);
            veh2.Shifts.Add(sh2);
            veh3.Shifts.Add(sh3);
            veh4.Shifts.Add(sh4);
            //Drivers
            dr1.Shifts = new List<Shift>();
            dr2.Shifts = new List<Shift>();
            dr3.Shifts = new List<Shift>();
            dr4.Shifts = new List<Shift>();
            dr1.Shifts.Add(sh1);
            dr2.Shifts.Add(sh2);
            dr3.Shifts.Add(sh3);
            dr4.Shifts.Add(sh4);

            mockVehicleRepo.Setup(m => m.ReadAll()).Returns(new List<Vehicle>()
            {
                veh1,
                veh2,
                veh3,
                veh4
            }.AsQueryable());
            mockDriverRepo.Setup(m => m.ReadAll()).Returns(new List<Driver>()
            {
                dr1,
                dr2,
                dr3,
                dr4
            }.AsQueryable());
            mockShiftRepo.Setup(m => m.ReadAll()).Returns(new List<Shift>()
            {
                sh1,
                sh2,
                sh3,
                sh4
            }.AsQueryable());

            vehicleLogic = new VehicleLogic(mockVehicleRepo.Object);
            driverLogic = new DriverLogic(mockDriverRepo.Object);
            shiftLogic = new ShiftLogic(mockShiftRepo.Object);
        }


        [Test]
        public void VehicleListModelsTest()
        {
            IQueryable<string> models = vehicleLogic.ListModels("VehManu2");
            Assert.That(models.Contains("VehMake3"), Is.True);
            Assert.That(models.Contains("VehMake2"), Is.False);
        }

        [Test]
        public void CreateVehicleTestWithValidRegTest()
        {
            var vehicle = new Vehicle() { Registration = "SIF013" };
            vehicleLogic.Create(vehicle);

            mockVehicleRepo.Verify(r => r.Create(vehicle), Times.Once);
        }

        [Test]
        public void CreateVehicleTestWithInvalidRegTest()
        {
            var vehicle = new Vehicle() { Registration = "AADI721" };
            try
            {
                vehicleLogic.Create(vehicle);
            }
            catch
            {
            }

            mockVehicleRepo.Verify(r => r.Create(vehicle), Times.Never);
        }

        [Test]
        public void AvgDriverAgeTest()
        {
            double? avg = driverLogic.AvgDriverAge();

            //AVG(30+35+40+45) = 37.5
            Assert.That(avg, Is.EqualTo(37.5));
        }

        [Test]
        public void ShiftsOfDriverTest()
        {
            var shifts = driverLogic.ShiftsOfDriverModified("Driver1");

            Assert.That(shifts.ToArray()[0].ShiftId, Is.EqualTo(1));
        }

        [Test]
        public void VehicleListDriversTest()
        {
            var drivers = vehicleLogic.ListDrivers("DDD444");
            Assert.That(drivers.ToArray<Driver>()[0].DriverId == 4);
        }

        [Test]
        public void ShiftVehiclesOnLineTest()
        {
            var vehicles = shiftLogic.VehiclesOnLine("002");

            Assert.That(vehicles.ToArray<Vehicle>()[0].Registration == "CCC333");
        }

        [Test]
        public void ShiftVehiclesOnLineInvalidTest()
        {
            var vehicles = shiftLogic.VehiclesOnLine("100");

            Assert.That(vehicles.ToArray<Vehicle>().Length == 0);
        }

        [Test]
        public void ShiftLengthOfVehiclesOnLineTest()
        {
            var results = shiftLogic.LengthOfVehiclesOnLine("002");

            Assert.That(results.ToArray()[0] == 18);
        }

        [Test]
        public void ShiftLengthOfVehiclesOnLineDistinctTest()
        {
            var results = shiftLogic.LengthOfVehiclesOnLine("001");

            Assert.That(results.ToArray().Length == 1);
        }


    }
}

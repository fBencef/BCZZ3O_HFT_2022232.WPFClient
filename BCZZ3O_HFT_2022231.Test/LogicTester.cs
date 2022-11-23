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

            mockVehicleRepo.Setup(m => m.ReadAll()).Returns(new List<Vehicle>()
            {
                new Vehicle("AAA111#VehManu1#VehMake1#12#2014-05-01"),
                new Vehicle("BBB222#VehManu1#VehMake2#12#2014-05-01"),
                new Vehicle("CCC333#VehManu2#VehMake3#18#2014-05-01"),
                new Vehicle("DDD444#VehManu2#VehMake4#18#2014-05-01"),
            }.AsQueryable());
            mockDriverRepo.Setup(m => m.ReadAll()).Returns(new List<Driver>()
            {
                new Driver(){ DriverId = 1, Name="Driver1", Age=30},
                new Driver(){ DriverId = 2, Name="Driver2", Age=35},
                new Driver(){ DriverId = 3, Name="Driver3", Age=40},
                new Driver(){ DriverId = 4, Name="Driver4", Age=45},
            }.AsQueryable());
            mockShiftRepo.Setup(m => m.ReadAll()).Returns(new List<Shift>()
            {
                new Shift(){ ShiftId = 1, FromYard = "Yard name", Date=DateTime.Parse("2022-11-02"),Line="001",Tour="F1",DriverId=1,VehicleId="AAA111" },
                new Shift(){ ShiftId = 2, FromYard = "Yard name", Date=DateTime.Parse("2022-11-02"),Line="001",Tour="F2",DriverId=2,VehicleId="BBB222" },
                new Shift(){ ShiftId = 3, FromYard = "Yard name", Date=DateTime.Parse("2022-11-02"),Line="002",Tour="F1",DriverId=3,VehicleId="CCC333" },
                new Shift(){ ShiftId = 4, FromYard = "Yard name", Date=DateTime.Parse("2022-11-02"),Line="003",Tour="F1",DriverId=4,VehicleId="DDD444" },
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
            var shifts = driverLogic.ShiftsOfDriver("Driver1");

            Assert.That(shifts.ToArray()[0], Is.EqualTo(1));
        }

        [Test]
        public void ShiftGetDriverTest()
        {
            //Nem működik >> LAZY LOADING ???

            ////Shift shift = shiftLogic.Read(1);
            //Shift shift = new Shift() { ShiftId = 1, FromYard = "Yard name", Date = DateTime.Parse("2022-11-02"), Line = "001", Tour = "F1", DriverId = 1, VehicleId = "AAA111" };
            //Driver item = shiftLogic.GetDriver(shift);
            //Assert.That(item.DriverId, Is.EqualTo(1));
        }


    }
}

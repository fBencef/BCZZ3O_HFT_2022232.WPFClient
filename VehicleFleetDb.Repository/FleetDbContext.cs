using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;

namespace VehicleFleetDb.Repository
{
    public class FleetDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        public FleetDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Repositories\VehicleFleet.mdf;Integrated Security=True;MultipleActiveResultSets=true";
                builder
                .UseSqlServer(conn)
                .UseLazyLoadingProxies();

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //REALTIONS
            modelBuilder.Entity<Shift>(shift => shift
                          .HasOne<Vehicle>()
                          .WithMany()
            .HasForeignKey(shift => shift.VehicleId));
            modelBuilder.Entity<Shift>(shift => shift
                           .HasOne<Driver>()
                           .WithMany()
                           .HasForeignKey(shift => shift.DriverId));
            //SEED CONTENT
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle[]
                {
                    new Vehicle("MHU863#Mercedes-Benz#Citaro C2#12#2014-05-01"),
                    new Vehicle("MHU859#Mercedes-Benz#Citaro C2#12#2014-05-01"),
                    new Vehicle("MHU723#Mercedes-Benz#Citaro C2G#18#2014-05-01")
                });

            modelBuilder.Entity<Driver>().HasData(new Driver[]
                {
                    new Driver(){ DriverId = 1, Name="Kiss János", Age=45},
                    new Driver(){ DriverId = 2, Name="Lantos Johnny", Age=21},
                    new Driver(){ DriverId = 3, Name="Nagy Maros", Age=35},
                    new Driver(){ DriverId = 4, Name="Pista Bácsi", Age=66},
                    new Driver(){ DriverId = 5, Name="Floor Jansen", Age=46},
                    new Driver(){ DriverId = 2, Name="Farkas Gellért Máté", Age=27}
                });

            modelBuilder.Entity<Shift>().HasData(new Shift[]
                {
                    new Shift(){ ShiftId = 1, FromYard = "Bogáncs utca", Date=DateTime.Parse("2022-9-11"),Line="204",Tour="F10",DriverId=1,VehicleId="MHU863" },
                    new Shift(){ ShiftId = 2, FromYard = "Bogáncs utca", Date=DateTime.Parse("2022-9-11"),Line="296",Tour="F7",DriverId=3,VehicleId="MHU859" },
                    new Shift(){ ShiftId = 3, FromYard = "Bogáncs utca", Date=DateTime.Parse("2022-9-11"),Line="20E",Tour="F15",DriverId=5,VehicleId="MHU723" }
                }) ;
        }

    }
}

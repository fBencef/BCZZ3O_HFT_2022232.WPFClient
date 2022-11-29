using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleFleetDb.Models;

namespace VehicleFleetDb.Logic
{
    public interface IShiftLogic
    {
        void Create(Shift item);
        void Delete(int id);
        Shift Read(int id);
        IEnumerable<Shift> ReadAll();
        void Update(Shift item);

        //NON-CRUDS
        public Driver GetDriver(int shiftId);
    }
}

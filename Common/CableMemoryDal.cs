using stdole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CableMemoryDal
    {
        private static List<Cable> _cables = new List<Cable>();
        private static List<CableHistroy> _cableHistroys = new List<CableHistroy>();

        private static object _cablesLock = new object();
        private static object _cablesHistroyLock = new object();
        private static object _UpdateLock = new object();

        private void AddCable(Cable cable)
        {
            lock (_cablesLock)
            {
                _cables.Add(cable);
            }
        }
        private void RemoveCable(Cable cable)
        {
            lock (_cablesLock)
            {
                _cables.Remove(cable);
            }
        }
        private void AddCablesHistroy(CableHistroy cableHistroy)
        {
            lock (_cablesHistroyLock)
            {
                _cableHistroys.Add(cableHistroy);
            }
        }
        private void RemoveCablesHistroy(CableHistroy cableHistroy)
        {
            lock (_cablesHistroyLock)
            {
                _cableHistroys.Remove(cableHistroy);
            }
        }
        public void AddCable(List<Cable> Cables)
        {
            var fixtures= Cables.GroupBy(x => x.FAI1_A).Select(x=>x.Key);
            foreach (var fixture in fixtures)
            {
                var cables = _cables.Where(x => x.FAI1_A == fixture);
                foreach (var cable in cables)
                {
                    RemoveCable(cable);
                }
            }
            foreach (var cable in Cables)
            {
                AddCable(cable);
            }
        }

        public void AddHistroy(string fixture,string fixturePat)
        {
            var cables= _cables.Where(x => x.FAI1_A == fixture);
            foreach (var cable in cables)
            {
                lock (_UpdateLock)
                {
                    cable.FAI1_B = fixturePat;
                    cable.Status = "PASS";
                    var dt = cable.ToTable();
                    CSVHelper.SaveCSV(dt, $"{DataContent.SystemConfig.CSVPath}\\{Guid.NewGuid().ToString()}.csv") ;
                    RemoveCable(cable);
                }
            }
        }
    }
}

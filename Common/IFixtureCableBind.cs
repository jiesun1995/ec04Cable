using stdole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IFixtureCableBindService
    {
        [OperationContract]
        bool FixtureCableBind(List<Cable> cables);

        [OperationContract]
        bool FixtureBind(string fixture, string fixturePat);
    }

    public class FixtureCableBindService : IFixtureCableBindService
    {
        //private readonly CableMemoryDal _cableMemoryDal;
        private readonly CableSqliteDal _cableSqliteDal;

        public FixtureCableBindService()
        {
            _cableSqliteDal = new CableSqliteDal();
        }

        public bool FixtureBind(string fixture, string fixturePat)
        {
            try
            {
                _cableSqliteDal.AddHistroy(fixture, fixturePat);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
                return false;

            }
            return true;
        }

        public bool FixtureCableBind(List<Cable> cables)
        {
            try
            {
                _cableSqliteDal.AddCable(cables);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
                return false;

            }
            return true;
        }
    }
}

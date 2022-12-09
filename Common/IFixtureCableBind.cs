using stdole;
using System;
using System.Collections.Generic;
using System.Data;
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

        [OperationContract]
        DataTable QueryHistroy(string cable=null, string fixture = null, string fixturePat = null, DateTime? startDate = null, DateTime? endDate = null);
        [OperationContract]
        DataTable Query(string cable = null, string fixture = null, string fixturePat = null);
    }

    public class FixtureCableBindService : IFixtureCableBindService
    {
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
                LogManager.Info($"进行治具绑定：【{fixture}:{fixturePat}】");
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
                LogManager.Info($"进行数据绑定：{JsonHelper.SerializeObject(cables)}" );
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
                return false;

            }
            return true;
        }

        public DataTable QueryHistroy(string cable = null, string fixture = null, string fixturePat = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var dt = _cableSqliteDal.QueryHistroy(cable, fixture, fixturePat, startDate, endDate);
            return dt;
        }
        public DataTable Query(string cable = null, string fixture = null, string fixturePat = null)
        {
            var dt = _cableSqliteDal.Query(cable, fixture, fixturePat);
            return dt;
        }
    }
}

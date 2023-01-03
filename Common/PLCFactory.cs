using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PLCFactory
    {
        public static IPLCReadWrite Instance(string ip, int port, PLCType type = PLCType.None)
        {
            IPLCReadWrite plcReadWrite = null;
            switch (type)
            {
                case PLCType.OMR:
                    plcReadWrite = new OMRHelper(ip, port);
                    break;
                case PLCType.Inovance:
                    plcReadWrite = new InovanceHelper(ip, port);
                    
                    break;
                default:
                    throw new ArgumentNullException("不存在选中类型的PLC");
            }
            return plcReadWrite;
        }
    }
    public interface IPLCReadWrite
    {
        bool IsConnect();
        int Read(int address);
        bool Write(int address,int value);
    }
    public enum PLCType
    {
        None = 0,
        OMR = 1,
        Inovance = 2,
    }
}

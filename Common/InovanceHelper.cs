using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum SoftElemType
    {
        //AM600
        ELEM_QX = 0,     //QX元件
        ELEM_MW = 1,     //MW元件
        ELEM_X = 2,      //X元件(对应QX200~QX300)
        ELEM_Y = 3,      //Y元件(对应QX300~QX400)

        //H3U
        REGI_H3U_Y = 0x20,       //Y元件的定义	
        REGI_H3U_X = 0x21,      //X元件的定义							
        REGI_H3U_S = 0x22,      //S元件的定义				
        REGI_H3U_M = 0x23,      //M元件的定义							
        REGI_H3U_TB = 0x24,     //T位元件的定义				
        REGI_H3U_TW = 0x25,     //T字元件的定义				
        REGI_H3U_CB = 0x26,     //C位元件的定义				
        REGI_H3U_CW = 0x27,     //C字元件的定义				
        REGI_H3U_DW = 0x28,     //D字元件的定义				
        REGI_H3U_CW2 = 0x29,        //C双字元件的定义
        REGI_H3U_SM = 0x2a,     //SM
        REGI_H3U_SD = 0x2b,     //
        REGI_H3U_R = 0x2c,      //
                                //H5u
        REGI_H5U_Y = 0x30,       //Y元件的定义	
        REGI_H5U_X = 0x31,      //X元件的定义							
        REGI_H5U_S = 0x32,      //S元件的定义				
        REGI_H5U_M = 0x33,      //M元件的定义	
        REGI_H5U_B = 0x34,       //B元件的定义
        REGI_H5U_D = 0x35,       //D字元件的定义
        REGI_H5U_R = 0x36,       //R字元件的定义

    }
    public class InovanceHelper :IPLCReadWrite
    {
        #region //标准库
        [DllImport("StandardModbusApi.dll", EntryPoint = "Init_ETH_String", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Init_ETH_String(string sIpAddr, int nNetId = 0, int IpPort = 502);

        [DllImport("StandardModbusApi.dll", EntryPoint = "Exit_ETH", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Exit_ETH(int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem_Float", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Read_Soft_Elem_Float(SoftElemType eType, int nStartAddr, int nCount, float[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Soft_Elem_Float", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Soft_Elem_Float(SoftElemType eType, int nStartAddr, int nCount, float[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Soft_Elem_Int32", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Soft_Elem_Int32(SoftElemType eType, int nStartAddr, int nCount, long[] pValue, int nNetId = 0);
        //STANDARD_MODBUS_FUNC_EXPORT int H5u_Read_Soft_Elem_Int32(SoftElemType eType,int nStartAddr,int nCount, long* pValue,int nNetId = 0);
        //int nRet = H5u_Write_Soft_Elem_Int32(SoftElemType.REGI_H5U_D, nStartAddr, nCount, pValue, nNetId);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Write_Soft_Elem_Int32", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Write_Soft_Elem_Int32(SoftElemType eType, int nStartAddr, int nCount, long[] pValue, int nNetId = 0);

        //STANDARD_MODBUS_FUNC_EXPORT int H5u_Write_Soft_Elem_Int32(SoftElemType eType,int nStartAddr,int nCount, long* pValue,int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        private readonly string _ip;
        private readonly int _netId;
        private readonly int _port;
        private bool _isConnect;

        public InovanceHelper(string ip, int port,int netId= 0)
        {
            _ip = ip;
            _netId = netId;
            _port = port;
            _isConnect = Init_ETH_String(_ip, _netId, _port);
        }

        #endregion

        /// <summary>
        /// 连接方法
        /// </summary>
        /// <returns></returns>
        public bool connect_H5U()
        {
            int nNetId = 0;
            int nIpPort = 502;
            //plc-IP地址
            _isConnect = Init_ETH_String("192.168.1.10", nNetId, nIpPort);
            return _isConnect;
        }

       // public bool IsConnect { get;  }

        public bool connect_H5U(string ip, int port , int netId=0)
        {
            _isConnect = Init_ETH_String(ip, netId, port);
            return _isConnect;
        }

        //public int ReadAddressByD(int nStartAddr)
        //{
        //    var result = read_D_Address(nStartAddr);
        //    return result;
        //}
        //public bool WriteAddressByD(int nStartAddr, int value)
        //{
        //    var result = write_D_Address(nStartAddr, value);
        //    LogManager.Info($"PLC中写入：{{{nStartAddr}:{value}}}");
        //    return result;
        //}
        /// <summary>
        /// 读D区一个值
        /// </summary>
        /// <param name="nStartAddr"></param>
        /// <returns></returns>
        public int read_D_Address(int nStartAddr)
        {
            int value;
            int nCount = 1;

            long[] pValue = new long[1];
            int nNetId = 0;

            int nRet = H5u_Read_Soft_Elem_Int32(SoftElemType.REGI_H5U_D, nStartAddr, nCount, pValue, nNetId);
            _isConnect = nRet == 1;
            long fValue = pValue[0];
            value = Convert.ToInt32(fValue);
            return value;
        }
        public double read_D_Address_Float(int nStartAddr)
        {
            double value;
            int nCount = 2;

            float[] pValue = new float[2];
            int nNetId = 0;

            int nRet = H5u_Read_Soft_Elem_Float(SoftElemType.REGI_H5U_D, nStartAddr, nCount, pValue, nNetId);
            float fValue = pValue[0];
            value = Convert.ToDouble(fValue);
            return value;
        }
        /// <summary>
        /// 写D区一个值---整数-------------->>>>>
        /// </summary>
        /// <param name="StartAddr"></param>
        /// <param name="value"></param>
        public bool write_D_Address(int nStartAddr, int value)
        {
            //int nStartAddr = 100;//寄存器地址
            int nCount = 2;//寄存器个数
                           //byte[] pValue = new byte[2];//缓冲区
            int nNetId = 0;//连接id

            //把要写的数据存入缓冲区，备写
            //pValue[0] = (byte)(nValue % 256);
            //pValue[1] = (byte)(nValue / 256);

            //调用api写数据
            long[] pValue = new long[2];

            pValue[0] = value;

            int nRet = H5u_Write_Soft_Elem_Int32(SoftElemType.REGI_H5U_D, nStartAddr, nCount, pValue, nNetId);

            return 1 == nRet;

        }
        /// <summary>
        /// 读M区一个整数-------------->>>>>
        /// </summary>
        /// <param name="nStartAddr"></param>
        /// <returns></returns>
        public string read_M_Address(int nStartAddr)
        {
            {
                int nCount = 1;//寄存器个数
                byte[] pValue = new byte[8];//缓冲区(要是8的整数倍)
                int nNetId = 0;//连接id
                bool[] Y = new bool[7];//缓冲区(要是8的整数倍)

                //调用api写数据
                int nRet = H5u_Read_Soft_Elem(SoftElemType.REGI_H5U_M, nStartAddr, nCount, pValue, nNetId);

                for (int i = 0; i < 7; i++)
                {
                    if (1 == pValue[i])
                    {
                        Y[i] = true;
                    }
                    else
                    {
                        Y[i] = false;
                    }
                }
                return pValue[0].ToString();
                //return nRet == 1 ? pValue[0].ToString() : "read error";
            }
        }
        public bool set_M_on(int nStartAddr)
        {
            int nCount = 1;//寄存器个数
            byte[] pValue = new byte[8];//缓冲区
            int nNetId = 0;//连接id

            //把要写的数据存入缓冲区，备写
            pValue[0] = 1;//Y5
            pValue[1] = 1;//Y6
            pValue[2] = 1;//Y7
            pValue[3] = 1;//Y10
            pValue[4] = 1;//Y11
            pValue[5] = 1;//Y12
            pValue[6] = 1;//Y13
            pValue[7] = 1;//Y14

            int nRet = H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_M, nStartAddr, nCount, pValue, nNetId);
            return nRet == 1;
        }
        public bool set_M_off(int nStartAddr)
        {
            int nCount = 1;//寄存器个数
            byte[] pValue = new byte[8];//缓冲区
            int nNetId = 0;//连接id

            //把要写的数据存入缓冲区，备写
            pValue[0] = 0;//Y5
            pValue[1] = 1;//Y6
            pValue[2] = 1;//Y7
            pValue[3] = 1;//Y10
            pValue[4] = 1;//Y11
            pValue[5] = 1;//Y12
            pValue[6] = 1;//Y13
            pValue[7] = 1;//Y14

            int nRet = H5u_Write_Soft_Elem(SoftElemType.REGI_H5U_M, nStartAddr, nCount, pValue, nNetId);

            return nRet == 1;
        }
        public bool close_conect()
        {
            int nNetId = 0;
            return Exit_ETH(nNetId);
        }

        bool IPLCReadWrite.IsConnect()
        {
            return _isConnect;
        }

        public int Read(int address)
        {
            var result = read_D_Address(address);
            return result;
        }

        public bool Write(int address, int value)
        {
            var result = write_D_Address(address, value);
            LogManager.Info($"PLC中写入：{{{address}:{value}}}");
            return result;
        }
    }
}

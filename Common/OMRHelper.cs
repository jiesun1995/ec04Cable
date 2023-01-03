using HslCommunication.Profinet.Omron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 欧姆龙plc帮助类
    /// </summary>
    public class OMRHelper:IPLCReadWrite
    {
        private readonly OmronFinsUdp _omronFinsUdp;
        private bool _isConnect = false;
        public OMRHelper(string ip,int port)
        {
            try
            {
                _omronFinsUdp = new OmronFinsUdp(ip, port);
            }
            catch (Exception ex)
            {
                throw new Exception("PLC初始化失败",ex);
            }
        }

        //public ushort Read(string address)
        //{
        //    var result = _omronFinsUdp.ReadUInt16(address);
        //    _isConnect = result.IsSuccess;
        //    return result.Content;
        //}
        ////public bool IsConnect { get { return _isConnect; } }
        
        //public bool Write(string address,int val)
        //{
        //    LogManager.Debug($"往plc写入:{address}:{val}");
        //    var result = _omronFinsUdp.Write(address, (ushort)val);
        //    return result.IsSuccess;
        //}

        public bool IsConnect()
        {
            return _isConnect;
        }

        public int Read(int address)
        {
            var result = _omronFinsUdp.ReadUInt16($"D{address}");
            _isConnect = result.IsSuccess;
            return result.Content;
        }

        public bool Write(int address,int value)
        {
            LogManager.Debug($"往plc写入:D{address}:{value}");
            var result = _omronFinsUdp.Write($"D{address}", (ushort)value);
            return result.IsSuccess;
        }
    }
}

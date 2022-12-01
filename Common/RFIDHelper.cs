using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TBEN_RFID;

namespace Common
{
    internal class RFIDHelper
    {
        private string writeString0;
        private string writeString1;
        private string writeString2;
        private string writeString3;

        public int DataLength_Ch0 { get; set; } = 100;
        public int DataLength_Ch1 { get; set; } = 100;
        public int DataLength_Ch2 { get; set; } = 100;
        public int DataLength_Ch3 { get; set; } = 100;

        public int StartAddress_Ch0 { get; set; } = 0;
        public int StartAddress_Ch1 { get; set; } = 0;
        public int StartAddress_Ch2 { get; set; } = 0;
        public int StartAddress_Ch3 { get; set; } = 0;

        //标志位
        public bool channel0_connect;
        public bool channel1_connect;
        public bool channel2_connect;
        public bool channel3_connect;

        private bool channel0_tp;
        private bool channel1_tp;
        private bool channel2_tp;
        private bool channel3_tp;

        //public delegate void ChannelStateCallback(RfidChannelState state);
        //private ChannelStateCallback channelStateCallback;
        //public delegate void ReadCallback(int channelId, string content);
        //private ReadCallback readCallback;

        public Action<RfidChannelState> ChannelStateCallback;
        public Action<int, string> ReadCallback;

        private Master luxmaster;
        private byte[] data;
        private object obj = new object();
        public int stateRefreshCT = 100;

        int responseCode_Ch0;
        int responseCode_Ch1;
        int responseCode_Ch2;
        int responseCode_Ch3;

        public RFIDHelper(string ip, int channelId, int port = 502)
        {
            if (string.IsNullOrEmpty(ip)) throw new Exception("连接rfid存在问题");

            luxmaster = new Master(ip, (ushort)port);
            luxmaster.OnResponseData += new Master.ResponseData(LuxMaster_OnOnResponseData);

            if (luxmaster.connected)
            {
                ThreadStatus(channelId);
            }
            else
            {
                throw new Exception("连接rfid存在问题");
            }
        }

        public void Yolo_Closed()
        {
            if (luxmaster != null)
                luxmaster.disconnect();
            ReadCallback = null;
            ChannelStateCallback = null;
        }

        #region Write
        public void Write(int channelId, string content)
        {
            if (string.IsNullOrEmpty(content) || content == "") { throw new ArgumentNullException("content"); }
            bool connect = false;
            bool tp = false;
            int length;
            int startAddress;
            int responseCode;
            byte unit;
            ushort StartAddress;
            switch (channelId)
            {
                case 0:
                    connect = channel0_connect;
                    tp = channel0_tp;
                    length = DataLength_Ch0;
                    writeString0 = content;
                    startAddress = StartAddress_Ch0;
                    responseCode = responseCode_Ch0;
                    unit = 0;
                    StartAddress = 0x0800;
                    break;
                case 1:
                    connect = channel1_connect;
                    tp = channel1_tp;
                    length = DataLength_Ch1;
                    writeString1 = content;
                    startAddress = StartAddress_Ch1;
                    responseCode = responseCode_Ch1;
                    unit = 1;
                    StartAddress = 0x084C;
                    break;
                case 2:
                    connect = channel2_connect;
                    tp = channel2_tp;
                    length = DataLength_Ch2;
                    writeString2 = content;
                    startAddress = StartAddress_Ch2;
                    responseCode = responseCode_Ch2;
                    unit = 2;
                    StartAddress = 0x0898;
                    break;
                case 3:
                    connect = channel3_connect;
                    tp = channel3_tp;
                    length = DataLength_Ch3;
                    writeString3 = content;
                    startAddress = StartAddress_Ch3;
                    responseCode = responseCode_Ch3;
                    unit = 3;
                    StartAddress = 0x08E4;
                    break;
                default:
                    connect = channel0_connect;
                    tp = channel0_tp;
                    length = DataLength_Ch0;
                    writeString0 = content;
                    startAddress = StartAddress_Ch0;
                    responseCode = responseCode_Ch0;
                    unit = 0;
                    StartAddress = 0x0800;
                    break;
            }

            if (!connect)
            {
                string message = "通道ID: " + channelId + "断开连接！";
                throw new Exception(message);
            }
            if (!tp) { throw new Exception("请将载码体放入读写头的感应区域！"); }
            if (content.Length != length) { throw new Exception("写入数据长度错误！"); }

            //*************通用代码**************
            //----- 初始化 字节数组的长度
            //----- 如果是奇数，则加1，如果是偶数，则不变
            byte[] sendData_data;
            if (length % 2 == 0)
                sendData_data = new byte[length];
            else
                sendData_data = new byte[length + 1];

            //-----把字符串转换为单个字符的数组，ASCII码。
            char[] tmp = content.ToCharArray();

            for (int i = 0; i < content.Length; i++)
            {
                //-----把tmp 的每一个字符都转换为数字
                sendData_data[i] = (byte)tmp[i];
            }

            //-----建立一个新的数组，长度比输入的字符数组的长度要长24，需要开始写入需要保存的字节
            byte[] sendData = new byte[sendData_data.Length + 24];
            ushort ID;

            //-----------如果事件标识符
            if (responseCode != 0x0008)
            {
                sendData[0] = 0x00;
                sendData[1] = 0x08;  //写数据
                sendData[2] = 0x00;
                sendData[3] = 0x00;  //
                int staradd_L = startAddress % 256;
                int staradd_H = startAddress / 256;
                int length_L = length % 256;
                int length_H = length / 256;
                sendData[4] = Convert.ToByte(staradd_H);
                sendData[5] = Convert.ToByte(staradd_L); //-----代表起始地址高低位
                sendData[6] = 0x00;
                sendData[7] = 0x00;
                sendData[8] = Convert.ToByte(length_H);
                sendData[9] = Convert.ToByte(length_L); //-----代表地址长度高低位
                sendData[10] = 0x00;
                sendData[11] = 0x00;

                for (int i = 0; i < sendData_data.Length / 2; i++)
                {
                    //--------------把数据写入到寄存器中
                    sendData[2 * i + 24] = sendData_data[2 * i + 1];  //---把写入的数据写入sendData数组中
                    sendData[2 * i + 25] = sendData_data[2 * i];  //---把写入的数据写入sendData数组中
                }
                ID = 9;
            }
            else
            {
                sendData[0] = 0x00;
                sendData[1] = 0x00;

                ID = 10;
            }
            luxmaster.WriteMultipleRegister(ID, unit, StartAddress, sendData);  //----写入多数的寄存器.....
        }
        #endregion

        #region Read
        public void Read(int channelId)
        {
            bool connect = false;
            bool tp = false;
            int length;
            int startAddress;
            int responseCode;
            byte unit;
            ushort StartAddress;
            switch (channelId)
            {
                case 0:
                    connect = channel0_connect;
                    tp = channel0_tp;
                    length = DataLength_Ch0;

                    startAddress = StartAddress_Ch0;
                    responseCode = responseCode_Ch0;
                    unit = 0;
                    StartAddress = 0x0800;
                    break;
                case 1:
                    connect = channel1_connect;
                    tp = channel1_tp;
                    length = DataLength_Ch1;
                    startAddress = StartAddress_Ch1;
                    responseCode = responseCode_Ch1;
                    unit = 1;
                    StartAddress = 0x084C;
                    break;
                case 2:
                    connect = channel2_connect;
                    tp = channel2_tp;
                    length = DataLength_Ch2;
                    startAddress = StartAddress_Ch2;
                    responseCode = responseCode_Ch2;
                    unit = 2;
                    StartAddress = 0x0898;
                    break;
                case 3:
                    connect = channel3_connect;
                    tp = channel3_tp;
                    length = DataLength_Ch3;
                    startAddress = StartAddress_Ch3;
                    responseCode = responseCode_Ch3;
                    unit = 3;
                    StartAddress = 0x08E4;
                    break;
                default:
                    connect = channel0_connect;
                    tp = channel0_tp;
                    length = DataLength_Ch0;
                    startAddress = StartAddress_Ch0;
                    responseCode = responseCode_Ch0;
                    unit = 0;
                    StartAddress = 0x0800;
                    break;
            }

            if (!connect)
            {
                string message = "通道ID: " + channelId + "断开连接！";
                throw new Exception(message);
            }

            if (!tp) { throw new Exception("请将载码体放入读写头的感应区域！"); }
            //-------初始化数据
            byte[] sendData = new byte[12];
            ushort ID;

            if (responseCode != 0x0002)
            {
                sendData[0] = 0x00;
                sendData[1] = 0x02;  //CMD
                sendData[2] = 0x00;
                sendData[3] = 0x00;
                int staradd_L = startAddress % 256;
                int staradd_H = startAddress / 256;

                int length_L, length_H;

                if (length % 2 == 0)
                {
                    length_L = length % 256;
                    length_H = length / 256;
                }
                else
                {
                    length_L = (length + 1) % 256;
                    length_H = (length + 1) / 256;
                }
                sendData[4] = Convert.ToByte(staradd_H); //起始地址0
                sendData[5] = Convert.ToByte(staradd_L); //起始地址1
                sendData[6] = 0x00;
                sendData[7] = 0x00;
                sendData[8] = Convert.ToByte(length_H);  //----数据长度1
                sendData[9] = Convert.ToByte(length_L);  //----数据长度2
                sendData[10] = 0x00;
                sendData[11] = 0x00;

                ID = 6;
            }
            else
            {
                sendData[0] = 0x00;
                sendData[1] = 0x00;
                ID = 8;
            }
            luxmaster.WriteMultipleRegister(ID, unit, StartAddress, sendData);
        }
        #endregion

        #region Reset
        public void Reset()
        {
            int[] ResponseArr = ResponseCodeArr();
            ushort[] addressArr = { 0x0800, 0x084c, 0x0898, 0x08e4 };
            byte[] sendData = new byte[12];
            ushort ID = 4;
            ushort StartAddress;
            byte unit;
            for (int i = 0; i < ResponseArr.Length; i++)
            {
                int res = ResponseArr[i];
                if (res != 0x8000)
                {
                    sendData[0] = 0x80;
                    sendData[1] = 0x00;
                    unit = Convert.ToByte(i);
                    StartAddress = addressArr[i];
                    luxmaster.WriteMultipleRegister(ID, unit, StartAddress, sendData);
                }
            }
        }
        #endregion

        private int[] ResponseCodeArr()
        {
            int[] responseArr = { responseCode_Ch0, responseCode_Ch1, responseCode_Ch2, responseCode_Ch3 };
            return responseArr;
        }

        #region Idle
        public void Idle()
        {
            int[] ResponseArr = ResponseCodeArr();
            ushort[] addressArr = { 0x0800, 0x084c, 0x0898, 0x08e4 };
            byte[] sendData = new byte[12];
            ushort ID = 5;
            ushort StartAddress;
            byte unit;
            for (int i = 0; i < ResponseArr.Length; i++)
            {
                int res = ResponseArr[i];
                if (res != 0x0000)
                {
                    sendData[0] = 0x00;
                    sendData[1] = 0x00;
                    unit = Convert.ToByte(i);
                    StartAddress = addressArr[i];
                    luxmaster.WriteMultipleRegister(ID, unit, StartAddress, sendData);
                }
            }
        }
        #endregion

        private void ThreadStatus(int channelId)
        {
            Task.Factory.StartNew(async obj =>
            {
                var index = int.Parse(obj.ToString());
                while (true)
                {
                    try
                    {
                        ReadSendStatus(channelId);
                        //ReadSendStatus(1);
                        //ReadSendStatus(2);
                        //ReadSendStatus(3);
                        await Task.Delay(10);
                    }
                    catch (Exception ex)
                    {
                        LogManager.Error(ex);
                    }
                }
            }, channelId);


            //ThreadPool.SetMaxThreads(10,10);
            //ThreadPool.QueueUserWorkItem(O =>
            //{
            //    ReadSendStatus(0);
            //});
            //ThreadPool.QueueUserWorkItem(O =>
            //{
            //    ReadSendStatus(1);
            //});

            //ThreadPool.QueueUserWorkItem(O =>
            //{
            //    ReadSendStatus(2);
            //});
            //ThreadPool.QueueUserWorkItem(O =>
            //{
            //    ReadSendStatus(3);
            //});
            /*ThreadPool.QueueUserWorkItem(new WaitCallback());*/
        }
        //实时读取case1 状态
        private void ReadSendStatus(int channelId)
        {
            ushort[] addressArr = { 0, 0x004C, 0x0098, 0x00E4 };
            byte unit = Convert.ToByte(channelId);
            ushort StartAddress = addressArr[channelId];  //----------通道0的首地址  ---状态开始地址0,读取数值存放的开始地址0c,
            lock (obj)
            {
                ushort ID = 1;
                ushort Length = 6;  //----------寄存器的个数，读取的是0-11;
                luxmaster.ReadInputRegister(ID, unit, StartAddress, Length);
                Thread.Sleep(stateRefreshCT + 50);
            }
        }
        private void LuxMaster_OnOnResponseData(ushort ID, byte unit, byte function, byte[] values)
        {
            switch (ID)
            {
                case 1:  //--线程反馈实时信息
                    #region 状态1
                    data = values;
                    //this.Invoke((EventHandler)delegate
                    //{  
                    if (data.Length == 12)
                    {
                        RfidChannelState state = new RfidChannelState();
                        state.ChannelId = unit;
                        state.Connect = (data[4] & 1) == 1 ? true : false;
                        state.Busy = (data[0] & 128) == 128 ? true : false;
                        state.Error = (data[0] & 64) == 64 ? true : false;
                        state.Tp = (data[5] & 1) == 1 ? true : false;

                        byte resCode_L = data[1];
                        byte resCode_H = (byte)(data[0] & 63);
                        int resCode = resCode_L + resCode_H * 256;

                        byte errorCode_L = data[9];
                        byte errorCode_H = data[8];
                        int errorCode = errorCode_L + errorCode_H * 256;
                        state.ErrorCode = errorCode;

                        switch (unit)
                        {
                            case 0:
                                responseCode_Ch0 = resCode;
                                channel0_tp = state.Tp;
                                channel0_connect = state.Connect;
                                break;
                            case 1:
                                responseCode_Ch1 = resCode;
                                channel1_tp = state.Tp;
                                channel1_connect = state.Connect;
                                break;
                            case 2:
                                responseCode_Ch2 = resCode;
                                channel2_tp = state.Tp;
                                channel2_connect = state.Connect;
                                break;
                            case 3:
                                responseCode_Ch3 = resCode;
                                channel3_tp = state.Tp;
                                channel3_connect = state.Connect;
                                break;
                            default:
                                break;
                        }

                        if (ChannelStateCallback != null) ChannelStateCallback(state);
                    }
                    //});  
                    #endregion
                    break;
                case 10: //--写入寄存器
                    #region 状态10
                    {
                        string writeString = string.Empty;
                        int length = 0;
                        int start = 0;
                        ushort startAddress = 0;
                        byte units = 0;
                        switch (unit)
                        {
                            case 0:
                                units = 0;
                                startAddress = 0x0800;
                                start = StartAddress_Ch0;
                                length = DataLength_Ch0;
                                writeString = writeString0;
                                break;
                            case 1:
                                units = 1;
                                startAddress = 0x084C;
                                start = StartAddress_Ch1;
                                length = DataLength_Ch1;
                                writeString = writeString1;
                                break;
                            case 2:
                                units = 2;
                                startAddress = 0x0898;
                                start = StartAddress_Ch2;
                                length = DataLength_Ch2;
                                writeString = writeString2;
                                break;
                            case 3:
                                units = 3;
                                startAddress = 0x08E4;
                                start = StartAddress_Ch3;
                                length = DataLength_Ch3;
                                writeString = writeString3;
                                break;
                            default:
                                break;
                        }

                        byte[] sendData_data;
                        if (length % 2 == 0)
                            sendData_data = new byte[length];
                        else
                            sendData_data = new byte[length + 1];

                        char[] tmp = writeString.ToCharArray();
                        for (int i = 0; i < writeString.Length; i++)
                        {
                            sendData_data[i] = (byte)tmp[i];
                        }

                        byte[] sendData = new byte[sendData_data.Length + 24];
                        sendData[0] = 0x00;
                        sendData[1] = 0x08;  //CMD
                        sendData[2] = 0x00;
                        sendData[3] = 0x00;
                        int staradd_L = start % 256;
                        int staradd_H = start / 256;
                        int length_L = length % 256;
                        int length_H = length / 256;
                        sendData[4] = Convert.ToByte(staradd_H);
                        sendData[5] = Convert.ToByte(staradd_L);
                        sendData[6] = 0x00;
                        sendData[7] = 0x00;
                        sendData[8] = Convert.ToByte(length_H);
                        sendData[9] = Convert.ToByte(length_L);
                        sendData[10] = 0x00;
                        sendData[11] = 0x00;

                        for (int i = 0; i < sendData_data.Length / 2; i++)
                        {
                            sendData[2 * i + 24] = sendData_data[2 * i + 1];
                            sendData[2 * i + 25] = sendData_data[2 * i];
                        }

                        ushort IDs = 9;

                        Thread.Sleep(100);
                        luxmaster.WriteMultipleRegister(IDs, units, startAddress, sendData);
                    }
                    #endregion
                    break;
                case 6:  //--读取寄存器
                    #region 状态6
                    {
                        ushort startAddress = 0;
                        byte units = 0;
                        int length = 0;
                        switch (unit)
                        {
                            case 0:
                                units = 0;
                                startAddress = 0x000C;
                                length = DataLength_Ch0;
                                break;
                            case 1:
                                units = 1;
                                startAddress = 0x0058;
                                length = DataLength_Ch1;
                                break;
                            case 2:
                                units = 2;
                                startAddress = 0x00A4;
                                length = DataLength_Ch2;
                                break;
                            case 3:
                                units = 3;
                                startAddress = 0x00F0;
                                length = DataLength_Ch3;
                                break;
                            default:
                                break;
                        }
                        ushort IDs = 7;
                        int length_t;
                        if (length % 2 == 0)
                            length_t = length;
                        else
                            length_t = length + 1;

                        ushort Length = Convert.ToUInt16(length_t / 2);
                        Thread.Sleep(300);
                        luxmaster.ReadInputRegister(IDs, units, startAddress, Length);  //---发送读输入寄存器的指令
                    }
                    #endregion
                    break;
                case 8:  //--反馈码赋0后，从新读取寄存器
                    #region 状态8
                    {
                        int start = 0;
                        ushort startAddress = 0;
                        byte units = 0;
                        int length = 0;
                        switch (unit)
                        {
                            case 0:
                                units = 0;
                                startAddress = 0x0800;
                                start = StartAddress_Ch0;
                                length = DataLength_Ch0;
                                break;
                            case 1:
                                units = 1;
                                startAddress = 0x084C;
                                start = StartAddress_Ch1;
                                length = DataLength_Ch1;
                                break;
                            case 2:
                                units = 2;
                                startAddress = 0x0898;
                                start = StartAddress_Ch2;
                                length = DataLength_Ch2;
                                break;
                            case 3:
                                units = 3;
                                startAddress = 0x08E4;
                                start = StartAddress_Ch3;
                                length = DataLength_Ch3;
                                break;
                            default:
                                break;
                        }
                        byte[] sendData = new byte[12];
                        sendData[0] = 0x00;
                        sendData[1] = 0x02;  //CMD
                        sendData[2] = 0x00;
                        sendData[3] = 0x00;
                        int staradd_L = start % 256;
                        int staradd_H = start / 256;
                        int length_L = length % 256;
                        int length_H = length / 256;
                        sendData[4] = Convert.ToByte(staradd_H);
                        sendData[5] = Convert.ToByte(staradd_L);
                        sendData[6] = 0x00;
                        sendData[7] = 0x00;
                        sendData[8] = Convert.ToByte(length_H);
                        sendData[9] = Convert.ToByte(length_L);
                        sendData[10] = 0x00;
                        sendData[11] = 0x00;

                        ushort IDs = 6;
                        Thread.Sleep(100);
                        luxmaster.WriteMultipleRegister(IDs, units, startAddress, sendData);

                    }

                    #endregion
                    break;
                case 7:  //--读取到数据
                    #region 状态7
                    data = values;
                    //this.Invoke((EventHandler)delegate
                    //{
                    {
                        int length = 0;
                        int channelId = 0;
                        switch (unit)
                        {
                            case 0:
                                channelId = 0;
                                length = DataLength_Ch0;
                                break;
                            case 1:
                                channelId = 1;
                                length = DataLength_Ch1;
                                break;
                            case 2:
                                channelId = 2;
                                length = DataLength_Ch2;
                                break;
                            case 3:
                                channelId = 3;
                                length = DataLength_Ch3;
                                break;
                            default:
                                break;
                        }

                        if (data.Length == 1) return;
                        byte[] data_Real;
                        if (length % 2 == 0)
                            data_Real = new byte[data.Length];
                        else
                        {
                            data_Real = new byte[data.Length - 1];
                        }
                        if (length % 2 == 0)
                            for (int i = 0; i < data.Length / 2; i++)
                            {
                                data_Real[2 * i] = data[2 * i + 1];
                                data_Real[2 * i + 1] = data[2 * i];
                            }
                        else
                            for (int i = 0; i < data.Length / 2; i++)
                            {
                                data_Real[2 * i] = data[2 * i + 1];
                                if (i < data.Length / 2 - 1)
                                    data_Real[2 * i + 1] = data[2 * i];
                            }
                        string DATA_Real = System.Text.Encoding.ASCII.GetString(data_Real);

                        //txtReaddata_Ch0.Text = DATA_Real;
                        //已读取到数据//将数据抛出
                        if (ReadCallback != null) ReadCallback(channelId, DATA_Real);
                        //});
                    }
                    #endregion
                    break;
            }
        }

    }

    public class RFIDChannel
    {
        private Reader _reader;
        private bool _isConnect = false;
        private const int _dataLength = 50;
        public RFIDChannel(Reader reader)
        {
            _reader = reader;
            _reader.ConnChangedHandler += state =>
            {
                _isConnect = state;
            };
        }

        public void SetChannelState(Action<bool> TpChangedHandler)
        {
            _reader.TpChangedHandler += TpChangedHandler;
        }

        public bool IsConnect { get { return _isConnect; } }

        public bool Wirte(string content)
        {
            return _reader.WriteString(0, _dataLength, content);
        }

        public string Read()
        {
            var result = _reader.ReadString(0, _dataLength).Replace("\0", "");
            return result;
        }
    }

    public class RFIDFactory
    {
        private static object _obj = new object();
        private static Dictionary<string, Gateway> _gatewaies =new Dictionary<string, Gateway>();

        private RFIDFactory() { }
        public static RFIDChannel Instance(string ip, int channel, int port = 502)
        {
            lock (_obj)
            {
                if (!_gatewaies.ContainsKey(ip))
                {
                    var gateway = new Gateway();
                    gateway.Connect(ip, port);
                    _gatewaies.Add(ip, gateway);
                }
            }
            try
            {
                var reader = _gatewaies[ip].GetReaderInstance(channel);
                return new RFIDChannel(reader);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
            }
            return null;
        }
    }
}

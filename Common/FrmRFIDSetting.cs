using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Common
{
    public partial class FrmRFIDSetting : Form
    {
        private readonly RFIDConfig _config;

        public FrmRFIDSetting(RFIDConfig config)
        {
            InitializeComponent();
            _config= config;

            tbxIP1.Text = _config.IP;
            tbxChannel1.Text = _config.Channel.ToString();
            tbxPort1.Text = _config.Port.ToString();
            //tbxDataLength1.Text = _config.DataLength.ToString();
            //tbxStartAddress1.Text = _config.StartAddress.ToString();
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                RFIDHelper rfidHelper = null;
                try
                {
                    var ip = tbxIP1.Text;
                    var channel = ValidateDataByInt(tbxChannel1.Text, "通道不能设置为非数字");
                    var port = ValidateDataByInt(tbxPort1.Text, "端口不能设置为非数字");
                    //var dataLength = ValidateDataByInt(tbxDataLength1.Text, "内容长度不能设置为非数字");
                    //var startAddress = ValidateDataByInt(tbxStartAddress1.Text, "地址不能设置为非数字");

                    rfidHelper = new RFIDHelper(ip, channel, port);
                    //rfidHelper.DataLength_Ch0 = dataLength;
                    //rfidHelper.StartAddress_Ch0 = startAddress;
                    rfidHelper.ReadCallback = (channelId, Content) =>
                    {
                        if (channelId == channel)
                        {
                            Invoke((EventHandler)delegate
                            {
                                lblContent.Text = Content;
                            });
                        }
                    };
                    Task.Delay(200).Wait();
                    rfidHelper.Read(channel);
                    Task.Delay(500).Wait();
                }
                catch (Exception ex)
                {
                    try
                    {
                        Invoke((EventHandler)delegate
                                   {
                                       lblContent.Text = ex.Message;
                                   });
                    }
                    catch (Exception exception)
                    {
                        LogManager.Error(exception);
                    }
                }
                finally
                {
                    if (rfidHelper != null)
                    {
                        rfidHelper.Yolo_Closed();
                    }
                }
            });
        }

        public RFIDConfig GetRFIDConfig()
        {
            var ip = tbxIP1.Text;
            var channel = ValidateDataByInt(tbxChannel1.Text, "通道不能设置为非数字");
            var port = ValidateDataByInt(tbxPort1.Text, "端口不能设置为非数字");
            //var dataLength = ValidateDataByInt(tbxDataLength1.Text, "内容长度不能设置为非数字");
            //var startAddress = ValidateDataByInt(tbxStartAddress1.Text, "地址不能设置为非数字");
            var config = new RFIDConfig
            {
                IP = ip,
                Port = port,
                Channel = channel,
                //DataLength = dataLength,
                //StartAddress = startAddress

            };
            return config;
        }

        private int ValidateDataByInt(string content, string tip)
        {
            if (string.IsNullOrEmpty(content)) return 0;
            if (int.TryParse(content, out int value))
            {
                return value;
            }
            else
            {
                throw new Exception(tip);
            }
        }
    }
}

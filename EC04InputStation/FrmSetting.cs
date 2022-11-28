using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC04InputStation
{
    public partial class FrmSetting : Form
    {
        private SystemConfig _systemConfig;
        public FrmSetting(SystemConfig config)
        {
            _systemConfig= config;
            InitializeComponent();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            tbxIP1.Text = _systemConfig.RFIDConfigs[0].IP;
            tbxChannel1.Text = _systemConfig.RFIDConfigs[0].Channel.ToString();
            tbxPort1.Text = _systemConfig.RFIDConfigs[0].Port.ToString();
            tbxDataLength1.Text = _systemConfig.RFIDConfigs[0].DataLength.ToString();
            tbxStartAddress1.Text = _systemConfig.RFIDConfigs[0].StartAddress.ToString();

            tbxIP2.Text = _systemConfig.RFIDConfigs[1].IP;
            tbxChannel2.Text = _systemConfig.RFIDConfigs[1].Channel.ToString();
            tbxPort2.Text = _systemConfig.RFIDConfigs[1].Port.ToString();
            tbxDataLength2.Text = _systemConfig.RFIDConfigs[1].DataLength.ToString();
            tbxStartAddress2.Text = _systemConfig.RFIDConfigs[1].StartAddress.ToString();

            tbxIP3.Text = _systemConfig.RFIDConfigs[2].IP;
            tbxChannel3.Text = _systemConfig.RFIDConfigs[2].Channel.ToString();
            tbxPort3.Text = _systemConfig.RFIDConfigs[2].Port.ToString();
            tbxDataLength3.Text = _systemConfig.RFIDConfigs[2].DataLength.ToString();
            tbxStartAddress3.Text = _systemConfig.RFIDConfigs[2].StartAddress.ToString();

            tbxIP4.Text = _systemConfig.RFIDConfigs[3].IP;
            tbxChannel4.Text = _systemConfig.RFIDConfigs[3].Channel.ToString();
            tbxPort4.Text = _systemConfig.RFIDConfigs[3].Port.ToString();
            tbxDataLength4.Text = _systemConfig.RFIDConfigs[3].DataLength.ToString();
            tbxStartAddress4.Text = _systemConfig.RFIDConfigs[3].StartAddress.ToString();

            tbxCSVPath.Text = _systemConfig.CSVPath;
            tbxModel.Text = _systemConfig.Model;
            tbxFixtureId.Text = _systemConfig.FixtureID;
            tbxTestStation.Text = _systemConfig.TestStation;
        }
        private int ValidateDataByInt(string content,string tip)
        {
            if(string.IsNullOrEmpty(content)) return 0;
            if (int.TryParse(content, out int value))
            {
                return value;
            }
            else
            {
                throw new Exception(tip);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var json = JsonHelper.SerializeObject(_systemConfig);
            var systemConfig = JsonHelper.DeserializeObject<SystemConfig>(json);
            try
            {
                systemConfig.RFIDConfigs[0].IP = tbxIP1.Text;
                systemConfig.RFIDConfigs[0].Channel = ValidateDataByInt(tbxChannel1.Text, "通道不能设置为非数字");
                systemConfig.RFIDConfigs[0].Port = ValidateDataByInt(tbxPort1.Text, "端口不能设置为非数字");
                systemConfig.RFIDConfigs[0].DataLength = ValidateDataByInt(tbxDataLength1.Text, "内容长度不能设置为非数字");
                systemConfig.RFIDConfigs[0].StartAddress = ValidateDataByInt(tbxStartAddress1.Text, "地址不能设置为非数字");

                systemConfig.RFIDConfigs[1].IP = tbxIP2.Text;
                systemConfig.RFIDConfigs[1].Channel = ValidateDataByInt(tbxChannel2.Text, "通道不能设置为非数字");
                systemConfig.RFIDConfigs[1].Port = ValidateDataByInt(tbxPort2.Text, "端口不能设置为非数字");
                systemConfig.RFIDConfigs[1].DataLength = ValidateDataByInt(tbxDataLength2.Text, "内容长度不能设置为非数字");
                systemConfig.RFIDConfigs[1].StartAddress = ValidateDataByInt(tbxStartAddress2.Text, "地址不能设置为非数字");

                systemConfig.RFIDConfigs[2].IP = tbxIP3.Text;
                systemConfig.RFIDConfigs[2].Channel = ValidateDataByInt(tbxChannel3.Text, "通道不能设置为非数字");
                systemConfig.RFIDConfigs[2].Port = ValidateDataByInt(tbxPort3.Text, "端口不能设置为非数字");
                systemConfig.RFIDConfigs[2].DataLength = ValidateDataByInt(tbxDataLength3.Text, "内容长度不能设置为非数字");
                systemConfig.RFIDConfigs[2].StartAddress = ValidateDataByInt(tbxStartAddress3.Text, "地址不能设置为非数字");

                systemConfig.RFIDConfigs[3].IP = tbxIP4.Text;
                systemConfig.RFIDConfigs[3].Channel = ValidateDataByInt(tbxChannel4.Text, "通道不能设置为非数字");
                systemConfig.RFIDConfigs[3].Port = ValidateDataByInt(tbxPort4.Text, "端口不能设置为非数字");
                systemConfig.RFIDConfigs[3].DataLength = ValidateDataByInt(tbxDataLength4.Text, "内容长度不能设置为非数字");
                systemConfig.RFIDConfigs[3].StartAddress = ValidateDataByInt(tbxStartAddress4.Text, "地址不能设置为非数字");

                systemConfig.CSVPath = tbxCSVPath.Text;
                systemConfig.Model = tbxModel.Text;
                systemConfig.FixtureID = tbxFixtureId.Text;
                systemConfig.TestStation = tbxTestStation.Text;

                DataContent.SetConfig(systemConfig);
                MessageBox.Show("更改RFID需重启程序连接。");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

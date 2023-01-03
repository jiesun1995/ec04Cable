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

namespace Common
{
    public partial class FrmSetting : Form
    {
        private SystemConfig _systemConfig;
        private FrmRFIDSetting _frmRFIDSetting0;
        private FrmRFIDSetting _frmRFIDSetting1;
        private FrmRFIDSetting _frmRFIDSetting2;
        private FrmRFIDSetting _frmRFIDSetting3;
        private FrmRFIDSetting _frmRFIDSetting4;

        private readonly Action<GroupBox, GroupBox, GroupBox, GroupBox, GroupBox, GroupBox, GroupBox, GroupBox> _configGroup;

        public FrmSetting(Action<GroupBox, GroupBox, GroupBox, GroupBox, GroupBox, GroupBox, GroupBox, GroupBox> configGroup)
        {
            _systemConfig = DataContent.SystemConfig;
            InitializeComponent();
            _configGroup = configGroup;
            

        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            _configGroup(gbxRFID1, gbxRFID2, gbxRFID3, gbxRFID4,gbxRFID5, gbxStation, gbxPLC, gbxWCF);

            _frmRFIDSetting0 = new FrmRFIDSetting(_systemConfig.RFIDConfigs[0]);
            _frmRFIDSetting0.TopLevel = false;
            _frmRFIDSetting0.Dock = DockStyle.Fill;
            _frmRFIDSetting0.FormBorderStyle = FormBorderStyle.None;
            gbxRFID1.Controls.Add(_frmRFIDSetting0);
            _frmRFIDSetting0.Show();

            _frmRFIDSetting1 = new FrmRFIDSetting(_systemConfig.RFIDConfigs[1]);
            _frmRFIDSetting1.TopLevel = false;
            _frmRFIDSetting1.Dock = DockStyle.Fill;
            _frmRFIDSetting1.FormBorderStyle = FormBorderStyle.None;
            gbxRFID2.Controls.Add(_frmRFIDSetting1);
            _frmRFIDSetting1.Show();

            _frmRFIDSetting2 = new FrmRFIDSetting(_systemConfig.RFIDConfigs[2]);
            _frmRFIDSetting2.TopLevel = false;
            _frmRFIDSetting2.Dock = DockStyle.Fill;
            _frmRFIDSetting2.FormBorderStyle = FormBorderStyle.None;
            gbxRFID3.Controls.Add(_frmRFIDSetting2);
            _frmRFIDSetting2.Show();

            _frmRFIDSetting3 = new FrmRFIDSetting(_systemConfig.RFIDConfigs[3]);
            _frmRFIDSetting3.TopLevel = false;
            _frmRFIDSetting3.Dock = DockStyle.Fill;
            _frmRFIDSetting3.FormBorderStyle = FormBorderStyle.None;
            gbxRFID4.Controls.Add(_frmRFIDSetting3);
            _frmRFIDSetting3.Show();

            _frmRFIDSetting4 = new FrmRFIDSetting(_systemConfig.RFIDConfigs[4]??new RFIDConfig());
            _frmRFIDSetting4.TopLevel = false;
            _frmRFIDSetting4.Dock = DockStyle.Fill;
            _frmRFIDSetting4.FormBorderStyle = FormBorderStyle.None;
            gbxRFID5.Controls.Add(_frmRFIDSetting4);
            _frmRFIDSetting4.Show();

            tbxPLCIp.Text = _systemConfig.PLCConfigs[0].IP;
            tbxPLCPort.Text = _systemConfig.PLCConfigs[0].Port.ToString();
            cbxPLCType.DataSource = Enum.GetNames(typeof(PLCType));
            cbxPLCType.SelectedIndex =cbxPLCType.FindString(_systemConfig.PLCConfigs[0].Type.ToString());

            tbxPLCIp2.Text = _systemConfig.PLCConfigs[1].IP;
            tbxPLCPort2.Text = _systemConfig.PLCConfigs[1].Port.ToString();
            cbxPLCType2.DataSource = Enum.GetNames(typeof(PLCType));
            cbxPLCType2.SelectedIndex = cbxPLCType.FindString(_systemConfig.PLCConfigs[1].Type.ToString());

            tbxWCFServerIP.Text = _systemConfig.WCFSeverIp.ToString();
            tbxWCFServerPort.Text = _systemConfig.WCFSeverPort.ToString();
            tbxWCFClinetIP.Text = _systemConfig.WCFClinetIp.ToString();
            tbxWCFClinetPort.Text = _systemConfig.WCFClinetPort.ToString();

            tbxCSVPath.Text = _systemConfig.CSVPath;
            tbxModel.Text = _systemConfig.Model;
            tbxFixtureId.Text = _systemConfig.FixtureID;
            tbxTestStation.Text = _systemConfig.TestStation;
            nudScannerCode.Value = _systemConfig.ScannerCode;
            tbxConfirmStation.Text = _systemConfig.ConfirmStation;
            tbxMesUrl.Text = _systemConfig.MESUrl;

            ChangeScannerCode();

        }
        private int ValidateDataByInt(string content,string tip)
        {
            if(string.IsNullOrEmpty(content)) return 0;
            var value = 0;
            if (int.TryParse(content, out value))
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
                systemConfig.RFIDConfigs[0]= _frmRFIDSetting0.GetRFIDConfig();
                systemConfig.RFIDConfigs[1] = _frmRFIDSetting1.GetRFIDConfig();
                systemConfig.RFIDConfigs[2] = _frmRFIDSetting2.GetRFIDConfig();
                systemConfig.RFIDConfigs[3] = _frmRFIDSetting3.GetRFIDConfig();
                systemConfig.RFIDConfigs[4] = _frmRFIDSetting4.GetRFIDConfig();

                systemConfig.CSVPath = tbxCSVPath.Text;
                systemConfig.Model = tbxModel.Text;
                systemConfig.FixtureID = tbxFixtureId.Text;
                systemConfig.TestStation = tbxTestStation.Text;
                systemConfig.ScannerCode = Convert.ToInt32(nudScannerCode.Value);
                systemConfig.ConfirmStation = tbxConfirmStation.Text;
                systemConfig.MESUrl = tbxMesUrl.Text;

                systemConfig.PLCConfigs[0].IP = tbxPLCIp.Text;
                systemConfig.PLCConfigs[0].Port = ValidateDataByInt(tbxPLCPort.Text, "端口号不能为字符");
                systemConfig.PLCConfigs[0].Type = (PLCType)Enum.Parse(typeof(PLCType), cbxPLCType.SelectedItem.ToString(), false);

                systemConfig.PLCConfigs[1].IP = tbxPLCIp2.Text;
                systemConfig.PLCConfigs[1].Port = ValidateDataByInt(tbxPLCPort2.Text, "端口号不能为字符");
                systemConfig.PLCConfigs[1].Type = (PLCType)Enum.Parse(typeof(PLCType), cbxPLCType2.SelectedItem.ToString(), false);

                systemConfig.WCFSeverIp = tbxWCFServerIP.Text;
                systemConfig.WCFSeverPort = tbxWCFServerPort.Text;
                systemConfig.WCFClinetIp = tbxWCFClinetIP.Text;
                systemConfig.WCFClinetPort = tbxWCFClinetPort.Text;

                DataContent.SetConfig(systemConfig);
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ChangeScannerCode();
        }

        private void ChangeScannerCode()
        {
            if (nudScannerCode.Value > 0)
            {
                gbxWCFClient.Visible = true;
                gbxWCFServer.Visible = false;
            }
            else
            {
                gbxWCFClient.Visible = true;
                gbxWCFServer.Visible = true;
            }
        }
    }
}

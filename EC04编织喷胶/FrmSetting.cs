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

namespace EC04编织喷胶
{
    public partial class FrmSetting : Form
    {
        private SystemConfig _systemConfig;
        public FrmSetting()
        {
            _systemConfig= DataContent.SystemConfig;
            InitializeComponent();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {

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
                systemConfig.CSVPath = tbxCSVPath.Text;
                systemConfig.Model = tbxModel.Text;
                systemConfig.FixtureID = tbxFixtureId.Text;
                systemConfig.TestStation = tbxTestStation.Text;

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
    }
}

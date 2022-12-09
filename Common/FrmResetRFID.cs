using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Common
{
    public partial class FrmResetRFID : Form
    {
        private readonly RFIDChannel _RFIDChannel;
        private readonly MesService _mesService;
        private readonly Action _doWork;
        public FrmResetRFID(RFIDChannel RFIDChannel, MesService mesService, Action doWork)
        {
            _RFIDChannel = RFIDChannel;
            _mesService = mesService;
            InitializeComponent();
            _doWork = doWork;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var Sn = _RFIDChannel.Read();
                var station = _mesService.GetCurrStation(Sn);
                lblMessage.Text = $"SN：{Sn} \n当前站点：{station}";
                _doWork();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(_RFIDChannel.Wirte("0"))
            {
                lblMessage.Text = $"RFID重置成功";
            }
            else
            {
                lblMessage.Text = $"RFID重置失败";
            }
            _doWork();
        }
    }
}

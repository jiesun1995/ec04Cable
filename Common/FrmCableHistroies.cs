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
    public partial class FrmCableHistroies : Form
    {
        private readonly IFixtureCableBindService _fixtureCableBindService;
        public FrmCableHistroies()
        {
            InitializeComponent();
            _fixtureCableBindService = WCFHelper.CreateClient();
        }
        private void FrmCableHistroies_Load(object sender, EventArgs e)
        {
            var dateStart = DateTime.Now;
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 20)
                dateStart = DateTime.Now.Date.AddHours(8);
            else
                dateStart = DateTime.Now.Date.AddHours(20);
            dtStart.Value = dateStart;
            dtEnd.Value = dateStart.AddHours(12);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            var cable = txtCable.Text;
            var fixture = txtFixture.Text;
            var fixturePat = txtFixturePat.Text;
            var startDate = dtStart.Value;
            var endDate = dtEnd.Value;
            var dt = _fixtureCableBindService.QueryHistroy(cable, fixture, fixturePat, startDate, endDate);
            dgvData.DataSource = dt;
        }

        
    }
}

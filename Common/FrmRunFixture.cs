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
    public partial class FrmRunFixture : Form
    {
        private readonly IFixtureCableBindService _fixtureCableBindService;
        public FrmRunFixture()
        {
            InitializeComponent();
            _fixtureCableBindService = WCFHelper.CreateClient();
        }

        private void btnQuering_Click(object sender, EventArgs e)
        {
            var cable = txtCable.Text;
            var fixture = txtFixture.Text;
            var fixturePat = txtFixturePat.Text;
            var dt = _fixtureCableBindService.Query(cable, fixture, fixturePat);
            dgvData.DataSource = dt;
        }
    }
}

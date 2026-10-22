using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using clsDataAccessSettings;
using DVLD_Buisness;
namespace Project1.Applications.Application_Types
{
    public partial class frmListApplicationTypes: Form
    {
       private DataTable _dtApplcation;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtApplcation = clsApplication.GetAllApplicationType();

            dgvAppliactionType.DataSource = _dtApplcation;
            dgvAppliactionType.Columns[0].HeaderText = "ID";
            dgvAppliactionType.Columns[0].Width = 80;

            dgvAppliactionType.Columns[1].HeaderText = "Title";
            dgvAppliactionType.Columns[1].Width = 420;

            dgvAppliactionType.Columns[2].HeaderText = "Fees";
            dgvAppliactionType.Columns[2].Width = 100;

            lblRecordsCount.Text = ((dgvAppliactionType.Rows.Count) - 1).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvAppliactionType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}

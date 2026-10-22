using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1.Applications.Application_Types
{
    public partial class frmListTestTypes: Form
    {
       private DataTable dtApplicationType;

        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void ListtestType_Load(object sender, EventArgs e)
        {
            dtApplicationType = clsTestType.GetAllApplicationType();
            dgvAppliactionType.DataSource = dtApplicationType;
            dgvAppliactionType.Columns[0].HeaderText = "ID";
            dgvAppliactionType.Columns[0].Width = 80;

            dgvAppliactionType.Columns[1].HeaderText = "TestType Title";
            dgvAppliactionType.Columns[1].Width = 100;

            dgvAppliactionType.Columns[2].HeaderText = "Test Type Description";
            dgvAppliactionType.Columns[2].Width = 450;

            dgvAppliactionType.Columns[3].HeaderText = "Test Type Fees";
            dgvAppliactionType.Columns[3].Width = 190;

           lblRecordsCount.Text= ((dgvAppliactionType.Rows.Count)-1).ToString();
           
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((clsTestType.enTestType)dgvAppliactionType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            ListtestType_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

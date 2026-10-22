using clsDataAccessSettings;
using DVLD.Classes;
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
    public partial class frmEditApplicationType: Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode;

        private int ApplactionID=-1;
        private clsApplication Aplaction;
  
        public frmEditApplicationType(int applactionID)
        {
            InitializeComponent();
           this.ApplactionID = applactionID;
            _Mode = enMode.Update;
        }

        private void _LoadApplaction()
        {
            lblApplicationTypeID.Text = ApplactionID.ToString();
            Aplaction = clsApplication.Find(ApplactionID);

            if (Aplaction != null)
            {
                txtTitle.Text = Aplaction.Application;
                txtFees.Text = Aplaction.Fees.ToString();
              
            }

        }
        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
                _LoadApplaction();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            Aplaction.Application = txtTitle.Text.Trim();
            Aplaction.Fees = Convert.ToSingle(txtFees.Text.Trim());


            if (Aplaction.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);

            }
            ;


            if (!clsValidatoin.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
            ;
        }
    }
}

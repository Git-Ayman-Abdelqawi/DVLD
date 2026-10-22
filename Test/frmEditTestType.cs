using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;

namespace Project1.Applications.Application_Types
{
    public partial class frmEditTestType: Form
    {
        private clsTestType.enTestType _TestTypeID =clsTestType.enTestType.VisionTest;

        private clsTestType ApplactionType;
        public frmEditTestType(clsTestType.enTestType ID)
        {
            InitializeComponent();
           
            this._TestTypeID = ID;
        }
       
        private void _LoadApplaction()
        {
            lblID.Text = _TestTypeID.ToString();
            ApplactionType = clsTestType.Find(_TestTypeID);

            if (ApplactionType != null)
            {
                txtTitle.Text = ApplactionType.Title;
                txtFeel.Text = ApplactionType.TestTypeFees.ToString();
                txtDescriptaion.Text = ApplactionType.Descaription;
            }
           
        }
        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _LoadApplaction();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            ApplactionType.Title = txtTitle.Text.Trim();
            ApplactionType.TestTypeFees = Convert.ToSingle(txtFeel.Text.Trim());


            if (ApplactionType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

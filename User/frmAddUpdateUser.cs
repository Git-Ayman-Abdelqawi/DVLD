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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project1.User
{
    public partial class frmAddUpdateUser: Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode;

        private int _UserID = -1;
        private clsUser _User;

      
     
        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.Update;
        }
        public frmAddUpdateUser()
        {
            InitializeComponent();
        }


        private void  _ResetDefualtValues()
        {

            if (_Mode ==enMode.AddNew)
            {
                _User = new clsUser();
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                tpLoginInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }


            txtConfirmPassword.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            chIsActive.Checked = true;

        }

    
        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }


            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chIsActive.Checked = _User.IsActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
            }
             
            if (ctrlPersonCardWithFilter1.PersonId != -1)
            {
                if(clsUser.isUserExistForPersonID(ctrlPersonCardWithFilter1.PersonId ) )
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();

                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }
                    
            }
            else
            {
                MessageBox.Show("Please select a Person first.", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                     "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonId;
            _User.IsActive = chIsActive.Checked;
            _User.Password = txtPassword.Text.Trim();
            _User.UserName = txtUserName.Text.Trim();

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();

                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }



        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            string txt = txtConfirmPassword.Text.Trim();
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {


                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Only number are allowed!");


            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }


           
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

            if ( string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Only number are allowed!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }

            if (_Mode == enMode.AddNew)
            {
                if (clsUser.isUserExist(txtUserName.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");

                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
                ;

            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.isUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                    ;
                }
            }
            }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Only number are allowed!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

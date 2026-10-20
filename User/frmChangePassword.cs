using DVLD.Classes;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1.User
{
    public partial class frmChangePassword: Form
    {
        private int _UserID = -1;
        private clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            this._UserID = UserID;
           


        }

        private void _ResetDefultValues()
        {
            txtCurrentPassowrd.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassowrd.Text = "";
            txtCurrentPassowrd.Focus();
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefultValues();
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                MessageBox.Show("Could not Find User with id = " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void txtCurrentPassowrd_Validating(object sender, CancelEventArgs e) 
        {

            if (string.IsNullOrEmpty(txtCurrentPassowrd.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassowrd, "Username cannot by blank!");
            }
            else
            {
                e.Cancel = false;

                errorProvider1.SetError(txtCurrentPassowrd, "");
            }



           
                if (txtCurrentPassowrd.Text.Trim() != _User.Password)
                {
                        e.Cancel = true;
                        errorProvider1.SetError(txtCurrentPassowrd, "Current Passowrd Woring!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtCurrentPassowrd, "");
                  
                }


            
            
        }


        private void txtCurrentPassowrd_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {



                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "This field is required!");
            }
            else
            {
                e.Cancel = false;
             
                errorProvider1.SetError(txtNewPassword, "");
                

            }
        }

        private void txtConfirmPassowrd_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassowrd.Text.Trim()))
            {



                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassowrd, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassowrd, "");


            }



            if (txtConfirmPassowrd.Text.Trim() != txtNewPassword.Text.Trim())
            {



                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassowrd, "This field is No!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassowrd, "");


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = txtNewPassword.Text.Trim();




            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.",
                  "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefultValues();
            }
            else
                MessageBox.Show("An Erro Occured, Password did not change.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            


        

}

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

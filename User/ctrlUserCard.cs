using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;

namespace Project1.User
{
    public partial class ctrlUserCard: UserControl
    {
        private int _UserID = -1;
       
        private clsUser _User;

      
        public ctrlUserCard()
        {
            InitializeComponent();
          
        }


        public int UserID
        {
            get { return _UserID; }
        }

        private void _ResetPersonInfo()
        {

            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblisActive.Text = "[???]";
        }
        public void LoadUserInfo(int UserId)
        {
            _UserID = UserId;
            _User = clsUser.FindByUserID(UserId);
            if (_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
              
            }
            _FillUserInfo();
           
        }

        public void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            if (_User.IsActive )
                lblisActive.Text = "Yes";
            else
                lblisActive.Text = "No";
           
        }
        private void ctrlUserCard_Load(object sender, EventArgs e)
        {
           
        }
    }
}

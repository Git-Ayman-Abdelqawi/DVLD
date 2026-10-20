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
using Project1.People;
namespace Project1.User
{
   
    
    public partial class frmListUser: Form
    {
      private static  DataTable _dtAllUser ;

        public frmListUser()
        {
            InitializeComponent();
        }

        private void _RefreshUserList()
        {
            _dtAllUser = clsUser.GetAllUsers();

            dgvUsers.DataSource = _dtAllUser;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

        }
        private void button1_Click(object sender, EventArgs e)
        {

            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
        }

        private void frmListUser_Load(object sender, EventArgs e)
        {

          _dtAllUser = clsUser.GetAllUsers();

            
            cbFilterBy.SelectedIndex = 0;
            dgvUsers.DataSource = _dtAllUser;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 100;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 100;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 300;

                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 190;

                dgvUsers.Columns[4].HeaderText = "ISActive";
                dgvUsers.Columns[4].Width = 80;

            }

        }

        private void showDetilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshUserList();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;


            if (clsUser.DeleteUser(UserID))
            {
                if (clsPerson.DeletePerson((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User has been Deleted Successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListUser_Load(null, null);
                }

                else
                    MessageBox.Show("User is not deleted due to  data connected to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListUser_Load(null, null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "is Active")
            {
                txtFilterValue.Visible = false;
                cbiActive.SelectedIndex = 0;
                cbiActive.Visible = true;
                cbiActive.Focus();

            }else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbiActive.Visible = false;
                
                    txtFilterValue.Visible = true;
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "None":
                    FilterColumn = "None";
                    break;
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "is Active":
                    FilterColumn = "isActive";
                    break;
                default:
                    break;
            }
            if (FilterColumn == "None" || string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                _dtAllUser.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAllUser.Rows.Count.ToString();
                return;
            }
            else if (FilterColumn == "PersonID" || FilterColumn == "UserID")
            {

                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else if (FilterColumn == "FullName" || FilterColumn == "UserName")
            {
                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
          

                lblRecordsCount.Text = _dtAllUser.Rows.Count.ToString();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            

                    if (cbiActive.Text == "All")
                    {
                _RefreshUserList();
                    }

                    else if (cbiActive.Text == "Yes")
                    {
                _dtAllUser.DefaultView.RowFilter = string.Format("[ISActive] ={0}", true);

                    }
                    else if (cbiActive.Text == "No")
                    {
                _dtAllUser.DefaultView.RowFilter = string.Format("[ISActive] ={0}", false);

                    }
            
        }

        private void changePassowrdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshUserList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            _RefreshUserList();
        }
    }
}

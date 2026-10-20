using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1.User
{
    public partial class frmUserInfo: Form
    {

        public int UserID = -1;
        public frmUserInfo(int userID)
        {
            InitializeComponent();
            this.UserID = userID;
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
             ctrlUserCard1.LoadUserInfo(UserID );
          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

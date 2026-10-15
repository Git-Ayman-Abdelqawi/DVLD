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

namespace Project1
{
    public partial class Form2: Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void GetAllPeople()
        {
         
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.Show();
        }
    }
}

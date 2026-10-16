using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1.People
{
    public partial class frmFindPerson: Form
    {

        public delegate void DataBackEventHander(object sender, int Person);


        public DataBackEventHander DataBack;
        public frmFindPerson()
        {
            InitializeComponent();
        }

        private void frmFindPerson_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this,ctrlPersonCardWithFilter1.PersonID);
        }
    }
}

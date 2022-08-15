using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManagement
{
    public partial class Main_page : Form
    {
        public Main_page()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_login adminlogin = new Admin_login();
            adminlogin.Show();
        }

        private void butemployee_Click(object sender, EventArgs e)
        {
            this.Hide();
            Emp_Registration empregistration = new Emp_Registration();
            empregistration.Show();
        }

        private void butGroup_Click(object sender, EventArgs e)
        {
            this.Hide();
            Department_login departmentlogin = new Department_login();
            departmentlogin.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace TaskManagement
{
    public partial class Emp_login : Form
    {
        public static string id = "";

        int attempt = 1;
        public Emp_login()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Emp_Registration empregistration = new Emp_Registration();
            empregistration.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmpId.Text = "";
            txtEmpPassword.Text = "";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtEmpId.Text == "")
            {
                errorEmpMobileNO.SetError(txtEmpId, "Enter Mobile No");
            }
            else if (txtEmpPassword.Text == "")
            {
                errorEmpPassword.SetError(txtEmpPassword, "Enter Password");
            }
            else
            {
                errorEmpMobileNO.Clear();
                errorEmpPassword.Clear();
            }

            if (attempt < 4)
            {
                string constring = ConfigurationManager.ConnectionStrings["TaskManagement"].ToString();
                SqlConnection con = new SqlConnection(constring);
                SqlCommand cmd = new SqlCommand("select * from Employee_table where id = '" + txtEmpId.Text + "' and password = '" + txtEmpPassword.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    id = txtEmpId.Text;

                    this.Hide();
                    Main_page mainpage = new Main_page();
                    mainpage.Hide();

                    Emp_panel emppanel = new Emp_panel();
                    emppanel.Show();
                }
                else
                {
                    MessageBox.Show("Invalide detail you attempt is: " + attempt);
                }
             }
             else if (attempt == 4)
             {
                MessageBox.Show("Login attempt exceed");
                txtEmpPassword.Enabled = false;
             }
             attempt++;

        }

        private void Emp_login_Load(object sender, EventArgs e)
        {

        }
    }
}

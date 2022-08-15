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
    public partial class Emp_Registration : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public Emp_Registration()
        {
            InitializeComponent();
        }

        private void Emp_Registration_Load(object sender, EventArgs e)
        {
            string stringconnected = ConfigurationManager.ConnectionStrings["TaskManagement"].ToString();
            con = new SqlConnection(stringconnected);
        }

        private void btnSubmitReg_Click(object sender, EventArgs e)
        {
            if(txtEmpUserNameReg.Text  == "")
            {
                errorUserName.SetError(txtEmpUserNameReg, "Enter User Name");
            }
            else if (txtEmpMobileReg.Text == "")
            {
                errorUserName.Clear();
                errorMobileNo.SetError(txtEmpMobileReg, "Enter Mobile No");
            }
            else if (txtEmpPasswordReg.Text == "")
            {
                errorMobileNo.Clear();
                errorPassword.SetError(txtEmpPasswordReg, "Enter Password");
            }
            else if(comboBoxDepartment.SelectedIndex == -1)
            {
                errorPassword.Clear();
                errorDepartment.SetError(comboBoxDepartment, "Select Department");
            }
            else
            {
                errorDepartment.Clear();

                cmd = new SqlCommand("insert into Employee_table(user_name,mobile_no,department,password) values(@username,@mobile_no,@department,@password) ", con);
                cmd.Parameters.Add("@username", txtEmpUserNameReg.Text);
                cmd.Parameters.Add("@mobile_no", txtEmpMobileReg.Text);
                cmd.Parameters.Add("@department", comboBoxDepartment.SelectedItem);
                cmd.Parameters.Add("@password", txtEmpPasswordReg.Text);
                con.Open();
                try
                {
                    int record = cmd.ExecuteNonQuery();
                    MessageBox.Show("Record added successfully");
                }
                catch (Exception)
                {
                    MessageBox.Show("Mobile no already registerd");
                }
                finally
                {
                    con.Close();
                }

            }

        }

        private void btnBackReg_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_page mainpage = new Main_page();
            mainpage.Show();
        }

        private void btnClearReg_Click(object sender, EventArgs e)
        {
            txtEmpUserNameReg.Text = "";
            txtEmpMobileReg.Text = "";
            txtEmpPasswordReg.Text = "";
            comboBoxDepartment.SelectedIndex = -1;
        }

        private void btnLoginPage_Click(object sender, EventArgs e)
        {
            this.Hide();
            Emp_login emplogin = new Emp_login();
            emplogin.Show();
        }
    }
}

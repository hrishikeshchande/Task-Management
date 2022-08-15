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
    public partial class Admin_Detail : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public Admin_Detail()
        {
            InitializeComponent();
        }

        private void Admin_Detail_Load(object sender, EventArgs e)
        {
            string stringconnected = ConfigurationManager.ConnectionStrings["TaskManagement"].ToString();
            con = new SqlConnection(stringconnected);
            GetData();
        }

        private void GetData()
        {
            cmd = new SqlCommand("Select * from Admin_table", con);
            con.Open();
            reader = cmd.ExecuteReader();           //ExecuteReader to dealing with the select statement
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void btnNewAdmin_Click(object sender, EventArgs e)
        {
            //check the all fill are not empty
            if (txtAdminUserName.Text == "")
            {
                MessageBox.Show("Enter the User Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtAdminPassword.Text == "")
            {
                MessageBox.Show("Enter the Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cmd = new SqlCommand("insert into Admin_table(user_name,password) values(@username,@password) ", con);
            cmd.Parameters.Add("@username", txtAdminUserName.Text);
            cmd.Parameters.Add("@password", txtAdminPassword.Text);
            con.Open();
            try
            {
                txtAdminUserName.Text = "";
                txtAdminPassword.Text = "";
                int record = cmd.ExecuteNonQuery();             
                MessageBox.Show("Record added successfully");
            }
            catch (Exception)
            {
                MessageBox.Show("You can not add duplicate data");
            }
            finally
            {
                con.Close();
            }
            GetData();
        }

        private void btnUpdateAdmin_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Update Admin_table set password = @password where user_name = @username", con);
            cmd.Parameters.Add("@username", txtAdminUserName.Text);
            cmd.Parameters.Add("@password", txtAdminPassword.Text);
            con.Open();

            try
            {
                txtAdminUserName.Text = "";
                txtAdminPassword.Text = "";
                int record = cmd.ExecuteNonQuery();                 //ExecuteNonQuery:-to deleaing with the DML statement 
                MessageBox.Show("Record Updated Succesfully");
            }
            catch (Exception)
            {
                MessageBox.Show("First Insert some records");
            }
            finally
            {
                con.Close();
            }
            GetData();
        }


        private void btnDeleteAdmin_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("delete from Admin_table where user_name = @username", con);
            cmd.Parameters.Add("@username", txtAdminUserName.Text);
            con.Open();

            try
            {
                txtAdminUserName.Text = "";
                txtAdminPassword.Text = "";
                int record = cmd.ExecuteNonQuery();                 //ExecuteNonQuery:-to deleaing with the DML statement 
                MessageBox.Show("Record Updated Succesfully");
            }
            catch (Exception)
            {
                MessageBox.Show("First Insert some records");
            }
            finally
            {
                con.Close();
            }
            GetData();
        }

        private void btnCountAdmin_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select count(*)from Admin_table", con);
            con.Open();
            int record = (int)cmd.ExecuteScalar();      //ExecuteScalar:-to deleaing with the aggregate function like count
            labCount.Text = record.ToString();
            con.Close();
        }
        
    }
}

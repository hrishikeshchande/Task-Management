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
    public partial class Department_Front_End : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public Department_Front_End()
        {
            InitializeComponent();
        }

        private void Department_Front_End_Load(object sender, EventArgs e)
        {
            string stringconnected = ConfigurationManager.ConnectionStrings["TaskManagement"].ToString();
            con = new SqlConnection(stringconnected);
            GetDataTask();
            GetDataEmp();
        }
        private void GetDataTask()
        {
            cmd = new SqlCommand("Select * from Task_table where task_department = 'front end'", con);
            con.Open();
            reader = cmd.ExecuteReader();           //ExecuteReader to dealing with the select statement
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridViewTask.DataSource = dt;
            con.Close();
        }
        private void GetDataEmp()
        {
            cmd = new SqlCommand("Select user_name, mobile_no, department, id from Employee_table where department = 'Front End'", con);
            con.Open();
            reader = cmd.ExecuteReader();           //ExecuteReader to dealing with the select statement
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridViewEmp.DataSource = dt;
            con.Close();
        }

        private void dataGridViewTask_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dvg = sender as DataGridView;
            if (dvg != null && dvg.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dvg.SelectedRows[0];
                if (row != null)
                {
                    labTaskId.Text = row.Cells[0].Value.ToString();
                }
            }
        }

        private void btnUpdateTaskToEmp_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Update Task_table set emp_id = @empid where id = @id", con);
            cmd.Parameters.Add("@empid", txtEmpIdToTask.Text);
            cmd.Parameters.Add("@id", labTaskId.Text);
            con.Open();

            try
            {
                txtEmpIdToTask.Text = "";
                labTaskId.Text = "";
                int record = cmd.ExecuteNonQuery();                 //ExecuteNonQuery:-to deleaing with the DML statement 
                MessageBox.Show("Task Assign Succesfully");
            }
            catch (Exception)
            {
                MessageBox.Show("Task Assign Unsuccesfully");
            }
            finally
            {
                con.Close();
            }
            GetDataTask();
        }

        private void btnBackAdmin_Click(object sender, EventArgs e)
        {
            this.Hide();

            Department_login departmentlogin = new Department_login();
            departmentlogin.Show();
        }
    }
}

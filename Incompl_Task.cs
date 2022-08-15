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
    public partial class Incompl_Task : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        public Incompl_Task()
        {
            InitializeComponent();
        }
        private void GetData()
        {
            cmd = new SqlCommand("Select * from Task_table where complet = 'false'", con);
            con.Open();
            reader = cmd.ExecuteReader();           //ExecuteReader to dealing with the select statement
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }
       
        private void Incompl_Task_Load(object sender, EventArgs e)
        {
            string stringconnected = ConfigurationManager.ConnectionStrings["TaskManagement"].ToString();
            con = new SqlConnection(stringconnected);
            GetData();
        }
    }
}

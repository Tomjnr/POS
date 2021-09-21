using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class ViewBills : Form
    {
        public ViewBills()
        {
            InitializeComponent();
            //datagridview properties
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //selection of datagrid view
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
      
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           

        }

        private void ViewBills_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");

            String query = "SELECT BDate as Date,CustName as Customer ,PMethod as PaidWith,Amount FROM BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}

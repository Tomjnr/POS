using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace POS
{
    public partial class ViewCustomers : Form
    {
        public ViewCustomers()
        {
            InitializeComponent();
            //datagridview properties
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //selection of datagrid view
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }
        SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");
        SqlDataAdapter sda = new SqlDataAdapter();
        private void Reset()
        {
            textBox1.Text = "";
            textBox4.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";

        }
        private void button3_Click(object sender, EventArgs e)
        {
            MainMenu mn = new MainMenu();
            mn.Show();
            this.Close();
        }

        private void ViewCustomers_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");

            String query = "SELECT CustId As Num ,CustName As Name ,CustAddress As Adress,CustPhone As PhoneNo FROM CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox5.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }
        private void Displayproduct()
        {
            SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");

            String query = "SELECT CustId As Num ,CustName As Name ,CustAddress As Adress,CustPhone As PhoneNo FROM CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Select Customer To Update and update Fields Required");
            }
            else
            {
                con.Open();
                string query = "update  CustomerTbl set CustName='" + textBox1.Text + "',CustAddress='" + textBox2.Text + "',CustPhone='" + textBox4.Text + "' where CustId=" + textBox5.Text + "; ";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Updated");
                Reset();
                Displayproduct();
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Select Customer To Delete");
            }
            else
            {
                con.Open();
                string query = "delete from CustomerTbl where CustId='" + textBox5.Text + "' ";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Cutomer Successfully Deleted");
                Reset();
                Displayproduct();
                con.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}

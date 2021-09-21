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
    public partial class ViewSuppliers : Form
    {
        public ViewSuppliers()
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
            textBox3.Text = "";
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

        private void ViewSuppliers_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");

            String query = "SELECT SupId As Num ,SupName As Name ,SupAddress As Adress,SupPhone As PhoneNo,SupRem As Remarks FROM SupplierTbl";
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
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void Displayproduct()
        {
            SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");

            String query = "SELECT SupId As Num ,SupName As Name ,SupAddress As Adress,SupPhone As PhoneNo,SupRem As Remarks FROM SupplierTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Select Supplier To Update and update Fields Required");
            }
            else
            {
                con.Open();
                string query = "update  SupplierTbl set SupName='" + textBox1.Text + "',SupAddress='" + textBox2.Text + "',SupPhone='" + textBox4.Text + "',SupRem='" + textBox3.Text + "' where SupId=" + textBox5.Text + "; ";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Supplier Successfully Updated");
                Reset();
                Displayproduct();
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Select Supplier To Delete");
            }
            else
            {
                con.Open();
                string query = "delete from SupplierTbl where SupId='" + textBox5.Text + "' ";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Supplier Successfully Deleted");
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

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
    public partial class ViewProduct : Form
    {
        public ViewProduct()
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
            comboBox1.Text = "";
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainMenu mn = new MainMenu();
            mn.Show();
            this.Close();
        }

        private void ViewProduct_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");

            String query = "SELECT PId As Num ,PName As Name ,PCat As Category,PPrice As price,pQuant As Quantity FROM ProductsTbl";
           SqlDataAdapter sda = new SqlDataAdapter(query, con);
           DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void Displayproduct()
        {
            SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");

            String query = "SELECT PId As Num ,PName As Name ,PCat As Category,PPrice As price,pQuant As Quantity FROM ProductTsbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox4.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Select Product To Update and update Fields Required");
            }
            else
            {
                con.Open();
                string query = "update  ProductsTbl set PName='" + textBox1.Text + "',PCat='" + comboBox1.Text + "',PPrice='" + textBox4.Text + "',pQuant='" + textBox3.Text + "' where PId=" + textBox2.Text + "; ";
               SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Updated");
                Reset();
                Displayproduct();
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox4.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Select Product To Delete");
            }
            else
            {
                con.Open();
                string query = "delete from ProductsTbl where PId='" + textBox2.Text + "' ";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Deleted");
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

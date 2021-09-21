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
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");
        private void Reset()
        {
            ProductNametxt.Text = "";
            productCatCmb.Text= "";
            ProductPricetxt.Text = "";
            productQuantitytxt.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ProductNametxt.Text == ""||productCatCmb.SelectedIndex==-1||ProductPricetxt.Text==""||productQuantitytxt.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
           else {

                try
                {
                    con.Open();
                    string query = "insert into ProductsTbl values('" + ProductNametxt.Text + "','" + productCatCmb.Text + "','" + ProductPricetxt.Text + "','" + productQuantitytxt.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Saved");
                    Reset();
                    con.Close();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();

        }
    }
}

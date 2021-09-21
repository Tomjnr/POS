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
    public partial class Billings : Form
    {
        public Billings()
        {
            InitializeComponent();
            DisplayProduct();
            GetCustomer();
            //datagridview properties
            productsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //selection of datagrid view
            productsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            productsDGV.MultiSelect = false;

            //datagridview properties
            BillsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //selection of datagrid view
            BillsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BillsDGV.MultiSelect = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");
        public void DisplayProduct()
        {
          //  con.Open();

            String query = "SELECT PId As Num ,PName As Name ,PCat As Category,PPrice As price,pQuant As Quantity FROM ProductsTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            productsDGV.DataSource = dt;
            con.Close();

        }
        private void GetCustomerName()
        {
            SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");
            con.Open();

            //String query = "SELECT CustId As Num ,CustName As Name ,CustAddress As Adress,CustPhone As PhoneNo FROM CustomerTbl";
            String query = "SELECT * FROM CustomerTbl where CustId='"+CustomerIdCmb.SelectedValue.ToString()+"'";
           // SqlCommand cmd = new SqlCommand(query, con);
          //  SqlDataReader rdr;
           // rdr = cmd.ExecuteReader();
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
           // dt.Load(rdr);
          //  CustomerIdCmb.ValueMember = "CustId";
           // CustomerIdCmb.DataSource = dt;
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                CustomerNameTb.Text = dr["CustName"].ToString();
            }
            //dataGridView1.DataSource = dt;
            con.Close();
        }
        public void SearchProduct()
        {
            //con.Open();

            String query = "SELECT  PId As Num ,PName As Name ,PCat As Category,PPrice As price,pQuant As Quantity FROM ProductsTbl where PName='" + textBox4.Text + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            productsDGV.DataSource = dt;
            con.Close();

        }
        private void button5_Click(object sender, EventArgs e)
        {
            SearchProduct();
            textBox4.Text = "";

        }

        private void Billings_Load(object sender, EventArgs e)
        {
            DisplayProduct();
            textBox4.Text = "";

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DisplayProduct();
            textBox4.Text = "";
        }

        private void GetCustomer()
        {
            SqlConnection con = new SqlConnection(@"Data Source=mwangi\tom;Initial Catalog=POS;Integrated Security=True");
            con.Open();

            //String query = "SELECT CustId As Num ,CustName As Name ,CustAddress As Adress,CustPhone As PhoneNo FROM CustomerTbl";
            String query = "SELECT CustId FROM CustomerTbl";
            SqlCommand cmd = new SqlCommand(query,con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            //SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(rdr);
            CustomerIdCmb.ValueMember = "CustId";
            CustomerIdCmb.DataSource = dt;
            //sda.Fill(dt);
            //dataGridView1.DataSource = dt;
            con.Close();
        }
        private void Reset()
        {
            PName = "";
            QtyTb.Text = "";
            key = 0;
        }
        private void UpdateQty()
        {
            int NewQty = PStock - Convert.ToInt32(QtyTb.Text);
            con.Open();
            string query = "update  ProductsTbl set pQuant='" +NewQty+ "' where PId=" + textBox9.Text + "; ";
            SqlCommand command = new SqlCommand(query, con);
            command.ExecuteNonQuery();

            DisplayProduct();
            con.Close();
        }
        int key = 0;
        string PName;
        int Pprice, PStock;
        int n = 1,Total=0;

        private void AddToBill_Click(object sender, EventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("Select A Product");
            }else if (QtyTb.Text=="")
            {
                MessageBox.Show("Enter the Quantity");
            }else if (Convert.ToInt32(QtyTb.Text) > PStock)
            {
                MessageBox.Show("No Enough Stock");

            }
            else
            {
                int subTotal = Convert.ToInt32(QtyTb.Text)* Pprice;
                Total = Total + subTotal;
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillsDGV);
                newRow.Cells[0].Value = n;
                newRow.Cells[1].Value = PName;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value = Pprice;
                newRow.Cells[4].Value = subTotal;
                BillsDGV.Rows.Add(newRow);
                n++;
                SubTotalTb.Text = "" + Total;
                UpdateQty();
                Reset();
            }
        }

        private void CustomerIdCmb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void VATtB_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void VATtB_KeyUp(object sender, KeyEventArgs e)
        {
            if (VATtB.Text=="")
            {

            }
            else if (SubTotalTb.Text=="")
            {
                MessageBox.Show("Select Product First");
                VATtB.Text = "";

            }
            else { 
            try
            {
                double VAT = (Convert.ToDouble(VATtB.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
           
                TotalTaxTb.Text = "" + VAT;//allows to insert an ineterger as a string
                    GrdTotalTb.Text = ""+(Convert.ToInt32(SubTotalTb.Text) + Convert.ToDouble(TotalTaxTb.Text));
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            }
        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            if (DiscountTb.Text == "")
            {

            }
            else if (SubTotalTb.Text == "")
            {
                MessageBox.Show("Select Product First");
                DiscountTb.Text = "";

            }
            else
            {
                try
                {
                    double Disc = (Convert.ToDouble(DiscountTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);

                    DiscTotalTb.Text = "" + Disc;//allows to insert an ineterger as a string
                    GrdTotalTb.Text = "" + (Convert.ToInt32(SubTotalTb.Text) + Convert.ToDouble(TotalTaxTb.Text)- Convert.ToDouble(DiscTotalTb.Text));
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }
        private void InsertBill()
        {
            if (CustomerIdCmb.SelectedIndex == -1 || paymentM.SelectedIndex == -1 || GrdTotalTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {

                /*con.Open();
                string query = "insert into BillTbl values(' "+this. doB.Value.ToString() +" ','" + CustomerIdCmb.Text + "','" + CustomerNameTb.Text + "','" + paymentM.SelectedItem.ToString() + "','" +(Convert.ToDouble(GrdTotalTb.Text)) + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Saved");
                //  Reset();
                con.Close();*/
                try
                 {
                     con.Open();
                string query = "insert into BillTbl values(' "+this. doB.Value.ToString() +" ','" + CustomerIdCmb.Text + "','" + CustomerNameTb.Text + "','" + paymentM.SelectedItem.ToString() + "','" +(Convert.ToDouble(GrdTotalTb.Text)) + "')";
                     SqlCommand cmd = new SqlCommand(query, con);
                     cmd.ExecuteNonQuery();
                     MessageBox.Show("Bill Saved");
                   //  Reset();
                     con.Close();
                 }
                 catch (Exception Ex)
                 {
                     MessageBox.Show(Ex.Message);
                 }
            }
        }
        int flag = 0;
        private void printBtn_Click(object sender, EventArgs e)
        {
            InsertBill();
            if (flag == 1)
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
        }
        int prodid, prodqty, prodprice, tottal, pos = 60;

        private void paymentM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            ViewBills obj = new ViewBills();
            obj.Show();
            obj.TopMost = true;
            
        }

        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Marcotel POS",new Font("Century Gothic",12,FontStyle.Bold),Brushes.Red,new Point(80));
            e.Graphics.DrawString("ID PRODUCT PRICE QUANITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26,40));
            foreach(DataGridViewRow row in BillsDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = ""+row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                tottal = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString(""+prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString(""+prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString(""+prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString(""+prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString(""+prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));

            }
            e.Graphics.DrawString("Grand Total:Ksh" + GrdTotalTb.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos+50));
            e.Graphics.DrawString("******Develeped by 0720017114******" , new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(10, pos+85));
            BillsDGV.Rows.Clear();
            //BillsDGV.RowsRefresh();
            pos = 100;
            GrdTotalTb.Text = "";
            n = 0;
        }

        private void CustomerIdCmb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustomerName();
        }

        private void productsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
              textBox9.Text = productsDGV.SelectedRows[0].Cells[0].Value.ToString();

            PName = productsDGV.SelectedRows[0].Cells[1].Value.ToString();
            //  comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            Pprice =Convert.ToInt32(productsDGV.SelectedRows[0].Cells[3].Value.ToString());
            PStock = Convert.ToInt32(productsDGV.SelectedRows[0].Cells[4].Value.ToString());
            if (PName == "")
            {
                 key = 0;
            }
            else
            {
                key = Convert.ToInt32(productsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}

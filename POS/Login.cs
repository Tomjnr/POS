using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Billings lg = new Billings();
            lg.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserN.Text == "" && Pass.Text == "")
            {
                MessageBox.Show("Enter UserName and Password");
            }
          else   if(UserN.Text=="MAWAYA"&& Pass.Text=="MAWAYA")
            {
                MainMenu MN = new MainMenu();
                MN.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Credentials");
            }
        }
    }
}

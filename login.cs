using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Internal_Assessment_Database
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select * from internals.login where UserName='" + this.textBox1.Text + "' and Password='" + this.textBox2.Text + "';", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                int count = 0;
                while (myReader.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    MessageBox.Show("YOU ARE AN AUTHORIZED USER...ACCESS GRANTED.");
                    this.Hide();
                    mainform f2 = new mainform();
                    f2.ShowDialog();



                }
                else if (count > 1)
                {
                    MessageBox.Show("DUPLICATE USERNAME AND PASSWORD...ACCESS DENIED.");
                }
                else
                {
                    MessageBox.Show("YOU SEEM TO BE AN UNAUTHORIZED USER...ACCESS DENIED.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://vizayyadav.blogspot.com/");
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

		private void login_Load(object sender, EventArgs e)
		{
			this.ActiveControl = textBox1;
			textBox1.Focus();
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode.Equals(Keys.Down))
			{
				e.Handled = true;
				textBox2.Focus();
			}
		}

		private void textBox2_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				button1.PerformClick();
			}
			if (e.KeyCode.Equals(Keys.Up))
			{
				e.Handled = true;
				textBox1.Focus();
			}
		}
	}
}

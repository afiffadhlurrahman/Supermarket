using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Supermarket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string SellerName = "";
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\Documents\smarketdb.mdf;Integrated Security=True;Connect Timeout=30");


        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            UnameTb.Text = "";
            PassTb.Text = "";
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if(UnameTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Enter The Username and Password");
            }
            else
            {
                if(RolerCb.SelectedIndex > -1)
                {
                    if (RolerCb.SelectedItem.ToString() == "ADMIN")
                    {
                        if(UnameTb.Text == "Admin" && PassTb.Text == "Admin")
                        {
                            ProductForm prod = new ProductForm();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If You are the Admin, Enter the Correct Username and Password");
                        }
                    }
                    else
                    {
                        //MessageBox.Show("You are in the Seller Section");
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select count(8) from SellerTbl where SellerName='" + UnameTb.Text + "' and SellerPass='" +PassTb.Text+"'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if(dt.Rows[0][0].ToString() == "1")
                        {
                            SellerName = UnameTb.Text;
                            SellingForm sell = new SellingForm();
                            sell.Show();
                            this.Hide();
                            Con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Username and/or Password");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select a Role");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

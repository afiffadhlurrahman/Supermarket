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
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\afiff\OneDrive\Documents\smarketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void CatAddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "Insert into CategoryTbl values (" + catIdTb.Text + ",'" + catNameTb.Text + "','" + catDescTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully");
                Con.Close();
                populate();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            catDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void catDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            catIdTb.Text = catDGV.SelectedRows[0].Cells[0].Value.ToString();
            catNameTb.Text = catDGV.SelectedRows[0].Cells[1].Value.ToString();
            catDescTb.Text = catDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void CatDelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(catIdTb.Text == "")
                {
                    MessageBox.Show("Select the category");
                }
                else
                {
                    Con.Open();
                    string query = "delete from CategoryTbl where CatId = " + catIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category deleted successfully");
                    Con.Close();
                    populate();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CatEditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (catIdTb.Text == "" || catNameTb.Text == "" || catDescTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update CategoryTbl set CatName = '" + catNameTb.Text + "', CatDesc = '" + catDescTb.Text + "' where CatId = " + catIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category successfully updated");
                    Con.Close();
                    populate();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();
            prod.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellerForm seller = new SellerForm();
            seller.Show();
            this.Hide();
        }
    }
}

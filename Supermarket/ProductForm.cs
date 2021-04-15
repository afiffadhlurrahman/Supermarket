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
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\afiff\OneDrive\Documents\smarketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void fillcombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CatName from CategoryTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            CatCb.ValueMember = "catName";
            CatCb.DataSource = dt;
            Con.Close();
        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillcombo();
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "Insert into ProductTbl values (" + prodIdTb.Text + ",'" + prodNameTb.Text + "', " + prodQtyTb.Text + ", " + prodPriceTb.Text + ", '"+CatCb.SelectedValue.ToString()+"' )";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully");
                Con.Close();
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ProdEditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodIdTb.Text == "" || prodNameTb.Text == "" || prodPriceTb.Text == "" || prodQtyTb.Text == "" || CatCb.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update ProductTbl set ProdName = '" + prodNameTb.Text + "', ProdQty = " + prodQtyTb.Text + ", ProdPrice = " + prodPriceTb.Text + ", ProdCat =  '"+ CatCb.SelectedValue.ToString() +"' where ProdId = " + prodIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product successfully updated");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProdDelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodIdTb.Text == "")
                {
                    MessageBox.Show("Select the category");
                }
                else
                {
                    Con.Open();
                    string query = "delete from ProductTbl where ProdId = " + prodIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted successfully");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProdDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            prodIdTb.Text = ProdDGV.SelectedRows[0].Cells[0].Value.ToString();
            prodNameTb.Text = ProdDGV.SelectedRows[0].Cells[1].Value.ToString();
            prodQtyTb.Text = ProdDGV.SelectedRows[0].Cells[2].Value.ToString();
            prodPriceTb.Text = ProdDGV.SelectedRows[0].Cells[3].Value.ToString();
            CatCb.SelectedValue = ProdDGV.SelectedRows[0].Cells[4].Value.ToString();
        }
    }
}

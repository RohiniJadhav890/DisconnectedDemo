using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Configuration;

namespace DisconnectedDemo
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder builder;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultname"].ConnectionString);

        }
        private DataSet GetEmployees()
        {
            string qry = "select * from Employee";
            // assign the query
            da = new SqlDataAdapter(qry, con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            builder = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "empdataset");// this name given to the DataSet table
            return ds;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                // create new row to add recrod
                DataRow row = ds.Tables["empdataset"].NewRow();
               

                // assign value to the row
                row["eid"] = textBox1.Text;
                row["ename"] = textBox2.Text;
                row["esal"] = textBox3.Text;
                
                // attach this row in DataSet table
                ds.Tables["empdataset"].Rows.Add(row);
                // update the changes from DataSet to DB
                int result = da.Update(ds.Tables["empdataset"]);
                if (result >= 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                // find the row
                DataRow row = ds.Tables["empdataset"].Rows.Find(textBox1.Text);
                if (row != null)
                {
                    row["eid"] = textBox1.Text;
                    row["ename"] = textBox2.Text;
                    row["esal"] = textBox3.Text;
                    // update the changes from DataSet to DB
                    int result = da.Update(ds.Tables["empdataset"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record updated");
                    }
                }
                else
                {
                    MessageBox.Show("Id not matched");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                
                ds = GetEmployees();
                // find the row
                DataRow row = ds.Tables["empdataset"].Rows.Find(textBox1.Text);
                if (row != null)
                {
                    // delete the current row from DataSet table
                    row.Delete();
                    // update the changes from DataSet to DB
                    int result = da.Update(ds.Tables["empdataset"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record deleted");
                    }
                }
                else
                {
                    MessageBox.Show("Id not matched");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ds= GetEmployees();
            dataGridView1.DataSource = ds.Tables["empdataset"];
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}

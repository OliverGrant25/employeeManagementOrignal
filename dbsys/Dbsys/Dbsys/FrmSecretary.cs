using Dbsys.AppData;
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

namespace Dbsys
{
    public partial class FrmSecretary : Form
    {
        public FrmSecretary()
        {
            InitializeComponent();
        }
       

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            login.Show();
            this.Close();
        }

 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmSecretary_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ViewEmpBtn_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = employeemanage; Integrated Security = True"))
                {
                    con.Open();

                    string query = "Select userInformation.userId, userInformation.userInFname, userInformation.userInLname, userInformation.userInEmail, userInformation.userInAddress, salary.salary from userInformation inner join salary ON userInformation.userId = salary.userId";
                    SqlCommand cmd = new SqlCommand(query, con);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    secGrid.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                decimal salary = Convert.ToDecimal(txtUpdate.Text);

                if (secGrid.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = secGrid.SelectedRows[0].Index;

                    int userId = Convert.ToInt32(secGrid.Rows[selectedRowIndex].Cells["userId"].Value);

                    //update salary
                    using (SqlConnection con = new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = employeemanage; Integrated Security = True"))
                    {
                        con.Open();

                        string updateQuery = "UPDATE salary SET salary = @NewSalary WHERE userId = @userId";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, con);
                        updateCommand.Parameters.AddWithValue("@NewSalary", salary);
                        updateCommand.Parameters.AddWithValue("@userId", userId);
                        updateCommand.ExecuteNonQuery();
                    }

                    LoadData();
                }
                else
                {
                    MessageBox.Show("Please select a row in the DataGridView.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDeduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (secGrid.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = secGrid.SelectedRows[0].Index;

                    int userId = Convert.ToInt32(secGrid.Rows[selectedRowIndex].Cells["userId"].Value);

                    decimal currentSalary = Convert.ToDecimal(secGrid.Rows[selectedRowIndex].Cells["salary"].Value);
  
                    decimal deductionAmount = Convert.ToDecimal(txtDeduct.Text);

                    if (deductionAmount <= currentSalary)
                    {

                        using (SqlConnection con = new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = employeemanage; Integrated Security = True"))
                        {
                            con.Open();

                            string updateQuery = "UPDATE salary SET salary = salary - @DeductionAmount WHERE userId = @userId";
                            SqlCommand updateCommand = new SqlCommand(updateQuery, con);
                            updateCommand.Parameters.AddWithValue("@DeductionAmount", deductionAmount);
                            updateCommand.Parameters.AddWithValue("@userId", userId);
                            updateCommand.ExecuteNonQuery();
                        }

                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Deduction amount cannot be greater than the current salary.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the DataGridView.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

using Dbsys.AppData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Dbsys
{
    public partial class FrmCEO : Form
    {
        private DataGridView dgvCeo;

        public FrmCEO()
        {
            InitializeComponent();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            login.Show();
            this.Close();
        }

        private void FrmCEO_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Get values from TextBoxes
                string firstName = txtFirstname.Text;
                string lastName = txtLastname.Text;
                string email = txtEmail.Text;
                string address = txtAddress.Text;
                decimal salary = Convert.ToDecimal(txtSalary.Text);

                // Insert the new employee record into the userInformation table
                int userId = InsertUserInformation(firstName, lastName, email, address);

                if (userId > 0)
                {
                    // Insert the salary record into the salary table
                    InsertSalary(userId, salary);

                    // Reload data to reflect changes in the DataGridView
                    LoadData();

                    // Clear TextBoxes after successful insertion
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("Failed to insert user information.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private int InsertUserInformation(string firstName, string lastName, string email, string address)
        {
            int userId;

            using (SqlConnection connection = new SqlConnection(@"Data Source=.\sqlexpress; Initial Catalog=employeemanage; Integrated Security=True"))
            {
                connection.Open();

                // Generate a unique identifier for the userId
                Guid userIdGuid = Guid.NewGuid();
                userId = userIdGuid.GetHashCode();

                // Insert the new employee record into the userInformation table
                string insertUserQuery = "INSERT INTO userInformation (userId, userInFname, userInLname, userInEmail, userInAddress) " +
                                         "VALUES (@userId, @userInFname, @userInLname, @userInEmail, @userInAddress)";
                SqlCommand insertUserCommand = new SqlCommand(insertUserQuery, connection);
                insertUserCommand.Parameters.AddWithValue("@userId", userId);
                insertUserCommand.Parameters.AddWithValue("@userInFname", firstName);
                insertUserCommand.Parameters.AddWithValue("@userInLname", lastName);
                insertUserCommand.Parameters.AddWithValue("@userInEmail", email);
                insertUserCommand.Parameters.AddWithValue("@userInAddress", address);

                insertUserCommand.ExecuteNonQuery();
            }

            return userId;
        }

        private void InsertSalary(int userId, decimal salary)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=.\sqlexpress; Initial Catalog=employeemanage; Integrated Security=True"))
            {
                connection.Open();

                // Insert the salary record into the salary table
                string insertSalaryQuery = "INSERT INTO salary (userId, salary) " +
                                           "VALUES (@userId, @salary)";
                SqlCommand insertSalaryCommand = new SqlCommand(insertSalaryQuery, connection);
                insertSalaryCommand.Parameters.AddWithValue("@userId", userId);
                insertSalaryCommand.Parameters.AddWithValue("@salary", salary);
                insertSalaryCommand.ExecuteNonQuery();
            }
        }

        private void LoadData()
        {
            dgvCeo.DataSource = LoadEmployeeData();
        }

        private DataTable LoadEmployeeData()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(@"Data Source=.\sqlexpress; Initial Catalog=employeemanage; Integrated Security=True"))
            {
                connection.Open();

                string query = "SELECT userInFname, userInLname, userInEmail, userInAddress, salary " +
                               "FROM userInformation " +
                               "INNER JOIN salary ON userInformation.userId = salary.userId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        private void ClearTextBoxes()
        {
            txtFirstname.Text = string.Empty;
            txtLastname.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtSalary.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell content click event if required
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Handle update button click event if required
        }
    }
}


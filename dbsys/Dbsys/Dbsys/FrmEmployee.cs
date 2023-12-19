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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Dbsys
{
    public partial class FrmEmployee : Form
    {
        UserRepository userRepo;
        public FrmEmployee()
        {
            InitializeComponent();
            userRepo = new UserRepository();
            
        }
       public static string connectionString = " Data Source=.\\sqlexpress;Initial Catalog=employeemanage;Integrated Security=True";

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            login.Show();
            this.Close();
        }

        /*
        public void load()
           
        {
          

        }
        */

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'employeemanageDataSet.salary' table. You can move, or remove it, as needed.
           // this.salaryTableAdapter.Fill(this.employeemanageDataSet.salary);
            //if (FrmLogin.loginuser != null)
            //{
            //    lblUsername.Text = FrmLogin.loginuser;
            //}

            //load();
        }

        private void salaryBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = employeemanage; Integrated Security = True");
            String sql = "select * from salary where userId='" + txtSalary.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    lblSalary.Text = dr["salary"].ToString();
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void profileBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con =  new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = employeemanage; Integrated Security = True");
            String sql = "select * from userInformation where userId='" + txtuserId.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    lblFname.Text = dr["userInFname"].ToString();                  
                    lblLname.Text = dr["userInLname"].ToString();
                    lblEmail.Text = dr["userInEmail"].ToString();
                    lblAddress.Text = dr["userInAddress"].ToString();
                }
            }
        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }
    }
}

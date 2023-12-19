using Dbsys.AppData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dbsys
{
    public partial class FrmRegister : Form
    {

        private UserRepository userRepo;
        employeemanageEntities db;


        public FrmRegister()
        {
            InitializeComponent();
            
            userRepo = new UserRepository();
            db = new employeemanageEntities();  

        }

        private void FrmRegister_Load(object sender, EventArgs e)
        {
            loadcmboRoles();

        }

        public void loadcmboRoles() 
        {
            //selecting roles
            var roles = db.roles.ToList();

            cmboRoles.DataSource = roles;
            cmboRoles.ValueMember = "roleId";
            cmboRoles.DisplayMember = "roleType";
           
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtFname.Text))
            {
                errorProvider.Clear();
                errorProvider.SetError(txtFname, "Empty Field!");
                return;
            }

            if (String.IsNullOrEmpty(txtLname.Text))
            {
                errorProvider.Clear();
                errorProvider.SetError(txtLname, "Empty Field!");
                return;
            }

            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                errorProvider.Clear();
                errorProvider.SetError(txtEmail, "Empty Field!");
                return;
            }


            if (String.IsNullOrEmpty(txtAddress.Text))
            {
                errorProvider.Clear();
                errorProvider.SetError(txtAddress, "Empty Field!");
                return;
            }


            if (String.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider.Clear();
                errorProvider.SetError(txtUsername, "Empty Field!");
                return;
            }


            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider.Clear();
                errorProvider.SetError(txtPassword, "Empty Field!");
                return;
            }

            errorProvider.Clear();


            //function userRepo
            ErrorCode res = userRepo.FrmRegister(txtFname.Text, txtLname.Text, txtEmail.Text, txtAddress.Text, txtUsername.Text, txtPassword.Text, (Int32)cmboRoles.SelectedValue);

            if (res == ErrorCode.Success)
            {
                //Clear textboxes
                txtFname.Clear();
                txtLname.Clear();
                txtEmail.Clear();
                txtAddress.Clear();
                txtUsername.Clear();
                txtPassword.Clear();
              
                //message prompts
                MessageBox.Show("Successfully Registered!");

                FrmLogin login = new FrmLogin();
                login.Show();
                this.Close();

                FrmRegister register = new FrmRegister();
                register.Close();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Error Registration!");
            }


        }


        private void Clearbtn_Click(object sender, EventArgs e)
        {
            txtFname.Text = " ";
            txtLname.Text = " ";
            txtEmail.Text = " ";    
            txtAddress.Text = " ";
            txtUsername.Text = " ";
            txtPassword.Text = " ";
        }

        private void chkShow_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkShow.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}

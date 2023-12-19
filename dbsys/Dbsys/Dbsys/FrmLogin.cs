using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Dbsys
{
    public partial class FrmLogin : Form
    {
        UserRepository userRepo;

        public FrmLogin()
        {
            InitializeComponent();
            userRepo = new UserRepository() ;
        }

        public static string loginuser = "";

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signupbtn_Click(object sender, EventArgs e)
        {
            FrmRegister reg = new FrmRegister();
            reg.Show();
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            txtUsername.Text = " ";
            txtPassword.Text = " ";
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtUsername.Text))
            {
                errorProviderCustom.SetError(txtUsername, "Empty Field!");
                return;
            }
            else
            {
                errorProviderCustom.SetError(txtUsername, null);
            }

            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                errorProviderCustom.SetError(txtPassword, "Empty Field!");
                return;
            }
            else
            {
                errorProviderCustom.SetError(txtPassword, null);
            }

            var userLogged = userRepo.GetUserbyUsername(txtUsername.Text);

            if (userLogged != null)
            {

                if (userLogged.password.Equals(txtPassword.Text))
                {
                    //singleton
                    UserLogged.GetInstance().UserAccount = userLogged;

                    switch ((roles)userLogged.roleId)
                    {
                        case roles.Employee:
                            //employee dashboard
                            loginuser = txtUsername.Text;
                            loginuser = txtPassword.Text;
                            new FrmEmployee().Show();
                            this.Hide();
                            break;

                        case roles.Secretary:
                            new FrmSecretary().Show();
                            this.Hide();
                            break;

                        case roles.CEO:
                            new FrmCEO().Show();
                            this.Hide();
                            break;

                        default:
                            MessageBox.Show("User has no role!");
                            break;
                    }
                }// if userlogged

                else
                {
                    MessageBox.Show("Incorrect Password");
                }
            }

                else
                {
                    MessageBox.Show("Username not found");
                }
        }//login button

        private void chkShow_CheckedChanged(object sender, EventArgs e)
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

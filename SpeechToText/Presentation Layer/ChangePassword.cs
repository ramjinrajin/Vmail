using SpeechToText.ApplicationLayer;
using SpeechToText.Business.Models;
using SpeechToText.Presentation_Layer.SessionData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechToText.Presentation_Layer
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Form> forms = new List<Form>();

            // All opened myForm instances
            foreach (Form f in Application.OpenForms)
               // if (f.Name == "ForgotPassword")
                    forms.Add(f);

            // Now let's close opened myForm instances
            foreach (Form f in forms)
                f.Hide();

            this.BringToFront();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string GetUserName = LoginCredentials.LoggedEmailId.Substring(0, LoginCredentials.LoggedEmailId.LastIndexOf('@'));
            CreateNewUser _createUser = new CreateNewUser();
            User _user = new User();
            _user.Name = GetUserName;
            _user.Password = txtPassword.Text;
            _user.ConPassword = txtCnfPassword.Text;

            if (IsEqual.CompareText(_user.Password, _user.ConPassword))
            {
                if (txtCnfPassword.Text != "" || txtPassword.Text != "")
                {
                    if (_createUser.ResetPassword(_user))
                    {
                        MessageBox.Show("Password reset successful !!!", "Alert!!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                        UserLogin _userLogin = new UserLogin(true);
                        _userLogin.Show();
                    }
                    else
                    {
                        MessageBox.Show("Unable to change password !!!", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        UserLogin _userLogin = new UserLogin(true);
                        _userLogin.Show();
                    }
                }
                else
                    MessageBox.Show("Mandatory fields cannot be empty !!!", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Password does not match", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCnfPassword.Text = "";
                txtPassword.Text = "";               
                txtPassword.Focus();
            }
      


        }
    }
}

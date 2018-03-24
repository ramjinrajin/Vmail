using SpeechToText.Presentation_Layer;
using SpeechToText.Presentation_Layer.Preconditions.UserSettings;
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

namespace SpeechToText
{
    public partial class Options : Form
    {
        bool _isadmin;
        public Options(bool Isadmin)
        {
            InitializeComponent();
            _isadmin=Isadmin;
        }

       

        private void btnDelete_Click(object sender, EventArgs e)
        {


            DeleteUser frmDeleteUser = new DeleteUser(_isadmin);
            frmDeleteUser.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeleteUser frmDeleteUser = new DeleteUser(_isadmin);
            frmDeleteUser.Show();

        }

        private void Options_Load(object sender, EventArgs e)
        {
           if(_isadmin)
           {
               btnDelete.Text = "Delete";
           }
            else
           {
               btnDelete.Text = "View";
           }
        }

        private void ChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword frmChangePass = new ChangePassword();
            frmChangePass.ShowDialog();
        }
        

    
  
    }
}

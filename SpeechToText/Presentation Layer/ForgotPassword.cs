using SpeechToText.ApplicationLayer;
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
    public partial class ForgotPassword : Form
    {
        string eMail_Id;
        static string SecAnswer;
        public ForgotPassword(string Email)
        {
            eMail_Id = Email;
            InitializeComponent();
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {
             
           List<string> SecParameters= MailServer.GetSecurityQues(eMail_Id);
           if(SecParameters[0].ToString()!="")
           {
               lblSecQuestion.Text = SecParameters[0].ToString();
           }
           SecAnswer =SecParameters[1].ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
           

            if(textBox1.Text==SecAnswer)
            {
                ChangePassword frmChangePass = new ChangePassword();
                frmChangePass.Show();
            }
            else
            {
                MessageBox.Show("Incorrect security answer ,please contact admin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
       
        }
    }
}

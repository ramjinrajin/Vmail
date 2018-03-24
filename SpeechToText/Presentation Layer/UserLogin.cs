using SpeechToText.ApplicationLayer;
using SpeechToText.Business.Methods;
using SpeechToText.Business.Models;
using SpeechToText.Presentation_Layer.Preconditions;
using SpeechToText.Presentation_Layer.Preconditions.UserSettings;
using SpeechToText.Presentation_Layer.SessionData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechToText.Presentation_Layer
{
    public partial class UserLogin : Form
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();
        public bool _islogout = false;
        public UserLogin(bool Islogout)
        {
            InitializeComponent();

            panel1.Hide();
            panel2.Hide();
            _islogout = Islogout;
            loadSpeechModule();
            this.KeyPreview = true;
          //  SpeechToTextalgorithm();
        }
        private void SpeechToTextalgorithm()
        {
            // richTextBox1.Focus();

            PromptBuilder pbuilder = new PromptBuilder();
            SpeechRecognitionEngine sregEngine = new SpeechRecognitionEngine();


            Choices slist = new Choices();
            slist.Add(new string[] {

                "next",
                "start",
                "submit",
                "admin"
            } ); 
            Grammar gr = new Grammar(new GrammarBuilder(slist));

            try
            {
                sregEngine.RequestRecognizerUpdate();
                sregEngine.LoadGrammar(gr);
                sregEngine.SpeechRecognized += sregEngine_SpeechRecognized;
                sregEngine.SetInputToDefaultAudioDevice();
                sregEngine.RecognizeAsync(RecognizeMode.Single);
                sregEngine.Recognize();
            }

            catch
            {
                return;
            }
        }

        private void sregEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "next")
            {
                FocusControl();
            }
            else if(e.Result.Text=="start")
            {
                START_FUNCTION();
            }
            else if(e.Result.Text=="submit")
            {
                
                Login();
            }

            
        }

        private void FocusControl()
        {
            if(txtUSerEmail.Text=="")
            {
                synth.Speak("Please provide the email id");
                txtUSerEmail.Focus();
                
            }
            else if(txtPassword.Text=="")
            {
                synth.Speak("Please provide password");
                txtPassword.Focus();


            }
            else
            {
                synth.Speak("say submit to login");
                button2.Focus();
            }
                
        }

        private void loadSpeechModule()
        {
            MuteIcon.Visible = false;
            Unmute.Visible = true;
            string DirPath = @"C:\\Ycet\\";
            string FilePath = @"C:\\Ycet\\YcetSpeechModuleEnabledKey.ini";


            if (Directory.Exists(DirPath))
            {

                File.Delete(FilePath);
                using (StreamWriter sw = File.AppendText(@"C:\\Ycet\\YcetSpeechModuleEnabledKey.ini"))
                {

                    sw.WriteLine("Enabled");
                    sw.Close();

                }
            }
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {
            ConfigureSqlConnectionString();
            if (_islogout)
            {
                panel2.Show();
                panel1.Dispose();
            }
            else
            {
                panel1.Show();
            }

        }

        private void ConfigureSqlConnectionString()
        {
            string Path = @"C:\\Ycet\\";
            if (!Directory.Exists(Path))
            {
                button1.Text = "Configure SQL";

            }

          
             

        }

        internal bool GetRememberStatus()
        {
            string Path = @"C:\\Ycet\\YcetRememberMeKey.ini";
            if (File.Exists(Path))
            {
                chkRem.Checked = true;
                return true;
            }
            else
            {
                chkRem.Checked = false;
                return false;
            }
      

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text=="START")
            {
                START_FUNCTION();
                greetingMsg();
            }
            else
            {
                SQLDBConnection SQlForm = new SQLDBConnection();
                SQlForm.ShowDialog();

            }
            
        }

        private void greetingMsg()
        {
            if(txtUSerEmail.Text!="")
            {
                synth.Speak("Say register if you are a new user else enter your email Id");
            }
        }

        private void START_FUNCTION()
        {
            if (GetRememberStatus())
            {
                string Credentials = System.IO.File.ReadAllText(@"C:\\Ycet\\YcetRememberMeKey.ini");
                string[] Keys = Credentials.Split('#');
                foreach (string Key in Keys)
                {

                    if (txtUSerEmail.Text == "")
                    {
                        txtUSerEmail.Text = Key.ToLower().Trim();
                    }
                    else
                    {
                        txtPassword.Text = "*****";
                        maskedPassword = Key.ToLower().Trim();
                    }

                    panel2.Show();
                    panel1.Dispose();

                }


            }
            else
            {
                panel2.Show();
                panel1.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login();

        }

        private void Login()
        {
            //if (txtPassword.Text != "")
            //{
            //    if (maskedPassword!="")
            //    {
            //        maskedPassword = txtPassword.Text.ToLower();
            //    }
               
            //    txtPassword.Text = "*****";


            //}
            if (txtUSerEmail.Text != "" && txtPassword.Text != "")
            {
                if (maskedPassword==null)
                {
                    MessageBox.Show("Password readed as empty");
                    return;
                }

                if (UserPrivilege.IsValidUser(txtUSerEmail.Text.ToLower().Trim(), maskedPassword.Trim()))
                {
                    LoginCredentials.LoggedEmailId = txtUSerEmail.Text.ToLower();
                    List<Form> forms = new List<Form>();
                    // All opened myForm instances
                    foreach (Form f in Application.OpenForms)
                    {

                        forms.Add(f);
                    }
                    // Now let's close opened myForm instances
                    foreach (Form f in forms)
                    {
                        f.Hide();
                    }
                  
                    RememberMe();

                    Mailbox _mailbox = new Mailbox();
                    _mailbox.Show();
                }
                else
                {
                    MessageBox.Show("Invalid credentials or invalid user ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }




            }
            else
            {
                MessageBox.Show("Mandatory  fields cannot be empty ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RememberMe()
        {
            string DirPath = @"C:\\Ycet\\";
            string FilePath = @"C:\\Ycet\\YcetRememberMeKey.ini";
 
             if(chkRem.Checked==true)
             {
                
                 if (Directory.Exists(DirPath))
                 {

                     File.Delete(FilePath);
                     using (StreamWriter sw = File.AppendText(@"C:\\Ycet\\YcetRememberMeKey.ini"))
                     {
                        
                       sw.WriteLine(string.Format("{0}#{1}", txtUSerEmail.Text.ToLower().Trim(),maskedPassword.Trim()));
                       sw.Close();
                        button2.Focus();

                     }
                 }
             }
            else
             {
                 
                 File.Delete(FilePath);
             }
           
        }

        private void UserLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                Login();
            }
            if(e.KeyCode==Keys.Space)
            {
                SpeechToTextalgorithm();
            }
                
        }

        private void btnCreateNewUser_Click(object sender, EventArgs e)
        {
            

        
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            List<Form> forms = new List<Form>();

            // All opened myForm instances
            foreach (Form f in Application.OpenForms)
                if (f.Name == "Options")
                    forms.Add(f);

            // Now let's close opened myForm instances
            foreach (Form f in forms)
                f.Close();

            CreateUser frmCreateUser = new CreateUser();
            frmCreateUser.ShowDialog();
        }

        private void chkRem_CheckedChanged(object sender, EventArgs e)
        {
          
            
           
        }

        private void helpText_CheckedChanged(object sender, EventArgs e)
        {
            EnableSpeechHelpModule();
        }

        private void EnableSpeechHelpModule()
        {
            string DirPath = @"C:\\Ycet\\";
            string FilePath = @"C:\\Ycet\\YcetSpeechModuleEnabledKey.ini";

          
                if (Directory.Exists(DirPath))
                {

                    File.Delete(FilePath);
                    using (StreamWriter sw = File.AppendText(@"C:\\Ycet\\YcetSpeechModuleEnabledKey.ini"))
                    {

                        sw.WriteLine("Enabled");
                        sw.Close();

                    }
                }
          
            else
            {

                File.Delete(FilePath);
            }

        }

        private void MuteIcon_Click(object sender, EventArgs e)
        {
            Unmute.Visible = true;
            MuteIcon.Visible = false;
            string DirPath = @"C:\\Ycet\\";
            string FilePath = @"C:\\Ycet\\YcetSpeechModuleEnabledKey.ini";

             
                if (Directory.Exists(DirPath))
                {

                    File.Delete(FilePath);
                    using (StreamWriter sw = File.AppendText(@"C:\\Ycet\\YcetSpeechModuleEnabledKey.ini"))
                    {

                        sw.WriteLine("Enabled");
                        sw.Close();

                    }
                }
             
            
        }

        private void Unmute_Click(object sender, EventArgs e)
        {
            MuteIcon.Visible = true;
            Unmute.Visible = false;
            string DirPath = @"C:\\Ycet\\";
            string FilePath = @"C:\\Ycet\\YcetSpeechModuleEnabledKey.ini";

        
                if (Directory.Exists(DirPath))
                {

                    File.Delete(FilePath);
                    using (StreamWriter sw = File.AppendText(@"C:\\Ycet\\YcetSpeechModuleEnabledKey.ini"))
                    {

                        sw.WriteLine("Disabled");
                        sw.Close();

                    }
                }
            
        }

        private void lblResetPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtUSerEmail.Text.Trim()))
            {
                MessageBox.Show("Please type your email Id ,if you have dont remember emailId too please contact admin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                CreateNewUser _createUser = new CreateNewUser();
                User _user = new User
                {
                    EmailID = txtUSerEmail.Text.ToLower()
                };

                if(!_createUser.IfExists(_user))
                {
                    LoginCredentials.LoggedEmailId = txtUSerEmail.Text.ToLower().Trim();
                    ForgotPassword frmForgotPass = new ForgotPassword(txtUSerEmail.Text.ToLower());
                    frmForgotPass.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Email Id you have entered is not registered with our application", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               
            }
        }

        private void txtUSerEmail_Leave(object sender, EventArgs e)
        {
            if (SpeechModule.GetStatus())
            {

                if (txtUSerEmail.Text != ""&&chkRem.Checked!=true)
                {
                    txtUSerEmail.Text = txtUSerEmail.Text.ToLower();
                    synth.SetOutputToDefaultAudioDevice();
                    synth.Speak("you told" + txtUSerEmail.Text+ "as the name");
                   // synth.Speak("Please enter the password");


                }
                
                //else if(chkRem.Checked!=true)
                //{
                //    synth.Speak("Email id field cannot be empty please say the Email id");
                //    txtUSerEmail.Focus();
                //}
            }
        }
        private string maskedPassword { get; set; }

    
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (SpeechModule.GetStatus())
            {

                if (txtPassword.Text != "")
                {

                    synth.SetOutputToDefaultAudioDevice();
                    synth.Speak("you told" + txtPassword.Text + "as the password");
                    //synth.Speak("Please say submit to login to the mail");
                    if (txtPassword.Text != "*****")
                    {
                        maskedPassword = txtPassword.Text;
                    }
                   
                    txtPassword.Text = "*****";


                }
                else
                {
                    synth.Speak("Password field cannot be empty please say the password");
                    txtUSerEmail.Focus();
                }
            }
        }


    }
}

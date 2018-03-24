using SpeechToText.ApplicationLayer;
using SpeechToText.Business.Models;
using SpeechToText.Presentation_Layer;
using SpeechToText.Presentation_Layer.Preconditions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechToText
{
    public partial class CreateUser : Form
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();
        static string Password { get; set; }
        static string ConfrmPassWord { get; set; }
        public CreateUser()
        {
            InitializeComponent();
            synth.SetOutputToDefaultAudioDevice();

        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            ConfirmToCreate();

        }

        private void ConfirmToCreate()
        {
            User _user = new User();
            _user.Name = txtname.Text;
            _user.Password = Password;
            _user.Gender = cmbGender.Text;
            _user.DOB = DateTime.Now.ToString("yyyy-MM-dd");
            _user.ConPassword = ConfrmPassWord;
            _user.EmailID = txtEmail.Text;
            _user.SecurityAns = txtSecurityAns.Text;
            _user.SecutityQues = CmbSecQues.Text;


            CreateNewUser _createUser = new CreateNewUser();


            string IsEmptyUSer = IsEmpty.CheckIfEmpty_User(_user);

            if (IsEmptyUSer != null)
            {
                MessageBox.Show(IsEmptyUSer, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                if (IsEqual.CompareText(_user.Password, _user.ConPassword))
                {
                    if (IsValidEmail(txtEmail.Text))
                    {



                        if (txtSecurityAns.Text != "")
                        {

                            if (_createUser.IfExists(_user))
                            {
                                if (_createUser.CreateUSer(_user))
                                {
                                    MessageBox.Show("New user created sucessfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    UserLogin ul = new UserLogin(true);
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
                                    ul.ShowDialog();



                                }
                                else
                                {
                                    MessageBox.Show("Unable to create new user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                            }
                            else
                            {
                                MessageBox.Show("EmailID already exists!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please choose a secuity answer cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {

                        MessageBox.Show("Email Id seems to be invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                }
                else
                {
                    if (SpeechModule.GetStatus())
                    {

                        if (cmbGender.Text != "")
                        {

                            synth.SetOutputToDefaultAudioDevice();
                            synth.Speak("Password does not match");
                            synth.Speak("Please say the password again");
                            txtPassword.Text = "";
                            txtConfirmPassword.Text = "";
                            txtPassword.Focus();

                        }
                    }
                    else


                        MessageBox.Show("Password does not match!!!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                }



            }
        }


        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void CreateUser_Load(object sender, EventArgs e)
        {
            if (SpeechModule.GetStatus())
            {
                // WelcomeMessage();
            }


        }


        string CmdReader = "Nil";
        private string FilterCommand(string ReadText)
        {
            string RecognizedText = ReadText;

            List<string> ListCommands = new List<string>
        {
                "go to Name",
                "go to Gender", 
                "go to Security answer",
                "go to Password", 
                "go to confirm password",
                "go to create", 
                "go to Email",
                "choose male",
                "choose Female",
                "Read name",
                "Read gender",
                "Read Email",
                "Next",
                "Create",
                "Read Security answer",
                "Read Password",
                "Read confirm Password",
                "Select one",
                "Select two",
                "Select three"
        };

            foreach (string cmd in ListCommands)
            {
                if (RecognizedText.ToLower().Contains(cmd.ToLower()))
                {
                    CmdReader = cmd;
                    RecognizedText = RecognizedText.ToLower().Replace(cmd.ToLower(), "");
                    return RecognizedText;

                }



            }

            return RecognizedText;

        }
        private void executeAction(string Command)
        {
            if (Command != "Nil")
            {
                Command = Command.ToLower();
                if (Command == "Select One".ToLower())
                {
                    CmbSecQues.SelectedIndex = 0;
                }
                if (Command == "Select two".ToLower())
                {
                    CmbSecQues.SelectedIndex = 1;
                }
                if (Command == "Select three".ToLower())
                {
                    CmbSecQues.SelectedIndex = 2;
                }
                if (Command == "go to name".ToLower())
                {
                    txtname.Focus();
                }
                else if (Command == "go to gender".ToLower())
                {
                    cmbGender.Focus();

                }
                else if (Command == "go to email".ToLower())
                {

                    txtEmail.Focus();
                }
                else if (Command == "go to security answer".ToLower())
                {

                    txtSecurityAns.Focus();
                }
                else if (Command == "go to password".ToLower())
                {

                    txtPassword.Focus();
                }
                else if (Command == "go to confirm password".ToLower())
                {

                    txtConfirmPassword.Focus();
                }
                else if (Command == "choose male".ToLower())
                {

                    cmbGender.Text = "Male";
                }
                else if (Command == "choose Female".ToLower())
                {

                    cmbGender.Text = "Female";
                }
                else if (Command == "Read name".ToLower())
                {
                    txtname_Leave();
                }
                else if (Command == "Next".ToLower())
                {
                    FocusControls();
                }
                else if (Command == "Create".ToLower())
                {
                    ConfirmToCreate();
                }
                else if (Command == "Read gender".ToLower())
                {
                    cmbGender_SelectedIndexChanged();
                }
                else if (Command == "Read Security answer".ToLower())
                {
                    CmbSecQues_Leave();
                }
                else if (Command == "Read Password".ToLower())
                {
                    synth.Speak("For security reason password cannot be read");
                }
                else if (Command == "Read confirm Password".ToLower())
                {
                    synth.Speak("For security reason password cannot be read");
                }
                else if (Command == "Read Email".ToLower())
                {
                    if (txtEmail.Text != "")
                    {
                        synth.Speak("you said " + txtEmail.Text + "as the email ID");
                    }
                    else
                    {
                        synth.Speak("Email Id is empty");
                    }

                }

            }



        }


        private void VoiceRecognizer()
        {
            SpeechSynthesizer asynthesis = new SpeechSynthesizer();
            PromptBuilder pbuilder = new PromptBuilder();
            SpeechRecognitionEngine sregEngine = new SpeechRecognitionEngine();


            Choices slist = new Choices();
            slist.Add(new string[] 
            {   "go to Name",
                "go to Gender", 
                "go to Security Question",
                "go to Password", 
                "go to confirm password",
                "go to create", 
                "go to Email",
                "select male",
                "select Female",
                "Read name",
                "Read gender",
                "Read Email",
                "Next",
                "Create",
                "Read Security answer",
                "Read Password",
                "Read confirm Password",
                "Select one",
                "Select two",
                "Select three"});
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
            throw new NotImplementedException();
        }

        //private void sregEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    if (Command == "Select One")
        //    {
        //        CmbSecQues.SelectedIndex = 0;
        //    }
        //    if (Command == "Select two")
        //    {
        //        CmbSecQues.SelectedIndex = 1;
        //    }
        //    if (Command == "Select three")
        //    {
        //        CmbSecQues.SelectedIndex = 2;
        //    }
        //    if (Command == "go to Name")
        //    {
        //        txtname.Focus();
        //    }
        //    else if (Command == "go to Gender")
        //    {
        //        cmbGender.Focus();

        //    }
        //    else if (Command == "go to Email")
        //    {

        //        txtEmail.Focus();
        //    }
        //    else if (Command == "go to Password")
        //    {

        //        txtPassword.Focus();
        //    }
        //    else if (Command == "go to confirm password")
        //    {

        //        txtConfirmPassword.Focus();
        //    }
        //    else if (Command == "select male")
        //    {

        //        cmbGender.Text = "Male";
        //    }
        //    else if (Command == "select Female")
        //    {

        //        cmbGender.Text = "Female";
        //    }
        //    else if (Command == "Read name")
        //    {
        //        txtname_Leave();
        //    }
        //    else if (Command == "Next")
        //    {
        //        FocusControls();
        //    }
        //    else if (Command == "Create")
        //    {
        //        ConfirmToCreate();
        //    }
        //    else if (Command == "Read gender")
        //    {
        //        cmbGender_SelectedIndexChanged();
        //    }
        //    else if (Command == "Read Security answer")
        //    {
        //        CmbSecQues_Leave();
        //    }
        //    else if (Command == "Read Password")
        //    {
        //        synth.Speak("For security reason password cannot be read");
        //    }
        //    else if (Command == "Read confirm Password")
        //    {
        //        synth.Speak("For security reason password cannot be read");
        //    }
        //    else if (Command == "Read Email")
        //    {
        //        if (txtEmail.Text != "")
        //        {
        //            synth.Speak("you said " + txtEmail.Text + "as the email ID");
        //        }
        //        else
        //        {
        //            synth.Speak("Email Id is empty");
        //        }

        //    }



        //}

        bool Narration = true;
        private void FocusControls()
        {

            if (txtname.Text == "")
            {
                synth.Speak("Please say name for the user");
                txtname.Focus();
            }

            else if (cmbGender.SelectedText == "" && cmbGender.Text == "" && cmbGender.SelectedIndex == -1)
            {


                synth.Speak("Please say choose male or choose female to select the gender");
                cmbGender.Focus();



            }
            else if (txtEmail.Text == "")
            {
                synth.Speak("Please say a email id for the user");
                txtEmail.Focus();


            }

            else if (txtSecurityAns.Text == "" && Narration)
            {
                synth.Speak("Please choose a security question and answer");
                int k = 0;
                foreach (var Msg in CmbSecQues.Items)
                {
                    k++;
                    synth.Speak("Question " + k.ToString());
                    synth.Speak(Msg.ToString());

                }
                txtSecurityAns.Focus();
                //Narration = false;
            }
            else if (CmbSecQues.SelectedIndex == -1)
            {
                txtSecurityAns.Text = "";
                synth.Speak("Please choose a security question before saying the answer");
                txtSecurityAns.Focus();
            }

            else if (txtPassword.Text == ""&&txtPassword.Text!="*****")
            {
                synth.Speak("Please say a password  for the user");
                txtPassword.Focus();

            }
            else if (txtConfirmPassword.Text == ""&&txtConfirmPassword.Text != "*****")
            {
                synth.Speak("Please confirm the password");
                txtConfirmPassword.Focus();

            }

            else
            {
                synth.Speak("Please say create or enter to create the acount");
                btnCreate.Focus();

            }

        }


        public async Task WelcomeMessage()
        {
            await Task.Delay(500);
            if (SpeechModule.GetStatus()) //Only for compose mail help text speech work
            {
                GreetingMsg();
            }
        }

        private void GreetingMsg()
        {
            synth.Speak("You are in create new user window");
            synth.Speak("Say next to move next control say Read email to speak it back");
            synth.Speak("Please say the name of the user");


        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSecQues.Text != "")
            {
                txtSecurityAns.Enabled = true;
            }
        }

        private void txtname_Leave()
        {
            if (SpeechModule.GetStatus())
            {

                if (txtname.Text != "")
                {

                    synth.SetOutputToDefaultAudioDevice();
                    synth.Speak("you told" + txtname.Text + "as the name");
                    //if(cmbGender.SelectedIndex!=0)
                    //{
                    //    synth.Speak("Please say select male or select female to select the gender");
                    //    cmbGender.Focus();
                    //}



                }
                else
                {
                    synth.Speak("Name field cannot be empty please say the Name");
                    txtname.Focus();
                }
            }


        }

        private void cmbGender_Leave(object sender, EventArgs e)
        {
            if (SpeechModule.GetStatus())
            {

                if (cmbGender.Text != "")
                {

                    synth.SetOutputToDefaultAudioDevice();
                    synth.Speak("you choosed" + cmbGender.Text + "as the gender");
                    synth.Speak("Please say the email Id");


                }
                else
                {
                    synth.Speak("Gender field cannot be empty please say the Gender field");
                    txtEmail.Focus();
                }

            }

        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (SpeechModule.GetStatus())
            {

                if (txtEmail.Text != "")
                {
                    if (IsValidEmail(txtEmail.Text))
                    {
                        synth.SetOutputToDefaultAudioDevice();
                        synth.Speak("you choosed" + txtEmail.Text + "as the email Id");
                        synth.Speak("Please choose the security question and answer");
                    }
                    else
                    {
                        synth.SetOutputToDefaultAudioDevice();
                        synth.Speak("The email id you have entered is invalid");
                        txtEmail.Text = "";
                        txtEmail.Focus();
                    }


                }
                else
                {
                    synth.Speak("Email Id cannot be empty please say the Email Id");
                    txtEmail.Focus();
                }
            }

        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (SpeechModule.GetStatus())
            {

                if (txtPassword.Text != "")
                {


                    synth.Speak("you choosed" + txtPassword.Text + "as the password");
                    synth.Speak("Please confirm the password again");


                }
                else
                {
                    synth.Speak("password cannot be empty please say your password");
                    txtPassword.Focus();
                }
            }


        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (SpeechModule.GetStatus())
            {

                if (txtConfirmPassword.Text != "")
                {


                    synth.Speak("you choosed" + txtConfirmPassword.Text + "as the password");

                }
                else
                {
                    synth.Speak("ConfirmPassword cannot be empty please say your ConfirmPassword");
                    txtConfirmPassword.Focus();
                }

            }

        }



        private void txtSecurityAns_Leave(object sender, EventArgs e)
        {
            if (SpeechModule.GetStatus())
            {

                if (txtSecurityAns.Text != "")
                {


                    synth.Speak("you choosed" + txtSecurityAns.Text + "as the security answer");
                    synth.Speak("Please say your password");

                }
                else
                {
                    synth.Speak("Security answer cannot be empty please say your security answer");
                    txtSecurityAns.Focus();
                }


            }

        }

        private void CmbSecQues_Leave()
        {
            if (SpeechModule.GetStatus())
            {

                if (txtSecurityAns.Text != "")
                {


                    synth.Speak("you choosed" + txtSecurityAns.Text + "as the security answer");


                }

            }

        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            // VoiceRecognizer();
        }

        private void txtPassword_Leave_1(object sender, EventArgs e)
        {
            if (txtPassword.Text != "")
            {
                Password = txtPassword.Text;
                txtPassword.Text = "*****";
            }
        }

        private void txtConfirmPassword_Leave_1(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text != "")
            {
                ConfrmPassWord = txtConfirmPassword.Text;
                txtConfirmPassword.Text = "*****";
            }
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbGender.SelectedIndex != -1)
            //{
            //    synth.Speak("you choosed" + cmbGender.Text + "as the gender");
            //}


        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

            txtname.Text = FilterCommand(txtname.Text);// replace the command and return the  content only and convert to lower case
            executeAction(CmdReader);//if next is there execute it
            CmdReader = "Nil";

        }

        public async Task RegcogniseCommad()
        {
            await Task.Delay(20);


        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {



            txtEmail.Text = FilterCommand(txtEmail.Text);
            executeAction(CmdReader);
            CmdReader = "Nil";


        }

        private void txtSecurityAns_TextChanged(object sender, EventArgs e)
        {


            txtSecurityAns.Text = FilterCommand(txtSecurityAns.Text);
            executeAction(CmdReader);
            CmdReader = "Nil";

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {


            txtPassword.Text = FilterCommand(txtPassword.Text);
            executeAction(CmdReader);
            CmdReader = "Nil";

        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            txtConfirmPassword.Text = FilterCommand(txtConfirmPassword.Text);
            executeAction(CmdReader);
            CmdReader = "Nil";
        }

        private void cmbGender_SelectedIndexChanged()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbGender_TextChanged(object sender, EventArgs e)
        {
            if (cmbGender.Text.ToString() != "")
            {

                cmbGender.Text = FilterCommand(cmbGender.Text);
                executeAction(CmdReader);
                CmdReader = "Nil";
            }

        }


    }
}

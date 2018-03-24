using SpeechToText.ApplicationLayer;
using SpeechToText.Business.Methods;
using SpeechToText.Business.Models;
using SpeechToText.Presentation_Layer;
using SpeechToText.Presentation_Layer.Preconditions;
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

namespace SpeechToText
{
    public partial class ComposeMail : Form
    {
        public enum Mailtype
        {
            ComposeMail,
            ReplyMail,
            DraftMail,
            ForwardMail
        };

        string _tomail = "";
        string _subject = "";
        string _message = "";
        Mailtype MailTypeEnum;
        int _msg_id;
        SpeechSynthesizer synth = new SpeechSynthesizer();
        public ComposeMail(Mailtype EnumType, string ToMail, string Subject, string Message, int MsgId)
        {
            MailTypeEnum = EnumType;
            _tomail = ToMail;
            _subject = Subject;
            _message = Message;
            _msg_id = MsgId;          
            InitializeComponent();
            //SpeechToTextalgorithm();
            WelcomeMessage();
            this.KeyPreview = true;
            //SpeechToTextalgorithm();
        }


        public async Task WelcomeMessage()
        {
            await Task.Delay(500);
            if (SpeechModule.GetStatus() && MailTypeEnum == Mailtype.ComposeMail) //Only for compose mail help text speech work
            {
                GreetingMsg();
            }
        }

        private void GreetingMsg()
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
            _recognizer.RecognizeAsync(RecognizeMode.Single);
            synth.SetOutputToDefaultAudioDevice();
            
            synth.Speak("You are in Compose mail window");
            synth.Speak("Say tab or press tab to goto the next field");
            synth.Speak("Enter the recipient email id");
            
            //synth.Speak("Please say next to move on to next control");
            //synth.Speak("Please say read email to read the email");
            //SpeechToTextalgorithm();
 
        }


        string CmdReader = "Nil";
        private string FilterCommand(string ReadText)
        {
            string RecognizedText = ReadText;

            List<string> ListCommands = new List<string>
        {
                "go to email", 
                "go to subject", 
                "go to message"
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
            //if (Command.Text == "Read email")
            //{
            //    txtEmail_Leave();
            //}
            //if (Command.Text == "Read subject")
            //{
            //    txtSubject_Leave();
            //}
            //if (Command.Text == "Read message")
            //{
            //    richTextBox1_Leave();
            //}
            if (Command  == "go to email")
            {
                txtEmail.Focus();
            }
            else if (Command  == "go to subject")
            {
                txtSubject.Focus();
            }

            else if (Command  == "go to message")
            {
                richTextBox1.Focus();
            }

            //else if (Command.Text == "sent")
            //{
            //    SentMail();

            //}
            //else if (Command.Text == "next")
            //{
            //    // SendKeys.Send("{TAB}");
            //    FocusControl();
            //}

        }


        private void SpeechToTextalgorithm()
        {
           // richTextBox1.Focus();
 
            PromptBuilder pbuilder = new PromptBuilder();
            SpeechRecognitionEngine sregEngine = new SpeechRecognitionEngine();


            Choices slist = new Choices();
            slist.Add(new string[] { 
                "go to email", 
                "go to subject", 
                "go to message"
                
                //"Read email",
                //"Read subject",
                //"Read message",
                //"sent"
               // "next",
                });
            Grammar gr = new Grammar(new GrammarBuilder(slist));

            try
            {
                sregEngine.RequestRecognizerUpdate();
                sregEngine.LoadGrammar(gr);
                sregEngine.SpeechRecognized += sregEngine_SpeechRecognized;
                sregEngine.SetInputToDefaultAudioDevice();
                sregEngine.RecognizeAsync(RecognizeMode.Multiple);
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
        //    //if (Command.Text == "Read email")
        //    //{
        //    //    txtEmail_Leave();
        //    //}
        //    //if (Command.Text == "Read subject")
        //    //{
        //    //    txtSubject_Leave();
        //    //}
        //    //if (Command.Text == "Read message")
        //    //{
        //    //    richTextBox1_Leave();
        //    //}
        //    if (Command.Text == "go to email")
        //    {
        //        txtEmail.Focus();
        //    }
        //    else if (Command.Text == "go to subject")
        //    {
        //       txtSubject.Focus();
        //    }

        //    else if (Command.Text == "go to message")
        //    {
        //        richTextBox1.Focus();
        //    }

        //    //else if (Command.Text == "sent")
        //    //{
        //    //    SentMail();

        //    //}
        //    //else if (Command.Text == "next")
        //    //{
        //    //    // SendKeys.Send("{TAB}");
        //    //    FocusControl();
        //    //}

        //}

        private void FocusControl()
        {
            if(txtEmail.Text=="")
            {
                synth.Speak("Email id cannot be empty please say a email id");
                txtEmail.Focus();

            }
            else if(txtSubject.Text=="")
            {
                synth.Speak("Please say a subject for this mail");
                txtSubject.Focus();
            }
            else if (richTextBox1.Text=="")
            {
              synth.Speak("Please tell your message for the recipient");
              richTextBox1.Focus();
            }
        }

        private void ComposeMail_Load(object sender, EventArgs e)
        {
            if (MailTypeEnum == Mailtype.DraftMail)
            {
                if (_tomail != "" && _subject != "" && _message != "")
                {
                    txtEmail.Text = _tomail;
                    txtSubject.Text = _subject;
                    richTextBox1.Text = _message;
                    btnDraft.Visible = false;
                }

            }
            else if (MailTypeEnum == Mailtype.ReplyMail)
            {
                if (_tomail != "" && _subject != "")
                {
                    txtEmail.Text = _tomail;
                    txtSubject.Text = "Re:" + _subject;
                    richTextBox1.Focus();
                }
            }
            else if (MailTypeEnum == Mailtype.ForwardMail)
            {
                if (_subject != "" && _message != "")
                {
                    txtSubject.Text = "Fwd:" + _subject;
                    richTextBox1.Text = _message;
                    btnDraft.Hide();
                }
            }

        }

        private void MailView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                //SpeechToTextalgorithm();
            }
            //if (e.KeyCode == Keys.Space)
            //{

            //    SendKeys.Send("{TAB}");
            //}

        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            SentMail();
            this.Close();

        }

        private void SentMail()
        {
            MailServer _composeMail = new MailServer();
            if (Mailtype.ComposeMail == MailTypeEnum || Mailtype.ReplyMail == MailTypeEnum || Mailtype.ForwardMail==MailTypeEnum)
            {
                Mail _mail = new Mail();
                _mail.EmailID = txtEmail.Text;
                _mail.Subject = txtSubject.Text;
                _mail.Message = richTextBox1.Text;
                _mail.FromEmailId = LoginCredentials.LoggedEmailId;
                _mail.FileName = AttachmentName;
                string IsEmptyUSer = IsEmpty.CheckIfEmpty_Mail(_mail);
                int User_id = UserBusinessModel.GetUser(_mail.EmailID);

                if (IsEmptyUSer != null)
                {
                    MessageBox.Show(IsEmptyUSer, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    
                    if (User_id != 0)
                    {
                        _mail.UserId = User_id;

                        if (_composeMail.SentMail(_mail))
                        {
                            if (SpeechModule.GetStatus() && MailTypeEnum == Mailtype.ComposeMail) //Only for compose mail help text speech work
                            {
                                synth.Speak("The mail sucessfully sent to the recipient");
                                synth.Speak("We are back to our main window");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Mail sent sucessfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }

                        else
                        {
                            MessageBox.Show("Unable to sent mail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                        MessageBox.Show("EmailID is not registered with our domain", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else if (Mailtype.DraftMail == MailTypeEnum)
            {

                if (MailServer.ResentDraftMail(_msg_id))
                {
                    if (SpeechModule.GetStatus() && MailTypeEnum == Mailtype.ComposeMail) //Only for compose mail help text speech work
                    {
                        synth.Speak("The mail sucessfully sent to the recipient");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Mail sent sucessfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                   
                }
                else
                {
                    MessageBox.Show("Unable to sent mail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void btnDraft_Click(object sender, EventArgs e)
        {
            Mail _mail = new Mail();
            _mail.EmailID = txtEmail.Text;
            _mail.Subject = txtSubject.Text;
            _mail.Message = richTextBox1.Text;
            _mail.FromEmailId = LoginCredentials.LoggedEmailId;
            _mail.FileName = AttachmentName;
            string IsEmptyUSer = IsEmpty.CheckIfEmpty_Mail(_mail);

            if (IsEmptyUSer != null)
            {
                MessageBox.Show(IsEmptyUSer, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int User_id = UserBusinessModel.GetUser(_mail.EmailID);
                if (User_id != 0)
                {
                    _mail.UserId = User_id;
                    MailServer _composeMail = new MailServer();
                    if (_composeMail.SaveMail(_mail))
                    {
                        MessageBox.Show("Mail saved to draft sucessfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    else
                    {
                        MessageBox.Show("Unable save this mail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                    MessageBox.Show("EmailID is not registered with our domain", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            this.Close();
        }

        private void txtEmail_Leave()
        {
            if (SpeechModule.GetStatus())
            {
                
                synth.SetOutputToDefaultAudioDevice();
                int User_id = UserBusinessModel.GetUser(txtEmail.Text);
                if (txtEmail.Text!="")
                {
                    synth.Speak("You said " + txtEmail.Text + "as the mail Id");
                    //synth.Speak("Please say the subject for this mail");
                  //  SpeechToTextalgorithm();
                }
                else
                {
                    synth.Speak("Email id field is empty");
                }
                //else
                //{


                //    synth.Speak("The mail id you told is not registered with our application");
                //    synth.Speak("Please say the recipient mail id and press the space button");
                //    txtEmail.Clear();
                //    txtEmail.Focus();

                //}
            }
        }

        private void txtSubject_Leave()
        {
            if (SpeechModule.GetStatus())
            {
                 
                synth.SetOutputToDefaultAudioDevice();
                
                if (txtSubject.Text!="")
                {
                    synth.Speak("You said " + txtSubject.Text + "as the subject for this mail");
                   // synth.Speak("Please say the message that you need to sent to your recipient");
                   // richTextBox1.Focus();
                  //  SpeechToTextalgorithm();
                }
                
                //else
                //{


                //    synth.Speak("Subject cannot be empty Please say a subject for this mail");
                //    synth.Speak("Please say the subject and press the space button");
                //    txtSubject.Clear();
                //    txtSubject.Focus();

                //}
            }
        }

        private void richTextBox1_Leave()
        {
            if (SpeechModule.GetStatus())
            {
                if (richTextBox1.Text != "")
                {
                    synth.Speak("You said " + richTextBox1.Text + "as the message for this mail");
                  //  btnSent.Focus();
                    //SpeechToTextalgorithm();
                }
                //else
                //{


                //    synth.Speak("you havnt told any message");
                //    richTextBox1.Focus();
                     

                //}
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if(txtEmail.Text!="")
            {
                txtEmail.Text = txtEmail.Text.ToLower();
                txtEmail_Leave();
            }

        }

        private void txtSubject_Leave(object sender, EventArgs e)
        {
            if(txtSubject.Text!="")
            {
                txtSubject_Leave();
            }
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if(richTextBox1.Text!="")
            {
                richTextBox1_Leave();
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmail.Text = FilterCommand(txtEmail.Text);
            executeAction(CmdReader);
            CmdReader = "Nil";
        }

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
            txtSubject.Text = FilterCommand(txtSubject.Text);
            executeAction(CmdReader);
            CmdReader = "Nil";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = FilterCommand(richTextBox1.Text);
            executeAction(CmdReader);
            CmdReader = "Nil";
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        string AttachmentName = "NIL";
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog op1 = new OpenFileDialog();

            op1.Multiselect = false;
            op1.ShowDialog();

            op1.Filter = "allfiles|*.xls";
            if (op1.FileName == "")
            {
                return;
            }

            if (op1.FileName!="")
            {
                var sourcePath = op1.FileName;
                if (sourcePath != null || sourcePath != "" || op1.Filter != "")
                {
                    string fileName = Path.GetFileName(op1.FileName);
                    string targetPath = @"C:\Ycet";


                    AttachmentName = fileName;
                    txtAttachment.Text = sourcePath;
                    string destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(sourcePath, destFile, true);
                }
            }

           
           

        }
    }
}

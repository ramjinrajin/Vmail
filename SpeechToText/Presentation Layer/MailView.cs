using SpeechToText.ApplicationLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechToText.Presentation_Layer
{
    public partial class MailView : Form
    {

        SpeechSynthesizer synth = new SpeechSynthesizer();

        // Configure the audio output. 
        

        string _fromEmail;
        string _Subject;
        string _Message;
        bool _isSentMail;
        bool KeyPress;
        int _msgId;
        string _attachment;

        public MailView(int MsgId, bool IsSentMail, string FromEmail, string Subject, string Message,string Attachment)
        {
            synth.SetOutputToDefaultAudioDevice();
            InitializeComponent();
            _fromEmail = FromEmail;
            _Subject = Subject;
            _Message = Message;
            lblFromMail.Focus();
            _isSentMail = IsSentMail;
            _attachment = Attachment;
            _msgId = MsgId;
            if(_isSentMail)
            {
                groupBox1.Text = "To";
                btnReply.Visible = false;
                btnDelete.Text = "Close";
            }
            this.KeyPreview = true;
            pictureBox1.Focus();
            if(Attachment=="NIL")
            {
                AttLbl.Visible = false;
                linkLabel1.Visible = false;
            }
            else
            {
                AttLbl.Visible = true;
                linkLabel1.Visible = true;
                linkLabel1.Text = Attachment;
            }

          
        }

        private void SpeechToTextalgorithm()
        {
            // richTextBox1.Focus();

            PromptBuilder pbuilder = new PromptBuilder();
            SpeechRecognitionEngine sregEngine = new SpeechRecognitionEngine();


            Choices slist = new Choices();
            slist.Add(new string[] { 
                "Forward",         
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

            if (e.Result.Text == "Forward")
            {
                GenerateForwardMail();
            }

        }


        private void MailView_Load(object sender, EventArgs e)
        {

           
             
            if (_fromEmail != "" && _Subject != "" && _Message != "")
            {
                lblFromMail.Text = _fromEmail;
                lblSubject.Text = _Subject;
                txtMsg.Text = _Message;
            }
        if(txtMsg.Text!="")
            {
                synth.Speak(txtMsg.Text);
            }
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (_Message != "")
            {
                SpeakTest(_Message);
            }
        }

        private  void SpeakTest(string Text)
        {

            // Speak a string.
            synth.Speak(Text);
        }

        private void txtMsg_KeyDown(object sender, KeyPressEventArgs e)
        {

        }

        
        private void MailView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                KeyPress = true;
                if (_Message != "")
                {
                    SpeakTest(_Message);
                }
            }
            
        }

 

        private void btnReply_Click(object sender, EventArgs e)
        {

            if (!KeyPress)
            {
                ReplyMail();
            }
            KeyPress = false;
        }

        private void ReplyMail()
        {
            // this.Close();
            List<Form> forms = new List<Form>();

            // All opened myForm instances
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "Mailbox")
                    forms.Add(f);
            }


            // Now let's close opened myForm instances
            foreach (Form f in forms)
            {
                f.Hide();
            }



            ComposeMail _composeMailForm = new ComposeMail(SpeechToText.ComposeMail.Mailtype.ReplyMail, _fromEmail, _Subject, _Message, 0);
            _composeMailForm.ShowDialog();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text!="Close")
            {
                if (MailServer.MarkMailAsDelete(_msgId))
                {
                    MessageBox.Show("Mail deleted sucessfully ,you can undelete it from recycle bin", "Done!!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {
                    MessageBox.Show("Unable to delete mail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            this.Close();
            
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            GenerateForwardMail();
        }

        private void GenerateForwardMail()
        {
            ComposeMail _composeMail = new ComposeMail(ComposeMail.Mailtype.ForwardMail, "", lblSubject.Text, txtMsg.Text, 0);
            _composeMail.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_attachment!="NIL")
            {
                Process.Start(@"C:\Ycet\"+_attachment);
            }
        }






    

        
    }
}

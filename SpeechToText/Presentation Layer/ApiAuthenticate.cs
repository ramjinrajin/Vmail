using SpeechToText.Business.Methods;
using SpeechToText.Business.Models;
using SpeechToText.Presentation_Layer.SessionData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechToText.Presentation_Layer
{

    public partial class ApiAuthenticate : Form
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();
        public ApiAuthenticate()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        List<string> listEmails = null;
        private void ApiAuthenticate_Load(object sender, EventArgs e)
        {
            synth.Speak("HI");
            synth.Speak("Please say your emailId");
            LblInvaliduser.Visible = false;
            Msg.Visible = false;
            Loader.Visible = false;
            SpeechRecognitionWithDictationGrammar();

            UserBusinessModel businesslayer = new UserBusinessModel();
            List<User> ListUser = businesslayer.GetUsers();
            listEmails = ListUser.Select(x => x.EmailID.ToLower()).ToList();

        }

        public SpeechRecognitionEngine _recognizer = null;
        public ManualResetEvent manualResetEvent = null;
        public void SpeechRecognitionWithDictationGrammar()
        {
            _recognizer = new SpeechRecognitionEngine();
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("compose mail")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("Open compose Window")));
            _recognizer.LoadGrammar(new DictationGrammar());
            _recognizer.SpeechRecognized += speechRecognitionWithDictationGrammar_SpeechRecognized;
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        string verifiedUserEmailId = "";
        private void speechRecognitionWithDictationGrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            
            bool IsInvaliduser = true;
            bool Isvaliduser = false;
            synth.SetOutputToDefaultAudioDevice();
            // synth.Speak(string.Format("You told {0}", e.Result.Text));
            MessageBox.Show(e.Result.Text); 
            foreach (var Email in listEmails)
            {
               // if(true)
                if (e.Result.Text.Trim().ToLower().Contains(Email.Trim().ToLower()))
                {
                    IsInvaliduser = false;
                    Isvaliduser = true;
                    txtUsername.Text = Email.Trim().ToLower();
                    Msg.Visible = true;
                    Loader.Visible = true;
                    verifiedUserEmailId = Email;
                    synth.Speak(string.Format("hi {0}", Email));
                    synth.Speak("Welcome Back");
                    UserBusinessModel UserModel = new UserBusinessModel();
                   int HitCount=  UserModel.GenerateBandHitCode(UserBusinessModel.GetUser(verifiedUserEmailId));
                    synth.Speak(string.Format("Please press your band {0} times to authorize your identity", HitCount));
                    LblInvaliduser.Visible = false;
                    _recognizer.Dispose();
                    break;
                }

            }




            if (IsInvaliduser)
            {
                LblInvaliduser.Visible = true;


              //  synth.Speak(string.Format("Invalid User", e.Result.Text));
                txtUsername.Text = "";
            }
            if (Isvaliduser)
            {
                InitAuthenticator();
            }
        }


        System.Windows.Forms.Timer timer1;
        public void InitAuthenticator()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 15000; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UserBusinessModel UserModel = new UserBusinessModel();
           bool Response= UserModel.IsAuthenticatedByApi(UserBusinessModel.GetUser(verifiedUserEmailId));
            if(Response)
            {
                LoginCredentials.LoggedEmailId = verifiedUserEmailId;
                List<Form> forms = new List<Form>();
               
                foreach (Form f in Application.OpenForms)
                {

                    forms.Add(f);
                }
                 
                foreach (Form f in forms)
                {
                    f.Hide();
                }


                timer1.Stop();
                Mailbox _mailbox = new Mailbox();
                _mailbox.Show();
            }
            else
            {
                synth.SpeakAsync("I am still waiting for your response ,Please press your band");

            }
          
        }



    }
}

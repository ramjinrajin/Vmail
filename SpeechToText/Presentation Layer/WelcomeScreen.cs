using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;

namespace SpeechToText
{
    public partial class WelcomeScreen : Form
    {
        public WelcomeScreen()
        {
            InitializeComponent();
        }

        SpeechRecognizer speechReconize = new SpeechRecognizer();

        private void Form1_Load(object sender, EventArgs e)
        {
            txtEmail.Focus();
            AtLoad();
           // speechReconize.SpeechRecognized+=speechReconize_SpeechRecognized;
        }

        private void speechReconize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //richTextBox2.AppendText(e.Result.ToString() + "");

            //if(e.Result.ToString()=="start")
            //{
            //    txtEmail.Focus();
            //}
            //else if(e.Result.ToString()=="X" || e.Result.ToString()=="next")
            //{
            //    txtSubject.Focus();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void AtLoad()
        {
            SpeechSynthesizer asynthesis = new SpeechSynthesizer();
            PromptBuilder pbuilder = new PromptBuilder();
            SpeechRecognitionEngine sregEngine = new SpeechRecognitionEngine();


            Choices slist = new Choices();
            slist.Add(new string[] { "Email", "Subject", "Message", "hello" ,"start"});
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
            if(e.Result.Text=="Email")
            {
                txtEmail.Focus();
            }
            else if(e.Result.Text=="Subject")
            {
                txtSubject.Focus();
            }

            else if(e.Result.Text=="Message")
            {
                richTextBox2.Focus();
            }

            else if (e.Result.Text == "hello" || e.Result.Text == "start")
            {
                panel1.Hide();
              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {  
             
            String strFullFilename = Path.Combine("~\\SqlScripts\\CreateDB.sql");
            //var zipFile = SpeechToText.Properties.Resources.;
        }

         
    }
}

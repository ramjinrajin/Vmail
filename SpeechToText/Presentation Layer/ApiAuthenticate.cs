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
        public ApiAuthenticate()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void ApiAuthenticate_Load(object sender, EventArgs e)
        {
            LblInvaliduser.Visible = false;
            Msg.Visible = false;
            Loader.Visible = false;
            SpeechRecognitionWithDictationGrammar();

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

        private void speechRecognitionWithDictationGrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();

            if (true)//Authenticate
            {
                synth.Speak(string.Format("You told {0}", e.Result.Text));
                synth.Speak(string.Format("Invalid User", e.Result.Text));

            }
             
        }
    }
}

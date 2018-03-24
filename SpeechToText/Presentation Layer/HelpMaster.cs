using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechToText.Presentation_Layer
{
    public partial class HelpMaster : Form
    {
        public HelpMaster()
        {
            InitializeComponent();
            WelcomeMessage();
        }

        private void HelpMaster_Load(object sender, EventArgs e)
        {
          
        }

        public async Task WelcomeMessage()
        {
            await Task.Delay(500);
            GreetingMsg();

        }

        private void GreetingMsg()
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("Hi Welcome to Speech to text ");
            synth.Speak("This application developed specially for blind people ");
            synth.Speak("Here is some commands that help to intract with this application");
            synth.Speak("Say Compose to compose a new mail");
            synth.Speak("say read my mails to read all unread mails for you");
            synth.Speak("say close to terminate this application");
            synth.Speak("Press the space button once you done any actions");
            this.Close();
        }

        
    }
}

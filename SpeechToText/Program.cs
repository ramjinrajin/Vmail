using SpeechToText.Presentation_Layer;
using SpeechToText.Presentation_Layer.SessionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechToText
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new UserLogin(false));
            LoginCredentials.LoggedEmailId = "heeracollegeofengineering@microsoftmail.com";
            Application.Run(new Mailbox());
           
        }
    }
}

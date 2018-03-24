using SpeechToText.Presentation_Layer;
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
            Application.Run(new ApiAuthenticate());
          //  Application.Run(new ComposeMail(SpeechToText.ComposeMail.Mailtype.ComposeMail, "", "", "", 0));
        }
    }
}

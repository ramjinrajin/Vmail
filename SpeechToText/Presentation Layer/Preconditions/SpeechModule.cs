using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.Presentation_Layer.Preconditions
{
   public static class SpeechModule
    {

       public static bool GetStatus()
       {
           string Credentials = System.IO.File.ReadAllText(@"C:\\Ycet\\YcetSpeechModuleEnabledKey.ini");
           if (Credentials.Trim() == "Enabled")
               return true;
           else
               return false;
       }

    }
}

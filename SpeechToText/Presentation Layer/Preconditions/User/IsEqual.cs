using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.Presentation_Layer
{
  public  class IsEqual
    {

      public static bool CompareText(string Value1,string Value2)
      {
          if (Value1 == Value2)
              return true;
          else
              return false;
      }
    }
}

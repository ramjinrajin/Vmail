using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.Business.Models
{
   public class User
    {
       public string UserId { get; set; }

       public string Name { get; set; }
       public string DOB { get; set; }
       public string Password { get; set; }
       public string Gender { get; set; }
       public string EmailID { get; set; }

       public string ConPassword { get; set; }
       public string SecurityAns { get; set; }
       public string SecutityQues { get; set; }
    }
}

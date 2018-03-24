using SpeechToText.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.Presentation_Layer
{
    public class IsEmpty
    {

        #region User
        public static string CheckIfEmpty_User(User _user)
        {
            if (_user.Name == "")
                return "Name field cannot be empty";
            else if (_user.Password == "")
                return "Password should not be empty";
            else if (_user.EmailID == "")
                return "EmailID should not be empty";
            else
                return null;

        }

        #endregion

        #region Mail

        public static string CheckIfEmpty_Mail(Mail _mail)
        {
            if(_mail.EmailID=="")
                return "EmailID field cannot be empty";
            else if (_mail.Subject == "")
                return "Subject should not be empty";
            else if (_mail.Message == "")
                return "Mail content should not be empty";

            return null;
        }





        #endregion
    }
}

using SpeechToText.Business.Methods;
using SpeechToText.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.ApplicationLayer
{

    public abstract class ManageUser
    {
         
        public abstract bool CreateUSer(User _user);
    }



    public class CreateNewUser : ManageUser
    {
        UserBusinessModel businessModel = new UserBusinessModel();
        public override bool CreateUSer(User _user)
        {
                     
         return  businessModel.InsertUsertoDatabase(_user);
             
        }

        public bool ResetPassword(User _user)
        {
            return businessModel.SetNewPassword(_user);
        }

        public bool IfExists(User _user)
        {
            return businessModel.IfexistsUser(_user);
        }


    }



}

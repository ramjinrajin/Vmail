using SpeechToText.Business.Methods;
using SpeechToText.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.Presentation_Layer.Preconditions.UserSettings
{
    public class UserPrivilege
    {
        #region USerValidations

        public static bool Isadmin(string Name)
        {
            if (Name == "admin")
                return true;
            else
                return false;
        }


        public static bool IsValidUser(string Username, string password)
        {
            if (adminUser(Username,password))
            {
                return true;
            }
            else
            {
                UserBusinessModel UserModel = new UserBusinessModel();
                List<User> listUsers = UserModel.GetUsers();
                foreach (var user in listUsers)
                {
                    if (user.EmailID == Username)
                    {
                        if (user.Password == password)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool adminUser(string Username, string Password)
        {
            if (Username == "admin@ycet.com" || Username == "Admin@ycet.com" && Password == "admin"|| Password == "Admin")
            {
                return true;
            }
            else
                return false;
        }

        #endregion
    }
}

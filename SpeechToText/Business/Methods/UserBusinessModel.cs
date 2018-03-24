using SpeechToText.Business.ConnectDB;
using SpeechToText.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SpeechToText.Business.Methods
{
    class UserBusinessModel
    {
        #region Users

        public bool InsertUsertoDatabase(User _user)
        {

            try
            {
                SqlCommand cmd = ExecuteSql.ExecuteCommand("Insert into YCET_USERS (Name,DOB,Gender,Password,EmailID,SecurityQues,SecurityAns) Values (@Name,@DOB,@Gender,@Password,@Email,@SecurityQues,@SecurityAns)");
                cmd.Parameters.AddWithValue("@Name", _user.Name);
                cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(_user.DOB));
                cmd.Parameters.AddWithValue("@Gender", _user.Gender);
                cmd.Parameters.AddWithValue("@Email", _user.EmailID);
                cmd.Parameters.AddWithValue("@Password", _user.Password);
                cmd.Parameters.AddWithValue("@SecurityQues", _user.SecutityQues);
                cmd.Parameters.AddWithValue("@SecurityAns", _user.SecurityAns);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {

                return false;
            }

        }

        public bool IfexistsUser(User _user)
        {
            SqlCommand cmd = ExecuteSql.ExecuteCommand("Select * from YCET_USERS Where EmailId=@EmailID");
            cmd.Parameters.AddWithValue("@EmailID", _user.EmailID);
            cmd.ExecuteNonQuery();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
                return false;
            else
                return true;
        }
        public List<User> GetUsers()
        {
            List<User> listuser = new List<User>();
            SqlCommand cmd = ExecuteSql.ExecuteCommand("Select * from YCET_USERS");
            cmd.ExecuteNonQuery();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                User _user = new User();
                _user.Name = rdr["Name"].ToString();
                _user.EmailID = rdr["EmailID"].ToString();
                _user.Password = rdr["Password"].ToString();
                listuser.Add(_user);

            }

            return listuser;
        }


        public static int GetUser(string EmailId)
        {
            int User_id = 0;
            SqlCommand cmd = ExecuteSql.ExecuteCommand("Select UserID from YCET_USERS where EmailID=@EmailID");
            cmd.Parameters.AddWithValue("@EmailID", EmailId);
            cmd.ExecuteNonQuery();
            SqlDataReader rdr = cmd.ExecuteReader();
            if(rdr.HasRows)
            {
                  while (rdr.Read())
                  {
                      User_id = (int)rdr["UserID"];
                  }
            }

            return User_id;
        }


        public bool DeleteUser(string Name)
        {
            try
            {
                SqlCommand cmd = ExecuteSql.ExecuteCommand(" DELETE FROM YCET_Mails WHERE USERID IN (select USERID from YCET_USERS Where Name=@NAME) DELETE FROM YCET_USERS Where NAME=@NAME");
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {

                return false;
            }
        }


        #endregion




        internal static bool Isadmin(string Name)
        {
            if (Name == "admin")
                return true;
            else
                return false;

        }

        internal bool SetNewPassword(User _user)
        {
            try
            {
                SqlCommand cmd = ExecuteSql.ExecuteCommand("UPDATE YCET_USERS SET PASSWORD=@Password WHERE Name=@NAME");
                cmd.Parameters.AddWithValue("@Name", _user.Name);
                cmd.Parameters.AddWithValue("@Password", _user.Password);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                
                return false;
            }
        }
    }
}

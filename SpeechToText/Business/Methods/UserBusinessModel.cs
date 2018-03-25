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
        public int GenerateRandomHitCount
        {
            get
            {
                Random rnd = new Random();
                int RandomNumber = rnd.Next(1, 9);
                return RandomNumber;
            }
        }
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


        public bool IsAuthenticatedByApi(int UserId)
        {
            using (SqlConnection con = new SqlConnection(ConnectRemoteSql.GetConnectionString))
            {

                bool IsValid = false; ;
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 IsAuthenticated From VmailUser Where UserId=@UserId", con);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.HasRows)
                    {
                        while(rdr.Read())
                        {
                            IsValid = Convert.ToInt32(rdr["IsAuthenticated"]) == 1;
                        }
                    }

                    return IsValid;
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        internal int GenerateBandHitCode(int userID)
        {
            int RandomNumberSet = GenerateRandomHitCount;
            using (SqlConnection con = new SqlConnection(ConnectRemoteSql.GetConnectionString))
            {

                 
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM VmailUser Where UserId=@UserId Insert into VmailUser(Userid,AuthCount,HITCount,IsAuthenticated) Values (@UserId,@HitCount,0,0)", con);
                    cmd.Parameters.AddWithValue("@UserId", userID);
                    cmd.Parameters.AddWithValue("@HitCount", RandomNumberSet);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    return RandomNumberSet;


                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
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

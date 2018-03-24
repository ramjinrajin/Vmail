using SpeechToText.Business.ConnectDB;
using SpeechToText.Business.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.Business.Methods
{
   public class MailBusinessModel
   {

       #region Mail
       public bool SentMail(Mail _mail)
       {


           try
           {
               SqlCommand cmd = ExecuteSql.ExecuteProcedure("YCET_MAIL_SENT");                          
               cmd.Parameters.AddWithValue("@UserID", _mail.UserId);
               cmd.Parameters.AddWithValue("@Subject", _mail.Subject);
               cmd.Parameters.AddWithValue("@Message", _mail.Message);
               cmd.Parameters.AddWithValue("@SenderID", _mail.FromEmailId);
               cmd.Parameters.AddWithValue("@FileName", _mail.FileName);
               cmd.ExecuteNonQuery();
               return true;
           }
           catch
           {

               return false;
           }

       }

       public static string GetMailCounts(string Username)
       {
           string Count="0";
           SqlCommand cmd = ExecuteSql.ExecuteCommand("select count(*) AS Count from ycet_mails WHERE EMAILID=@EMAILID");
           cmd.Parameters.AddWithValue("@EMAILID", Username);
           cmd.ExecuteNonQuery();
           SqlDataReader rdr = cmd.ExecuteReader();

           if(rdr.HasRows)
           {
               while (rdr.Read())
               {
                   Count = rdr["COUNT"].ToString();

               }
           }

           return Count;
       }

       public bool SaveMail(Mail _mail)
       {

           try
           {
               SqlCommand cmd = ExecuteSql.ExecuteProcedure("YCET_MAIL_DRAFT");
               cmd.Parameters.AddWithValue("@UserID", _mail.UserId);
               cmd.Parameters.AddWithValue("@Subject", _mail.Subject);
               cmd.Parameters.AddWithValue("@Message", _mail.Message);
               cmd.Parameters.AddWithValue("@SenderID", _mail.FromEmailId);
               cmd.Parameters.AddWithValue("@FileName", _mail.FileName);
               cmd.ExecuteNonQuery();
               return true;
           }
           catch
           {

               return false;
           }

       }


       public List<Mail> getAllSentMails(string LoggedEmailID)
       {
           List<Mail> Mails = new List<Mail>();

           SqlCommand cmd = ExecuteSql.ExecuteCommand("Select * from YCET_Mails Where SenderID=@SenderID AND ISDRAFT=0 order By 1 desc");
           cmd.Parameters.AddWithValue("@SenderID", LoggedEmailID);
           cmd.ExecuteNonQuery();
           SqlDataReader rdr = cmd.ExecuteReader();

           while (rdr.Read())
           {
               Mail _mail = new Mail();
               _mail.EmailID = rdr["EmailID"].ToString();
               _mail.Subject = rdr["Subject"].ToString();
               _mail.Message = rdr["Message"].ToString();
               _mail.MsgId = (int)rdr["MsgID"];
               _mail.FromEmailId = rdr["SenderId"].ToString();
               _mail.FileName = rdr["FileName"].ToString();
               Mails.Add(_mail);

           }

           return Mails;
       }


       public List<Mail> getAllReceivedMails(string LoggedEmailID)
       {
           List<Mail> Mails = new List<Mail>();

           SqlCommand cmd = ExecuteSql.ExecuteCommand("Select * from YCET_Mails Where EMAILID=@EMAILID AND ISDRAFT=0 AND Isdeleted=0 order by 1 desc");
           

           cmd.Parameters.AddWithValue("@EMAILID", LoggedEmailID);
           cmd.ExecuteNonQuery();
           SqlDataReader rdr = cmd.ExecuteReader();

           while (rdr.Read())
           {
               Mail _mail = new Mail();
               _mail.EmailID = rdr["EmailID"].ToString();
               _mail.Subject = rdr["Subject"].ToString();
               _mail.Message = rdr["Message"].ToString();
               _mail.FromEmailId = rdr["SenderId"].ToString();
               _mail.IsRead = (int)rdr["IsRead"];
               _mail.IsDraft = (int)rdr["IsDraft"];
               _mail.MsgId = (int)rdr["MsgID"];
               _mail.FileName = rdr["FileName"].ToString();
               Mails.Add(_mail);

           }

           return Mails;
       }

       public List<Mail> getSearchedMails(string LoggedEmailID,string SearchQuery)
       {
           List<Mail> Mails = new List<Mail>();
           SqlCommand cmd = ExecuteSql.ExecuteProcedure("YCET_SEARCH");
          // SqlCommand cmd = ExecuteSql.ExecuteCommand("select * from YCET_Mails WHERE   Message like '%'+@QUERY+'%' AND EMAILID=@EMAILID AND ISDRAFT=0 AND Isdeleted=0 order by 1 desc");
           cmd.Parameters.AddWithValue("@EmailID", LoggedEmailID);
           cmd.Parameters.AddWithValue("@SearchTerm", string.Format("{0}", "%" + SearchQuery + "%"));
           cmd.ExecuteNonQuery();
           SqlDataReader rdr = cmd.ExecuteReader();

           while (rdr.Read())
           {
               Mail _mail = new Mail();
               _mail.EmailID = rdr["EmailID"].ToString();
               _mail.Subject = rdr["Subject"].ToString();
               _mail.Message = rdr["Message"].ToString();
               _mail.FromEmailId = rdr["SenderId"].ToString();
               _mail.IsRead = (int)rdr["IsRead"];
               _mail.IsDraft = (int)rdr["IsDraft"];
               _mail.MsgId = (int)rdr["MsgID"];
               _mail.FileName = rdr["FileName"].ToString();
               Mails.Add(_mail);

           }

           return Mails;
       }

       public List<Mail> getAllDeletedMails(string LoggedEmailID)
       {
           List<Mail> Mails = new List<Mail>();

           SqlCommand cmd = ExecuteSql.ExecuteCommand("Select * from YCET_Mails Where EMAILID=@EMAILID AND Isdeleted=1");
           cmd.Parameters.AddWithValue("@EMAILID", LoggedEmailID);
           cmd.ExecuteNonQuery();
           SqlDataReader rdr = cmd.ExecuteReader();

           while (rdr.Read())
           {
               Mail _mail = new Mail();
               _mail.EmailID = rdr["EmailID"].ToString();
               _mail.Subject = rdr["Subject"].ToString();
               _mail.Message = rdr["Message"].ToString();
               _mail.FromEmailId = rdr["SenderId"].ToString();
               _mail.IsRead = (int)rdr["IsRead"];
               _mail.IsDraft = (int)rdr["IsDraft"];
               _mail.MsgId = (int)rdr["MsgID"];
               _mail.FileName = rdr["FileName"].ToString();
               Mails.Add(_mail);

           }

           return Mails;
       }

       public List<Mail> getAllDraftMails(string LoggedEmailID)
       {
           List<Mail> Mails = new List<Mail>();

           SqlCommand cmd = ExecuteSql.ExecuteCommand("Select * from YCET_Mails Where SenderID=@SenderID AND ISDRAFT=1 AND Isdeleted=0");
           cmd.Parameters.AddWithValue("@SenderID", LoggedEmailID);
           cmd.ExecuteNonQuery();
           SqlDataReader rdr = cmd.ExecuteReader();

           while (rdr.Read())
           {
               Mail _mail = new Mail();
               _mail.EmailID = rdr["EmailID"].ToString();
               _mail.Subject = rdr["Subject"].ToString();
               _mail.Message = rdr["Message"].ToString();
               _mail.FromEmailId = rdr["SenderId"].ToString();
               _mail.IsRead = (int)rdr["IsRead"];
               _mail.IsDraft = (int)rdr["IsDraft"];
               _mail.MsgId = (int)rdr["MsgID"];
               _mail.FileName = rdr["FileName"].ToString();
               Mails.Add(_mail);

           }

           return Mails;
       }





       public bool MarkMailAsRead(int MsgId)
       {
           try
           {
               SqlCommand cmd = ExecuteSql.ExecuteCommand("UPDATE YCET_Mails SET ISREAD=1 Where MsgId=@MsgId");
               cmd.Parameters.AddWithValue("@MsgId", MsgId);
               cmd.ExecuteNonQuery();
               return true;
           }
           catch
           {
               return false;
           }
           
       }
       public bool MarkMailAsDelete(int MsgId)
       {
           try
           {
               SqlCommand cmd = ExecuteSql.ExecuteCommand("UPDATE YCET_Mails SET Isdeleted=1 Where MsgId=@MsgId");
               cmd.Parameters.AddWithValue("@MsgId", MsgId);
               cmd.ExecuteNonQuery();
               return true;
           }
           catch
           {
               return false;
           }

       }

       public bool MarkMailAsUnDelete(int MsgId)
       {
           try
           {
               SqlCommand cmd = ExecuteSql.ExecuteCommand("UPDATE YCET_Mails SET Isdeleted=0 Where MsgId=@MsgId");
               cmd.Parameters.AddWithValue("@MsgId", MsgId);
               cmd.ExecuteNonQuery();
               return true;
           }
           catch
           {
               return false;
           }

       }

       public bool ResentDraftMail(int Msgid)
       {
           try
           {
               SqlCommand cmd = ExecuteSql.ExecuteCommand("UPDATE YCET_Mails SET IsDraft=0 Where MsgId=@MsgId");
               cmd.Parameters.AddWithValue("@MsgId", Msgid);
               cmd.ExecuteNonQuery();
               return true;
           }
           catch
           {
               return false;
           }
       }

       #endregion


       internal bool HardDeleteMail(int Msgid)
       {
           try
           {
               SqlCommand cmd = ExecuteSql.ExecuteCommand("UPDATE  YCET_Mails SET Isdeleted=2 Where MsgId=@MsgId");
               cmd.Parameters.AddWithValue("@MsgId", Msgid);
               cmd.ExecuteNonQuery();
               return true;
           }
           catch
           {
               return false;
           }
       }



       internal static List<string> GetSecQues(string Email)
       {
           List<string> SecurityParameters = new List<string>();

           SqlCommand cmd = ExecuteSql.ExecuteCommand("select SecurityQues,SecurityAns from YCET_USERS WHERE EMAILID=@EMAILID");
           cmd.Parameters.AddWithValue("@EMAILID", Email);
           cmd.ExecuteNonQuery();
           SqlDataReader rdr = cmd.ExecuteReader();

           if (rdr.HasRows)
           {
               while (rdr.Read())
               {
                   SecurityParameters.Add(rdr["SecurityQues"].ToString());
                   SecurityParameters.Add(rdr["SecurityAns"].ToString());
               }
           }

           return SecurityParameters;
       }
   }
}

using SpeechToText.Business.Methods;
using SpeechToText.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.ApplicationLayer
{
    public abstract class ManageMail
    {

        public abstract bool SentMail(Mail _mail);
   
    }

    public class MailServer : ManageMail
    {
        MailBusinessModel _mailBusniessmodel = new MailBusinessModel();
        public override bool SentMail(Mail _mail)
        {

            return _mailBusniessmodel.SentMail(_mail);
        }

        public bool SaveMail(Mail _mail)
        {
            return _mailBusniessmodel.SaveMail(_mail);
        }

        public List<Mail> SentMailsList(string LoggedEmailId)
        {
            List<Mail> Mails;
            MailBusinessModel _mailBusModel = new MailBusinessModel();
            Mails= _mailBusModel.getAllSentMails(LoggedEmailId);
            return Mails;

         
        }

        public List<Mail> ReceivedMailsList(string LoggedEmailId)
        {
            List<Mail> Mails;
            MailBusinessModel _mailBusModel = new MailBusinessModel();
            Mails = _mailBusModel.getAllReceivedMails(LoggedEmailId);
            return Mails;
        }

        public List<Mail> SearchMailsList(string LoggedEmailId,string SearchQuery)
        {
            List<Mail> Mails;
            MailBusinessModel _mailBusModel = new MailBusinessModel();
            Mails = _mailBusModel.getSearchedMails(LoggedEmailId, SearchQuery);
            return Mails;
        }

        public List<Mail> DeletedMailsList(string LoggedEmailId)
        {
            List<Mail> Mails;
            MailBusinessModel _mailBusModel = new MailBusinessModel();
            Mails = _mailBusModel.getAllDeletedMails(LoggedEmailId);
            return Mails;
        }

        public List<Mail> DraftMailsList(string LoggedEmailId)
        {
            List<Mail> Mails;
            MailBusinessModel _mailBusModel = new MailBusinessModel();
            Mails = _mailBusModel.getAllDraftMails(LoggedEmailId);
            return Mails;
        }

        public static bool MarkMsgAsRead(int Msgid)
        {
            MailBusinessModel _mailBusModel = new MailBusinessModel();
            return _mailBusModel.MarkMailAsRead(Msgid);
       
        }

        public static bool ResentDraftMail(int Msgid)
        {
            MailBusinessModel _mailBusModel = new MailBusinessModel();
            return _mailBusModel.ResentDraftMail(Msgid);
        }


        public static bool MarkMailAsDelete(int Msgid)
        {
            MailBusinessModel _mailBusModel = new MailBusinessModel();
            return _mailBusModel.MarkMailAsDelete(Msgid);
        }

        public static bool MarkMailAsUnDelete(int Msgid)
        {
            MailBusinessModel _mailBusModel = new MailBusinessModel();
            return _mailBusModel.MarkMailAsUnDelete(Msgid);
        }

        public static bool  MailHardDelete(int Msgid)
        {
            MailBusinessModel _mailBusModel = new MailBusinessModel();
            return _mailBusModel.HardDeleteMail(Msgid);
        }

        public static string GetMailCount(string Username)
        {
            return MailBusinessModel.GetMailCounts(Username);
        }

        public static List<string> GetSecurityQues(string Email)
        {
            return MailBusinessModel.GetSecQues(Email);
        }
    }

}

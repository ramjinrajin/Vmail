using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText.Business.Models
{
   public class Mail
    {
       public int MsgId { get; set; }
        public int UserId { get; set; }

        public string EmailID { get; set; }

        public string Subject { get; set; }

        public string FromEmailId { get; set; }

        public string Message { get; set; }

        public int Isdeleted { get; set; }

        public int IsRead { get; set; }

        public int IsDraft { get; set; }
        public DateTime createdate { get; set; }

       public string FileName {get;set;}

    }
}

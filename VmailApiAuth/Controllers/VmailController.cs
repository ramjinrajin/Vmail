using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VmailApiAuth.Models;

namespace VmailApiAuth.Controllers
{
    public class VmailController : Controller
    {


        Authenticator objAuthenticator = new Authenticator();
        [HttpGet]
        public int Authorize(int UserId)  
        {
            int i = 0;
            objAuthenticator.GetHitCount(UserId);




            return i;
        }
    }
}
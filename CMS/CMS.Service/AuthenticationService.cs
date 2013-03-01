using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CMS.Service
{
    public class AuthenticationService
    {
        private bool ValidateUser(string userName, string passWord)
        {
            if (userName == "kay" && passWord == "gerya")
                return true;
            else return false;
        }

        private void AuthenticateUser(string userName, string passWord)
        {
            if (ValidateUser(userName, passWord))
            {
                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;
                tkt = new FormsAuthenticationTicket(1, userName, DateTime.Now,
                DateTime.Now.AddMinutes(30), true, userName);
                cookiestr = FormsAuthentication.Encrypt(tkt);
                ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                ck.Expires = tkt.Expiration;
                ck.Path = FormsAuthentication.FormsCookiePath;
                HttpContext.Current.Response.Cookies.Add(ck);

                string strRedirect;
                strRedirect = HttpContext.Current.Request["ReturnUrl"];
                if (strRedirect == null)
                    strRedirect = "/default.aspx";
                HttpContext.Current.Response.Redirect(strRedirect, true);

            }
            else
                HttpContext.Current.Response.Redirect("/login.aspx", true);
        }


    }
}
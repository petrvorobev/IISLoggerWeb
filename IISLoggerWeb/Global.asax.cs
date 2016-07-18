using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace IISLoggerWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected string fileNameBase = "c:\\temp\\log-";
        protected string ext = "log";
 
        // One file name per day
        protected string FileName
        {
            get
            {
                return String.Format("{0}{1}.{2}", fileNameBase, DateTime.Now.ToString("yyyy - MM - dd"), ext);
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //// Creates a unique id to match Rquests with Responses
            //string id = String.Format("Id: {0} Uri: {1}", Guid.NewGuid(), Request.Url);
            //FilterSaveLog input = new FilterSaveLog(HttpContext.Current, Request.Filter, FileName, id);
            //Request.Filter = input;
            //input.SetFilter(false);
            //FilterSaveLog output = new FilterSaveLog(HttpContext.Current, Response.Filter, FileName, id);
            //output.SetFilter(true);
            //Response.Filter = output;
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
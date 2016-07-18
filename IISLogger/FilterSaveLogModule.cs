using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace IISLogger
{
    public class FilterSaveLogModule : IHttpModule
    {



        private static Dictionary<string, PropertyInfo> _formatProperties = new Dictionary<string, PropertyInfo>();

        private string _directory;

        private string _fileFormat;

        private HttpApplication _context;

        [LogFormat("y")]
        public int Year
        {
            get
            {
                return DateTime.Now.Year;
            }
        }

        [LogFormat("m")]
        public int Month
        {
            get
            {
                return DateTime.Now.Month;
            }
        }

        [LogFormat("d")]
        public int Day
        {
            get
            {
                return DateTime.Now.Day;
            }
        }

        [LogFormat("H")]
        public int Hour
        {
            get
            {
                return DateTime.Now.Hour;
            }
        }


        [LogFormat("i")]
        public int Minute
        {
            get
            {
                return DateTime.Now.Minute;
            }
        }

        [LogFormat("s")]
        public int Second
        {
            get
            {
                return DateTime.Now.Second;
            }
        }

        [LogFormat("host")]
        public string Host
        {
            get
            {
                return _context.Request.Url.Host;
            }
        }

        [LogFormat("port")]
        public int Port
        {
            get
            {
                return _context.Request.Url.Port;
            }
        }

        static FilterSaveLogModule()
        {
            var props = typeof(IISLogger.FilterSaveLogModule).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            foreach (var prop in props)
            {
                var attr = Attribute.GetCustomAttribute(prop, typeof(IISLogger.LogFormatAttribute)) as IISLogger.LogFormatAttribute;
                if (attr != null)
                {
                    _formatProperties.Add("{" + attr.FormatKey + "}", prop);
                }

            }
        }

        public void Dispose()
        {
            _context = null;
        }
    //        <add key = "iislogger:folder" value="c:\temp\logs"/>
    //<add key = "iislogger:logformat" value="{host}_{port}\{y}\{m}\{d}\{H}_{i}_{s}.log"/>
        public void Init(HttpApplication context)
        {
            _directory = System.Configuration.ConfigurationManager.AppSettings["iislogger:folder"] ?? @"c:\temp\logs";
            _fileFormat = System.Configuration.ConfigurationManager.AppSettings["iislogger:logformat"] ?? @"{host}_{port}\{y}\{m}\{d}\{H}_{i}_{s}.log";
            _context = context;
            context.BeginRequest += Context_BeginRequest;
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {

            var filename = GetFilename();

            // Creates a unique id to match Rquests with Responses
            string id = String.Format("Id: {0} Uri: {1}", Guid.NewGuid(), _context.Request.Url);
            FilterSaveLog input = new FilterSaveLog(HttpContext.Current, _context.Request.Filter, filename, id);
            _context.Request.Filter = input;
            input.SetFilter(false);
            FilterSaveLog output = new FilterSaveLog(HttpContext.Current, _context.Response.Filter, filename, id);
            output.SetFilter(true);
            _context.Response.Filter = output;
        }

        private string GetFilename()
        {
            var filename = _fileFormat;
            foreach (var pair in _formatProperties)
            {
                filename = filename.Replace(pair.Key, pair.Value.GetValue(this, null).ToString());
            }
            filename = Path.Combine(_directory, filename);

            var fileinfo = new FileInfo(filename);

            if (!fileinfo.Directory.Exists)
            {
                fileinfo.Directory.Create();
            }

            return fileinfo.FullName;
        }
    }
}

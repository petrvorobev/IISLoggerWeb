using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IISLogger
{
    public class LogFormatAttribute:Attribute
    {
        public string FormatKey { get;  set; }

        public LogFormatAttribute(string formatKey)
        {
            FormatKey = formatKey;
        }
    }
}

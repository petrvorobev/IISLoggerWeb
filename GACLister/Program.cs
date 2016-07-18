using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GACLister
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, PropertyInfo> _formatProperties = new Dictionary<string, PropertyInfo>();

            var props = typeof(IISLogger.FilterSaveLogModule).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
             
            foreach (var prop in props)
            {
                var attr = Attribute.GetCustomAttribute(prop, typeof(IISLogger.LogFormatAttribute)) as IISLogger.LogFormatAttribute;
                if (attr!=null)
                {
                    _formatProperties.Add(attr.FormatKey, prop);
                }

            }

            Console.Read();
        }
    }
}

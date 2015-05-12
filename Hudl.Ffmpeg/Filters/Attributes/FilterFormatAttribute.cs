using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hudl.FFmpeg.Filters.BaseTypes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FilterFormatAttribute : Attribute
    {
        public FilterFormatAttribute(Type formatter)
        {
            Formatter = formatter;
        }

        public Type Formatter { get; set; }
    }
}

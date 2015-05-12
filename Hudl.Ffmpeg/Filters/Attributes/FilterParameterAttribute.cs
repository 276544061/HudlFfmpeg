using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hudl.FFmpeg.Filters.BaseTypes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FilterParameterAttribute : Attribute
    {
        public FilterParameterAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}

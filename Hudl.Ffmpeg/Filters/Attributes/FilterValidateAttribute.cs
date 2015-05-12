using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hudl.FFmpeg.Filters.BaseTypes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FilterValidateAttribute : Attribute
    {
        public FilterValidateAttribute(LogicalOperators op, object value)
        {
            Operator = op;
            Value = value;
        }

        public object Value { get; set; }

        public LogicalOperators Operator { get; set; }
    }
}

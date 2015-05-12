using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hudl.FFmpeg.Filters.Formatters
{
    public class EnumFormatter
    {
        public string Format(object value)
        {
            return Formats.EnumValue(TransitionType);
        }
    }
}

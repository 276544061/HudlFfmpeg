using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hudl.FFmpeg.Filters.BaseTypes
{
    public class FilterAttribute : Attribute
    {
        public string Name { get; set; }

        public int MaxInputs { get; set; }
    }
}

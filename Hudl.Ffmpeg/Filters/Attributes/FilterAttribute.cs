using System;

namespace Hudl.FFmpeg.Filters.Attributes
{
    public class FilterAttribute : Attribute
    {
        public FilterAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public int? MaxInputs { get; set; }

        public int? MinInputs { get; set; }
    }
}

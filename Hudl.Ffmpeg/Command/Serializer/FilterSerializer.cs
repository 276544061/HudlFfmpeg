using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hudl.FFmpeg.Command.Serializer
{
    public class FilterSerializer
    {
        public static string Serialize<TFilterType>(TFilterType filter) 
        { 
            var filterType = filter.GetType();
            var filterAttribute = (Filterattribute) Attribute.GetCustomAttribute(filter, typeof (Filterattribute));.
            
            var filterProperties = filterType.GetProperties()
        }

        public 
    }
}

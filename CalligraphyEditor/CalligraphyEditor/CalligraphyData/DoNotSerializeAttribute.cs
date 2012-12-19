using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalligraphyEditor.CalligraphyData
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DoNotSerializeAttribute : Attribute
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionHelper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StringMinLengthAttribute : Attribute
    {
        public int MinimumLength { get; }

        public StringMinLengthAttribute(int minimumLength)
        {
            MinimumLength = minimumLength;
        }
    }
}

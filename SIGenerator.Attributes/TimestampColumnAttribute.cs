using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Attributes
{
    public class TimestampColumnAttribute : ColumnAttribute
    {
        public TimestampColumnAttribute(bool required)
            : base(required) { }
    }
}
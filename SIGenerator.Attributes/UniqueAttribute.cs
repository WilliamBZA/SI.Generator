using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Attributes
{
    public class UniqueAttribute : ColumnAttribute
    {
        public UniqueAttribute(bool required)
            : base(required) { }

        public string TableSpace { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Attributes
{
    public class KeyAttribute : ColumnAttribute
    {
        public KeyAttribute()
            : base(true) { }

        public string TableSpace { get; set; }
    }
}
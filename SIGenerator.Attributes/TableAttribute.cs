using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Attributes
{
    public class TableAttribute : Attribute
    {
        public string TableName { get; set; }

        public string TableSpace { get; set; }

        public string Server { get; set; }

        public string Connect { get; set; }
    }
}
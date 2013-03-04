using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Parser
{
    public class TableColumnProperties
    {
        public IEnumerable<KeyColumn> PrimaryKeys { get; set; }

        public IEnumerable<UniqueColumn> UniqueColumns { get; set; }

        public IEnumerable<NormalColumn> NormalColumns { get; set; }

        public IEnumerable<LinkColumn> LinkColumns { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGenerator.Parser
{
    public class FormattedResults
    {
        public string Server { get; set; }

        public string Connect { get; set; }

        public string TableSpace { get; set; }

        public string TableName { get; set; }

        public string PrimaryKeys { get; set; }

        public string UniqueColumns { get; set; }

        public string FileSuffix { get; set; }

        public string Columns { get; set; }
    }
}

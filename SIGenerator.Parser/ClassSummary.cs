using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGenerator.Parser
{
    public class ClassSummary
    {
        public string ClassName { get; set; }

        public string TableName { get; set; }

        public Type ClassType { get; set; }

        public string Connect { get; set; }

        public string Server { get; set; }

        public string TableSpace { get; set; }

        public TableColumnProperties Columns { get; set; }
    }
}
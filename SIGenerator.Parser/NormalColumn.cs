using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGenerator.Parser
{
    public class NormalColumn
    {
        public NormalColumn(string columnName, string type, bool required)
        {
            ColumnName = columnName;
            Type = type;
            Required = required;
        }

        public string ColumnName { get; set; }

        public int Length { get; set; }

        public bool Required { get; set; }

        public int Precision { get; set; }

        public string Type { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Attributes
{
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(bool required)
        {
            Required = required;
        }

        public string ColumnName { get; set; }

        public int Length { get; set; }

        public int Precision { get; set; }

        public bool Required { get; set; }
    }
}
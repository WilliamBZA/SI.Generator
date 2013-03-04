using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Parser
{
    public class StandardSIFormatter
    {
        private ClassSummary _summary;

        public StandardSIFormatter(ClassSummary summary)
        {
            _summary = summary;
        }

        public string TableSpace()
        {
            return string.IsNullOrWhiteSpace(_summary.TableSpace) ? string.Empty : "TABLESPACE " + _summary.TableSpace + @"

";
        }

        public string Connect()
        {
            return string.IsNullOrWhiteSpace(_summary.Connect) ? string.Empty : "CONNECT " + _summary.Connect + @"

";
        }

        public string Server()
        {
            return string.IsNullOrWhiteSpace(_summary.Server) ? string.Empty : "SERVER " + _summary.Server + @"
";
        }

        public string TableName()
        {
            return "TABLE " + _summary.TableName + @"
";
        }

        public string PrimaryKeys()
        {
            var primaryKeyDefinitions = _summary.Columns.PrimaryKeys.GroupBy(key => key.TableSpace)
                .Select((g, index) => "KEY pk" + (index + 1).ToString("D2") + " PRIMARY" + (string.IsNullOrWhiteSpace(g.Key) ? string.Empty : (" TABLESPACE " + g.Key)) + @"
    " + string.Join(@"
    ", g.Select(gr => gr.ColumnName)));

            return string.Join(@"
", primaryKeyDefinitions) + @"
";
        }

        public string UniqueColumns()
        {
            return string.Join(@"
", _summary.Columns.UniqueColumns.Select((c, index) => @"
KEY key" + (index + 1).ToString("D2") + " UNIQUE" + (string.IsNullOrWhiteSpace(c.TableSpace) ? string.Empty : (" TABLESPACE " + c.TableSpace)) + @"
    " + c.ColumnName)) + @"
";
        }

        public string Columns()
        {
            foreach (var column in _summary.Columns.NormalColumns)
            {
                var mappedType = MapType(column.Type);
                if (mappedType == "FLOAT")
                {
                    if (column.Length <= 0)
                    {
                        column.Length = 15;
                    }

                    if (column.Precision <= 0)
                    {
                        column.Precision = 2;
                    }
                }

                if (column.Type == "Boolean")
                {
                    column.Length = 1;
                    column.Precision = 0;
                }
            }

            return string.Join(@"
", _summary.Columns.NormalColumns.Select(c => "    " + c.ColumnName + " " + MapType(c.Type) + (c.Length > 0 ? ("(" + c.Length.ToString() + (c.Precision > 0 ? (", " + c.Precision.ToString()) : string.Empty) + ")") : string.Empty) + (c.Required ? string.Empty : " NULL"))) + @"

";
        }

        private string MapType(string nativeType)
        {
            switch (nativeType)
            {
                case "Int32":
                    return "INT";

                case "String":
                case "Boolean":
                    return "CHAR";

                case "TimeStamp":
                    return "TIMESTAMP";

                case "Date":
                    return "DATE";

                case "DateTime":
                    return "DATETIME";

                case "Decimal":
                    return "FLOAT";

                default:
                    throw new NotImplementedException();
            }
        }

        public string Suffix()
        {
            return @"
GRANT SELECT INSERT DELETE UPDATE TO PUBLIC

PROC Insert
PROC Update
PROC SelectOne
PROC DeleteOne
PROC Exists
PROC SelectAll";
        }

        public FormattedResults FormatIntoObject()
        {
            var result = new FormattedResults();

            result.Server = this.Server();
            result.Connect = this.Connect();
            result.TableSpace = this.TableSpace();
            result.TableName = this.TableName();
            result.PrimaryKeys = this.PrimaryKeys();
            result.UniqueColumns = this.UniqueColumns();
            result.FileSuffix = this.Suffix();
            result.Columns = this.Columns();

            return result;
        }
    }
}
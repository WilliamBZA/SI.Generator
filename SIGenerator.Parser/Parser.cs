using SIGenerator.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SIGenerator.Parser
{
    public class Parser
    {
        public IDictionary<string, string> Parse(string classDiagramLocation)
        {
            var classDiagram = ReadFile(classDiagramLocation);

            var callingAssembly = Assembly.GetCallingAssembly();

            var classesInDiagram = (from c in classDiagram.Class
                                    let classType = callingAssembly.GetType(c.Name, true)
                                    let tableAttributes = (classType.GetCustomAttributes(typeof(TableAttribute), true).Select(o => (TableAttribute)o)).FirstOrDefault()
                                    where tableAttributes != null
                                    select new ClassSummary
                                    {
                                        ClassName = classType.Name,
                                        TableName = tableAttributes.TableName,
                                        ClassType = classType,
                                        Connect = tableAttributes.Connect,
                                        Server = tableAttributes.Server,
                                        TableSpace = tableAttributes.TableSpace,
                                        Columns = ExtractColumnProperties(classType)
                                    });

            return classesInDiagram.ToDictionary(s => s.ClassName, s => 
                {
                    var details = FormatResults(s);

                    return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", details.Server, details.Connect, details.TableSpace, details.TableName, details.Columns, details.PrimaryKeys, details.UniqueColumns, details.FileSuffix);
                });
        }

        private FormattedResults FormatResults(ClassSummary summary)
        {
            var formatter = new StandardSIFormatter(summary);
            return formatter.FormatIntoObject();
        }

        

        private TableColumnProperties ExtractColumnProperties(Type classType)
        {
            var primaryKeys = from prop in classType.GetProperties()
                              let propertyType = prop.PropertyType
                              let keyAttribute = (KeyAttribute)prop.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault()
                              where keyAttribute != null
                              select new KeyColumn
                              {
                                  ColumnName = string.IsNullOrWhiteSpace(keyAttribute.ColumnName) ? prop.Name : keyAttribute.ColumnName,
                                  TableSpace = keyAttribute.TableSpace
                              };

            var uniqueColumns = from prop in classType.GetProperties()
                                let propertyType = prop.PropertyType
                                let uniqueAttribute = (UniqueAttribute)prop.GetCustomAttributes(typeof(UniqueAttribute), true).FirstOrDefault()
                                where uniqueAttribute != null
                                select new UniqueColumn
                                {
                                    ColumnName = string.IsNullOrWhiteSpace(uniqueAttribute.ColumnName) ? prop.Name : uniqueAttribute.ColumnName,
                                    TableSpace = uniqueAttribute.TableSpace
                                };

            var normalColumns = from prop in classType.GetProperties()
                                let propertyType = prop.PropertyType
                                let columnAttribute = (ColumnAttribute)prop.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault()
                                where columnAttribute != null
                                select new NormalColumn (string.IsNullOrWhiteSpace(columnAttribute.ColumnName) ? prop.Name : columnAttribute.ColumnName, GetColumnType(columnAttribute, prop.PropertyType.Name), columnAttribute.Required)
                                {
                                    Length = columnAttribute.Length,
                                    Precision = columnAttribute.Precision
                                };

            return new TableColumnProperties
            {
                NormalColumns = normalColumns,
                PrimaryKeys = primaryKeys,
                UniqueColumns = uniqueColumns
            };
        }

        private string GetColumnType(ColumnAttribute columnAttribute, string nativeType)
        {
            if (columnAttribute is DateColumnAttribute)
            {
                return "Date";
            }

            if (columnAttribute is TimestampColumnAttribute)
            {
                return "TimeStamp";
            }

            return nativeType;
        }

        private static ClassDiagram ReadFile(string classDiagramLocation)
        {
            var serializer = new XmlSerializer(typeof(ClassDiagram));
            using (var fileStream = File.OpenRead(classDiagramLocation))
            {
                return (ClassDiagram)serializer.Deserialize(fileStream);
            }
        }
    }
}

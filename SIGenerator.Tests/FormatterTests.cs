using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGenerator.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Tests
{
    [TestClass]
    public class FormatterTests
    {
        [TestMethod]
        public void ColumnFormatter_Creates_Single_Int_Column_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> { new NormalColumn("Id", "Int32", true) }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    Id INT

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Creates_Single_String_Column_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> { new NormalColumn("Id", "String", true) { Length = 10 } }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    Id CHAR(10)

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Creates_Single_Nullable_String_Column_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> { new NormalColumn("TmStamp", "TimeStamp", false) }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    TmStamp TIMESTAMP NULL

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Creates_Single_Nullable_TimeStamp_Column_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> { new NormalColumn("TmStamp", "TimeStamp", false) }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    TmStamp TIMESTAMP NULL

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Creates_Single_Nullable_Date_Column_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> { new NormalColumn("DateColumn", "Date", false) }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    DateColumn DATE NULL

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Creates_Single_Nullable_Decimal_Column_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> { new NormalColumn("InterestRate", "Decimal", false) }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    InterestRate FLOAT(15, 2) NULL

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Creates_Single_Nullable_Decimal_With_Custom_Precision_Column_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> { new NormalColumn("InterestRate", "Decimal", false) { Precision = 13 } }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    InterestRate FLOAT(15, 13) NULL

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Creates_Single_Nullable_Decimal_With_Custom_Length_Column_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> { new NormalColumn("InterestRate", "Decimal", false) { Length = 99 } }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    InterestRate FLOAT(99, 2) NULL

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Creates_Single_Nullable_Decimal_With_Custom_Length_And_Precision_Column_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> { new NormalColumn("InterestRate", "Decimal", false) { Length = 99, Precision = 6 } }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    InterestRate FLOAT(99, 6) NULL

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Creates_Multiple_Int_Columns_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> {
                        new NormalColumn("Id", "Int32", true),
                        new NormalColumn("Age", "Int32", true),
                    }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    Id INT
    Age INT

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Creates_Multiple_Int_Columns_With_One_Nullable_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> {
                        new NormalColumn("Id", "Int32", true),
                        new NormalColumn("Age", "Int32", false),
                    }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    Id INT
    Age INT NULL

", columnDefinition);
        }

        [TestMethod]
        public void ColumnFormatter_Formats_Complex_Object_Correctly()
        {
            // Arrange
            var summary = new ClassSummary
            {
                Columns = new TableColumnProperties
                {
                    NormalColumns = new List<NormalColumn> {
                        new NormalColumn("RecordLegId", "Int32", true),
                        new NormalColumn("ContactName", "String", false) { Length = 10 },
                        new NormalColumn("ContactSurname", "String", false) { Length = 10 },
                        new NormalColumn("EmailAddress", "String", false) { Length = 120 },
                        new NormalColumn("FaxNumber", "String", false) { Length = 15 },
                        new NormalColumn("TelephoneNumber", "String", false) { Length = 15 },
                        new NormalColumn("PhysicalAddressId", "Int32", false),
                        new NormalColumn("PostalAddressId", "Int32", false)
                    }
                }
            };

            var formatter = new StandardSIFormatter(summary);

            // Act
            var columnDefinition = formatter.Columns();

            // Assert
            Assert.AreEqual(@"    RecordLegId INT
    ContactName CHAR(10) NULL
    ContactSurname CHAR(10) NULL
    EmailAddress CHAR(120) NULL
    FaxNumber CHAR(15) NULL
    TelephoneNumber CHAR(15) NULL
    PhysicalAddressId INT NULL
    PostalAddressId INT NULL

", columnDefinition);
        }
    }
}
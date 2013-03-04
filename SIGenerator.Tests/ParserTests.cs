using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace SIGenerator.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Specifying_Connect_Without_Server_Renders_Without_Server_Heading()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\TestConnectNoServer.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.StartsWith("CONNECT fsrvd00/f1nsur3")));
        }

        [TestMethod]
        public void Parser_Generates_ServerName_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\test.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.StartsWith("SERVER @dn74")));
        }

        [TestMethod]
        public void Parser_Generates_Connect_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\test.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.Contains("CONNECT fsrvd00/f1nsur3")));
        }

        [TestMethod]
        public void Parser_Generates_TableSpace_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\test.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.Contains("TABLESPACE tstSpace")));
        }

        [TestMethod]
        public void Parser_Generates_TableName_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\test.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.Contains("TABLE Test")));
        }

        [TestMethod]
        public void Parser_Generates_SinglePrimaryKey_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\test.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.Contains(@"KEY pk01 PRIMARY
    Id")));
        }

        [TestMethod]
        public void Parser_Generates_SinglePrimaryKey_WithTableSpace_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\testKeyWithTableSpace.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.Contains(@"KEY pk01 PRIMARY TABLESPACE KEYSPACE
    Id")));
        }

        [TestMethod]
        public void Parser_Generates_MultiplePrimaryKeys_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\TestMultiplePrimaryKeysSameTableSpace.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.Contains(@"KEY pk01 PRIMARY
    Id
    Description")));
        }

        [TestMethod]
        public void Parser_Generates_Grant_Statements_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\TestMultiplePrimaryKeysSameTableSpace.cd");


            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.Contains(@"GRANT SELECT INSERT DELETE UPDATE TO PUBLIC

PROC Insert
PROC Update
PROC SelectOne
PROC DeleteOne
PROC Exists
PROC SelectAll")));
        }

        [TestMethod]
        public void Parser_Generates_NoPrimaryKeys_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();
            var regexMatch = new Regex("KEY(.+)PRIMARY", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\TestNoPrimaryKey.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => !regexMatch.IsMatch(kv.Value)));
        }

        [TestMethod]
        public void Parser_Generates_SingleUniqueConstraint_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\TestWithUniqueConstraint.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.Contains(@"KEY key01 UNIQUE
    Gender")));
        }

        [TestMethod]
        public void Parser_Generates_MultipleUniqueConstraint_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\TestWithMultipleUniqueConstraint.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.Contains(@"KEY key01 UNIQUE
    Gender

KEY key02 UNIQUE
    Age")));
        }

        [TestMethod]
        public void Parser_Generates_ComplexClass_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\AccountHolderDiagram.cd");

            // Assert
            Assert.IsTrue(siContent.All(kv => kv.Value.Equals(@"SERVER @dn74
CONNECT fsrvd00/f1nsur3

TABLESPACE fsrvt001

TABLE PL_AccountHolders
    RecordLegId INT
    ContactName CHAR(10) NULL
    ContactSurname CHAR(10) NULL
    EmailAddress CHAR(120) NULL
    FaxNumber CHAR(15) NULL
    TelephoneNumber CHAR(15) NULL
    PhysicalAddressId INT NULL
    PostalAddressId INT NULL

KEY pk01 PRIMARY
    RecordLegId

KEY key01 UNIQUE
    PhysicalAddressId

KEY key02 UNIQUE
    PostalAddressId

GRANT SELECT INSERT DELETE UPDATE TO PUBLIC

PROC Insert
PROC Update
PROC SelectOne
PROC DeleteOne
PROC Exists
PROC SelectAll")));
        }

        [TestMethod]
        public void Parser_Generates_ComplexClasses_For_Multiple_Class_Diagram_Correctly()
        {
            // Arrange
            var parser = new SIGenerator.Parser.Parser();

            // Act
            var siContent = parser.Parse(@"..\..\TestClassDiagrams\MultipleClassesDiagram.cd");

            // Assert
            Assert.AreEqual(2, siContent.Count());

            Assert.AreEqual(@"SERVER @dn74
CONNECT fsrvd00/f1nsur3

TABLESPACE fsrvt001

TABLE PL_AccountHolders
    RecordLegId INT
    ContactName CHAR(10) NULL
    ContactSurname CHAR(10) NULL
    EmailAddress CHAR(120) NULL
    FaxNumber CHAR(15) NULL
    TelephoneNumber CHAR(15) NULL
    PhysicalAddressId INT NULL
    PostalAddressId INT NULL

KEY pk01 PRIMARY
    RecordLegId

KEY key01 UNIQUE
    PhysicalAddressId

KEY key02 UNIQUE
    PostalAddressId

GRANT SELECT INSERT DELETE UPDATE TO PUBLIC

PROC Insert
PROC Update
PROC SelectOne
PROC DeleteOne
PROC Exists
PROC SelectAll", siContent["AccountHolder"]);

            Assert.AreEqual(@"SERVER @dn74
CONNECT fsrvd00/f1nsur3

TABLESPACE fsrvt001

TABLE PL_UserDetails
    Id INT
    Username CHAR(15)

KEY pk01 PRIMARY
    Id


GRANT SELECT INSERT DELETE UPDATE TO PUBLIC

PROC Insert
PROC Update
PROC SelectOne
PROC DeleteOne
PROC Exists
PROC SelectAll", siContent["UserDetails"]);
        }
    }
}

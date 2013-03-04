using SIGenerator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Tests.TestClassDiagrams
{
    [Table(TableName="Test", TableSpace="tstSpace", Connect="fsrvd00/f1nsur3", Server="@dn74")]
    public class TestTableClassWithKeyTableSpace
    {
        [Key(TableSpace="KEYSPACE")]
        public int Id { get; set; }

        [Column(true, Length = 40)]
        public string Description { get; set; }
    }
}
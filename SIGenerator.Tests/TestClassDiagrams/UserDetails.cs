using SIGenerator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Tests.TestClassDiagrams
{
    [Table(TableName = "PL_UserDetails", TableSpace = "fsrvt001", Connect = "fsrvd00/f1nsur3", Server = "@dn74")]
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }

        [Column(true, Length=15)]
        public string Username { get; set; }
    }
}
using SIGenerator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Tests.TestClassDiagrams
{
    [Table(TableName = "PL_AccountHolders", TableSpace = "fsrvt001", Connect = "fsrvd00/f1nsur3", Server = "@dn74")]    
    public class AccountHolder
    {
        [Key]
        public int RecordLegId { get; set; }

        [Column(false, Length = 10)]
        public string ContactName { get; set; }

        [Column(false, Length = 10)]
        public string ContactSurname { get; set; }

        [Column(false, Length = 120)]
        public string EmailAddress { get; set; }

        [Column(false, Length = 15)]
        public string FaxNumber { get; set; }

        [Column(false, Length = 15)]
        public string TelephoneNumber { get; set; }

        [Unique(false)]
        public int PhysicalAddressId { get; set; }

        [Unique(false)]
        public int PostalAddressId { get; set; }
    }
}
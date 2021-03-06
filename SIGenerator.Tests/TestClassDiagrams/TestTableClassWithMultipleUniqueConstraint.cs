﻿using SIGenerator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Tests.TestClassDiagrams
{
    [Table(TableName="Test", TableSpace="tstSpace", Connect="fsrvd00/f1nsur3", Server="@dn74")]
    public class TestTableClassWithMultipleUniqueConstraint
    {
        [Key]
        public int Id { get; set; }

        [Unique(false)]
        public bool Gender { get; set; }

        [Unique(false)]
        public int Age { get; set; }

        [Column(true, Length = 40)]
        public string Description { get; set; }
    }
}
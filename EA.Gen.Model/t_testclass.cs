namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_testclass")]
    public partial class TestClass
    {
        [Key]
        [StringLength(50)]
        [Column("TestClass")]
        public string Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}

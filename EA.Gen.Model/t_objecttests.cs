namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_objecttests")]
    public partial class ObjectTest
    {
        [Key]
        [Column("Object_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Test { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestClass { get; set; }

        [StringLength(50)]
        public string TestType { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [Column(TypeName = "ntext")]
        public string InputData { get; set; }

        [Column(TypeName = "ntext")]
        public string AcceptanceCriteria { get; set; }

        [StringLength(32)]
        public string Status { get; set; }

        public DateTime? DateRun { get; set; }

        [Column(TypeName = "ntext")]
        public string Results { get; set; }

        [StringLength(255)]
        public string RunBy { get; set; }

        [StringLength(255)]
        public string CheckBy { get; set; }

        public virtual Element Element { get; set; }
    }
}

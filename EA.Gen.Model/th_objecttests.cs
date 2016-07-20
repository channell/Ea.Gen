namespace Cephei.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class th_objecttests
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Obje.ID { get; set; }

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

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string User_Id { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime Change_Date { get; set; }
    }
}

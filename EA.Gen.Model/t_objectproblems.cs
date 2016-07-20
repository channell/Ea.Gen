namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_objectproblems")]
    public partial class ObjectProblem
    {
        [Key]
        [Column("Object_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Problem { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string ProblemType { get; set; }

        public DateTime? DateReported { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "ntext")]
        public string ProblemNotes { get; set; }

        [StringLength(255)]
        public string ReportedBy { get; set; }

        [StringLength(255)]
        public string ResolvedBy { get; set; }

        public DateTime? DateResolved { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [Column(TypeName = "ntext")]
        public string ResolverNotes { get; set; }

        [StringLength(50)]
        public string Priority { get; set; }

        [StringLength(50)]
        public string Severity { get; set; }

        public virtual Element Element { get; set; }
    }
}

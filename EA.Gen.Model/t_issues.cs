namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_issues")]
    public partial class Issue
    {
        [StringLength(255)]
        [Column("Issue")]
        public string IssueCode { get; set; }

        public DateTime? IssueDate { get; set; }

        [StringLength(255)]
        public string Owner { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(255)]
        public string Resolver { get; set; }

        public DateTime? DateResolved { get; set; }

        [Column(TypeName = "ntext")]
        public string Resolution { get; set; }

        [Key]
        public int IssueID { get; set; }

        [StringLength(255)]
        public string Category { get; set; }

        [StringLength(50)]
        public string Priority { get; set; }

        [StringLength(50)]
        public string Severity { get; set; }

        [StringLength(100)]
        public string IssueType { get; set; }
    }
}

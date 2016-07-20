namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_rules")]
    public partial class Rule
    {
        [Key]
        [StringLength(50)]
        [Column("RuleID")]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string RuleName { get; set; }

        [Required]
        [StringLength(255)]
        public string RuleType { get; set; }

        public int? RuleActive { get; set; }

        [StringLength(255)]
        public string ErrorMsg { get; set; }

        [StringLength(255)]
        public string Flags { get; set; }

        [Column(TypeName = "ntext")]
        public string RuleOCL { get; set; }

        [Column(TypeName = "ntext")]
        public string RuleXML { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
    }
}

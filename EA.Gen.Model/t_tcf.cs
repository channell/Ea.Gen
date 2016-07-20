namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_tcf")]
    public partial class tcf
    {
        [Key]
        [StringLength(12)]
        [Column("TCFID")]
        public string Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public double? Weight { get; set; }

        public double? Value { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }
    }
}

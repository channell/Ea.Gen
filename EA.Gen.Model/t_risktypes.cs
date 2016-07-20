namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_risktypes")]
    public partial class RiskType
    {
        [Key]
        [StringLength(12)]
        public string Risk { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public double? NumericWeight { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }
    }
}

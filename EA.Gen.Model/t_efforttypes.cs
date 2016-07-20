namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_efforttypes")]
    public partial class EffortType
    {
        [Key]
        [StringLength(12)]
        [Column("EffortType")]
        public string EffortTypeCode { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public double? NumericWeight { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }
    }
}

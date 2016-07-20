namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_trxtypes")]
    public partial class TrxType
    {
        [StringLength(50)]
        public string Description { get; set; }

        public double? NumericWeight { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(255)]
        public string TRX { get; set; }

        [Key]
        [Column("TRX_ID")]
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        public string Style { get; set; }
    }
}

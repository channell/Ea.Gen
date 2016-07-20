namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_cardinality")]
    public partial class Cardinality
    {
        [Key]
        [StringLength(50)]
        [Column("Cardinality")]
        public string CardinalityCode { get; set; }
    }
}

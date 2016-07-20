namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_constrainttypes")]
    public partial class CnstraintTypes
    {
        [Key]
        [StringLength(16)]
        public string Constraint { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
    }
}

namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_objectconstraint")]
    public partial class ObjectConstraint
    {
        [Key]
        [Column("Object_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Constraint { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string ConstraintType { get; set; }

        public double? Weight { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public virtual Element Element { get; set; }
    }
}

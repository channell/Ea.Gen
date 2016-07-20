namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_connectorconstraint")]
    public partial class ConnectorConstraint
    {
        [Key]
        [Column("ConnectorID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Constraint { get; set; }

        [StringLength(50)]
        public string ConstraintType { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
    }
}

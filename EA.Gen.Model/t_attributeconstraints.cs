namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_attributeconstraints")]
    public partial class AttributeConstraint
    {
        [Column ("Object_ID")]
        public int? ObjectId { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string Constraint { get; set; }

        [StringLength(255)]
        public string AttName { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [Key]
        [Column("ID", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public virtual Attribute Attribute { get; set; }
    }
}

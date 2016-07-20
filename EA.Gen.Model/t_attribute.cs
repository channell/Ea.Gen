namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_attribute")]
    public partial class Attribute
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attribute()
        {
            Constraints = new HashSet<AttributeConstraint>();
        }
        [Column("Object_ID")]
        public int? ObjectId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Scope { get; set; }

        [StringLength(50)]
        public string Stereotype { get; set; }

        [StringLength(50)]
        public string Containment { get; set; }

        public int? IsStatic { get; set; }

        public int? IsCollection { get; set; }

        public int? IsOrdered { get; set; }

        public int? AllowDuplicates { get; set; }

        [StringLength(50)]
        public string LowerBound { get; set; }

        [StringLength(50)]
        public string UpperBound { get; set; }

        [StringLength(50)]
        public string Container { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(1)]
        public string Derived { get; set; }

        [Key]
        public int ID { get; set; }

        public int? Pos { get; set; }

        [Column(TypeName = "ntext")]
        public string GenOption { get; set; }

        public int? Length { get; set; }

        public int? Precision { get; set; }

        public int? Scale { get; set; }

        public int? Const { get; set; }

        [StringLength(255)]
        public string Style { get; set; }

        [StringLength(50)]
        public string Classifier { get; set; }

        [Column(TypeName = "ntext")]
        public string Default { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [StringLength(50)]
        public string ea_guid { get; set; }

        [Column(TypeName = "ntext")]
        public string StyleEx { get; set; }

        public virtual Element Element { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttributeConstraint> Constraints { get; set; }
    }
}

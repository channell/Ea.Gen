namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_operation")]
    public partial class Operation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Operation()
        {
            operationparams = new HashSet<OperationParam>();
            operationposts = new HashSet<OperationPost>();
            operationpres = new HashSet<OperationPrecondition>();
        }

        [Key]
        [Column("OperationID")]
        public int Id { get; set; }

        [Column("Object_ID")]
        public int? ElementId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Scope { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [StringLength(1)]
        public string ReturnArray { get; set; }

        [StringLength(50)]
        public string Stereotype { get; set; }

        [StringLength(1)]
        public string IsStatic { get; set; }

        [StringLength(50)]
        public string Concurrency { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [Column(TypeName = "ntext")]
        public string Behaviour { get; set; }

        [StringLength(1)]
        public string Abstract { get; set; }

        [Column(TypeName = "ntext")]
        public string GenOption { get; set; }

        [StringLength(1)]
        public string Synchronized { get; set; }

        public int? Pos { get; set; }

        public int? Const { get; set; }

        [StringLength(255)]
        public string Style { get; set; }

        public int Pure { get; set; }

        [StringLength(255)]
        public string Throws { get; set; }

        [StringLength(50)]
        public string Classifier { get; set; }

        [Column(TypeName = "ntext")]
        public string Code { get; set; }

        public int IsRoot { get; set; }

        public int IsLeaf { get; set; }

        public int IsQuery { get; set; }

        [StringLength(255)]
        public string StateFlags { get; set; }

        [StringLength(50)]
        public string ea_guid { get; set; }

        [Column(TypeName = "ntext")]
        public string StyleEx { get; set; }

        public virtual Element Element { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperationParam> operationparams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperationPost> operationposts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperationPrecondition> operationpres { get; set; }
    }
}

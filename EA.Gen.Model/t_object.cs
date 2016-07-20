namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_object")]
    public partial class Element
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Element()
        {
            Attributes = new HashSet<Attribute>();
            StartConnectors = new HashSet<Connector>();
            EndConnectors = new HashSet<Connector>();
            Constraint = new HashSet<ObjectConstraint>();
            Efforts = new HashSet<ObjectEffort>();
            Files = new HashSet<ObjectFile>();
            Metrics = new HashSet<ObjectMetric>();
            Problems = new HashSet<ObjectProblem>();
            Resource = new HashSet<ObjectResource>();
            Risks = new HashSet<ObjectRisk>();
            Tests = new HashSet<ObjectTest>();
            Trxs = new HashSet<ObjectTrx>();
            Operations = new HashSet<Operation>();
            Properties = new HashSet<ObjectProperty>();
        }

        [Key]
        [Column("Object_ID")]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("Object_Type")]
        public string ObjectType { get; set; }

        [Column("Diagram_ID")]
        public int? DiagramId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Alias { get; set; }

        [StringLength(255)]
        public string Author { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [Column(TypeName = "ntext")]
        public string Note { get; set; }

        [Column("Package_ID")]
        public int? PackageId { get; set; }

        [StringLength(255)]
        public string Stereotype { get; set; }

        public int? NType { get; set; }

        [StringLength(50)]
        public string Complexity { get; set; }

        public int? Effort { get; set; }

        [StringLength(255)]
        public string Style { get; set; }

        public int? Backcolor { get; set; }

        public int? BorderStyle { get; set; }

        public int? BorderWidth { get; set; }

        public int? Fontcolor { get; set; }

        public int? Bordercolor { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(1)]
        public string Abstract { get; set; }

        public int? Tagged { get; set; }

        [StringLength(255)]
        public string PDATA1 { get; set; }

        [Column(TypeName = "ntext")]
        public string PDATA2 { get; set; }

        [Column(TypeName = "ntext")]
        public string PDATA3 { get; set; }

        [Column(TypeName = "ntext")]
        public string PDATA4 { get; set; }

        [StringLength(255)]
        public string PDATA5 { get; set; }

        [StringLength(50)]
        public string Concurrency { get; set; }

        [StringLength(50)]
        public string Visibility { get; set; }

        [StringLength(50)]
        public string Persistence { get; set; }

        [StringLength(50)]
        public string Cardinality { get; set; }

        [StringLength(50)]
        public string GenType { get; set; }

        [StringLength(255)]
        public string GenFile { get; set; }

        [Column(TypeName = "ntext")]
        public string Header1 { get; set; }

        [Column(TypeName = "ntext")]
        public string Header2 { get; set; }

        [StringLength(50)]
        public string Phase { get; set; }

        [StringLength(25)]
        public string Scope { get; set; }

        [Column(TypeName = "ntext")]
        public string GenOption { get; set; }

        [Column(TypeName = "ntext")]
        public string GenLinks { get; set; }

        public int? Classifier { get; set; }

        [StringLength(40)]
        public string ea_guid { get; set; }

        public int? ParentID { get; set; }

        [Column(TypeName = "ntext")]
        public string RunState { get; set; }

        [StringLength(40)]
        public string Classifier_guid { get; set; }

        public int? TPos { get; set; }

        public int IsRoot { get; set; }

        public int IsLeaf { get; set; }

        public int IsSpec { get; set; }

        public int IsActive { get; set; }

        [StringLength(255)]
        public string StateFlags { get; set; }

        [StringLength(255)]
        public string PackageFlags { get; set; }

        [StringLength(50)]
        public string Multiplicity { get; set; }

        [Column(TypeName = "ntext")]
        public string StyleEx { get; set; }

        [Column(TypeName = "ntext")]
        public string EventFlags { get; set; }

        [StringLength(255)]
        public string ActionFlags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attribute> Attributes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Connector> StartConnectors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Connector> EndConnectors { get; set; }

        public virtual Package package { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectConstraint> Constraint { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectEffort> Efforts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectFile> Files { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectMetric> Metrics { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectProblem> Problems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectResource> Resource { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectRisk> Risks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectTest> Tests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectTrx> Trxs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operation> Operations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectProperty> Properties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiagramObject> DiagramObjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Element> Children { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Element Parent { get; set; }

    }
    public static class ElementExtension
    {
        public static string TableSpace(this Element e)
        {
            if (e.ObjectType == "Class" && e.Stereotype == "table")
                return e.PDATA1;
            else
                return null;
        }
        public static void SetTableSpace(this Element e, string val)
        {
            if (e.ObjectType == "Class" && e.Stereotype == "table")
                e.PDATA1 = val;
            else
                throw new InvalidCastException("Element is not a table");
        }


    }

}

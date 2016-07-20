namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_package")]
    public partial class Package
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Package()
        {
            Diagrams = new HashSet<Diagram>();
            DiagramTypes = new HashSet<DiagramType>();
            Elements = new HashSet<Element>();
            Children = new HashSet<Package>();
        }

        [Key]
        [Column("Package_ID")]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [Column("Parent_ID")]
        public int? ParentId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(40)]
        public string ea_guid { get; set; }

        [StringLength(255)]
        public string XMLPath { get; set; }

        public int IsControlled { get; set; }

        public DateTime? LastLoadDate { get; set; }

        public DateTime? LastSaveDate { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        public int Protected { get; set; }

        [StringLength(255)]
        public string PkgOwner { get; set; }

        [StringLength(50)]
        public string UMLVersion { get; set; }

        public int UseDTD { get; set; }

        public int LogXML { get; set; }

        [StringLength(255)]
        public string CodePath { get; set; }

        [StringLength(50)]
        public string Namespace { get; set; }

        public int? TPos { get; set; }

        [StringLength(255)]
        public string PackageFlags { get; set; }

        public int? BatchSave { get; set; }

        public int? BatchLoad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diagram> Diagrams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiagramType> DiagramTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Element> Elements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Package> Children{ get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Package Parent { get; set; }
    }
}

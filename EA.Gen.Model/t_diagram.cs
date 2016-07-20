namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_diagram")]
    public partial class Diagram
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Diagram()
        {
            Elements = new HashSet<DiagramObject>();
            Links = new HashSet<DiagramLink>();
        }

        [Key]
        [Column("Diagram_ID")]
        public int DiagramId { get; set; }

        [Column("Package_ID")]
        public int? PackageId { get; set; }

        public int? ParentID { get; set; }

        [StringLength(50)]
        [Column("Diagram_Type")]
        public string DiagramTypeCode { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [StringLength(255)]
        public string Author { get; set; }

        public int? ShowDetails { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(50)]
        public string Stereotype { get; set; }

        public int AttPub { get; set; }

        public int AttPri { get; set; }

        public int AttPro { get; set; }

        [StringLength(1)]
        public string Orientation { get; set; }

        public int? cx { get; set; }

        public int? cy { get; set; }

        public int? Scale { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(255)]
        public string HTMLPath { get; set; }

        public int ShowForeign { get; set; }

        public int ShowBorder { get; set; }

        public int ShowPackageContents { get; set; }

        [StringLength(255)]
        public string PDATA { get; set; }

        public int Locked { get; set; }

        [StringLength(40)]
        public string ea_guid { get; set; }

        public int? TPos { get; set; }

        [StringLength(255)]
        public string Swimlanes { get; set; }

        [Column(TypeName = "ntext")]
        public string StyleEx { get; set; }

        public virtual Package Package { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiagramObject> Elements { get; set; }
        public virtual ICollection<DiagramLink> Links { get; set; }

    }
}

namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_diagramtypes")]
    public partial class DiagramType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiagramType()
        {
            diagram = new HashSet<Diagram>();
        }

        [Key]
        [StringLength(50)]
        [Column("Diagram_Type")]
        public string DiagramTypeCode { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? Package_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diagram> diagram { get; set; }

        public virtual Package package { get; set; }
    }
}

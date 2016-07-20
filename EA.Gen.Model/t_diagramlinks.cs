namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_diagramlinks")]
    public partial class DiagramLink
    {
        public int? DiagramID { get; set; }

        public int? ConnectorID { get; set; }

        [Column(TypeName = "ntext")]
        public string Geometry { get; set; }

        [StringLength(255)]
        public string Style { get; set; }

        public int Hidden { get; set; }

        [StringLength(255)]
        public string Path { get; set; }

        [Key]
        [Column("Instance_ID")]
        public int Id { get; set; }
        public virtual Diagram Diagram { get; set; }
    }
}

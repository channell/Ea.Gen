namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_diagramobjects")]
    public partial class DiagramObject
    {
        [Column("Diagram_ID")]
        public int? DiagramId { get; set; }

        [Column("Object_ID")]
        public int? ObjectId { get; set; }

        public int? RectTop { get; set; }

        public int? RectLeft { get; set; }

        public int? RectRight { get; set; }

        public int? RectBottom { get; set; }

        public int? Sequence { get; set; }

        [StringLength(255)]
        public string ObjectStyle { get; set; }

        [Key]
        public int Instance_ID { get; set; }

        public virtual Diagram Diagram { get; set; }

        public virtual Element Element { get; set; }

        public static implicit operator Element(DiagramObject o)
        {
            return o.Element ;
        }
    }
}

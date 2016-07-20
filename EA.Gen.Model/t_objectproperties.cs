namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_objectproperties")]
    public partial class ObjectProperty
    {
        [Key]
        [Column("PropertyID")]
        public int Id { get; set; }

        [Column("Object_ID")]
        public int? ElementId { get; set; }

        [StringLength(255)]
        public string Property { get; set; }

        [StringLength(255)]
        public string Value { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(40)]
        public string ea_guid { get; set; }

        public virtual Element Element { get; set; }
    }
}

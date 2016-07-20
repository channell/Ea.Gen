namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_stereotypes")]
    public partial class StereoType
    {
        [Key]
        [Column("Stereotype", Order = 0)]
        [StringLength(255)]
        public string Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string AppliesTo { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int MFEnabled { get; set; }

        [StringLength(255)]
        public string MFPath { get; set; }

        [Column(TypeName = "image")]
        public byte[] Metafile { get; set; }

        [Column(TypeName = "ntext")]
        public string Style { get; set; }

        [StringLength(50)]
        public string ea_guid { get; set; }

        [StringLength(100)]
        public string VisualType { get; set; }
    }
}

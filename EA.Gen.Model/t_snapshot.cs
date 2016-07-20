namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_snapshot")]
    public partial class Snapshot
    {
        [Key]
        [StringLength(40)]
        public string SnapshotID { get; set; }

        [StringLength(40)]
        public string SeriesID { get; set; }

        public int Position { get; set; }

        [Required]
        [StringLength(100)]
        public string SnapshotName { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(255)]
        public string Style { get; set; }

        [StringLength(40)]
        public string ElementID { get; set; }

        [Required]
        [StringLength(50)]
        public string ElementType { get; set; }

        [Column(TypeName = "ntext")]
        public string StrContent { get; set; }

        [Column(TypeName = "image")]
        public byte[] BinContent1 { get; set; }

        [Column(TypeName = "image")]
        public byte[] BinContent2 { get; set; }
    }
}

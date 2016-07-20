namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_files")]
    public partial class File
    {
        [Key]
        [StringLength(50)]
        [Column("FileID")]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string AppliesTo { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public DateTime? FileDate { get; set; }

        public int? FileSize { get; set; }
    }
}

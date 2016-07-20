namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_document")]
    public partial class Document
    {
        [Key]
        [StringLength(40)]
        [Column("DocID")]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DocName { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(255)]
        public string Style { get; set; }

        [Required]
        [StringLength(40)]
        public string ElementID { get; set; }

        [Required]
        [StringLength(50)]
        public string ElementType { get; set; }

        [Column(TypeName = "ntext")]
        public string StrContent { get; set; }

        [Column(TypeName = "image")]
        public byte[] BinContent { get; set; }

        [StringLength(100)]
        public string DocType { get; set; }

        [StringLength(255)]
        public string Author { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        public int? IsActive { get; set; }

        public int? Sequence { get; set; }

        public DateTime? DocDate { get; set; }
    }
}

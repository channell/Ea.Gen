namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_image")]
    public partial class Image
    {
        [Key]
        [Column("ImageID")]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [Column(TypeName = "image")]
        public byte[] ImageBytes { get; set; }
    }
}

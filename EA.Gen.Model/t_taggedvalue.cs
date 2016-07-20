namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_taggedvalue")]
    public partial class TaggedValue
    {
        [Key]
        [StringLength(40)]
        [Column("PropertyID")]
        public string Id { get; set; }

        [StringLength(40)]
        public string ElementID { get; set; }

        [Required]
        [StringLength(100)]
        public string BaseClass { get; set; }

        [Column(TypeName = "ntext")]
        public string TagValue { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
    }
}

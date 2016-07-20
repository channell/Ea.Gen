namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_lists")]
    public partial class List
    {
        [Key]
        [StringLength(50)]
        public string ListID { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int? NVal { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
    }
}

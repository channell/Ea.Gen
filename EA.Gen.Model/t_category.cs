namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_category")]
    public partial class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [Column("NOTES", TypeName = "ntext")]
        public string Notes { get; set; }
    }
}

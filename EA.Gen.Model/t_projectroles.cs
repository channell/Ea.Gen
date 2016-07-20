namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_projectroles")]
    public partial class ProjectRole
    {
        [Key]
        [StringLength(255)]
        public string Role { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
    }
}

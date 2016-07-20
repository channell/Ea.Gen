namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_authors")]
    public partial class Author
    {
        [Key]
        [StringLength(255)]
        public string AuthorName { get; set; }

        [StringLength(255)]
        public string Roles { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }
    }
}

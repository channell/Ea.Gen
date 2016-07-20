namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_glossary")]
    public partial class Glossary
    {
        [StringLength(255)]
        public string Term { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [Column(TypeName = "ntext")]
        public string Meaning { get; set; }

        [Key]
        public int GlossaryID { get; set; }
    }
}

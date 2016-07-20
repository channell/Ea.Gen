namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_script")]
    public partial class Script
    {
        [Key]
        [Column("ScriptID")]
        public int Id { get; set; }

        [StringLength(100)]
        public string ScriptCategory { get; set; }

        [StringLength(150)]
        public string ScriptName { get; set; }

        [StringLength(255)]
        public string ScriptAuthor { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [Column("script", TypeName = "ntext")]
        public string ScriptCode { get; set; }
    }
}

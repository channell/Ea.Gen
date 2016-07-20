namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_umlpattern")]
    public partial class UMLPattern
    {
        [Key]
        [Column("PatternID")]
        public int Id { get; set; }

        [StringLength(100)]
        public string PatternCategory { get; set; }

        [StringLength(150)]
        public string PatternName { get; set; }

        [StringLength(250)]
        public string Style { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [Column(TypeName = "ntext")]
        public string PatternXML { get; set; }

        [StringLength(50)]
        public string Version { get; set; }
    }
}

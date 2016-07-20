namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_version")]
    public partial class Version
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string ElementID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string VersionID { get; set; }

        [StringLength(100)]
        public string ElementType { get; set; }

        [StringLength(255)]
        public string Flags { get; set; }

        [StringLength(255)]
        public string ExternalFile { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(255)]
        public string Owner { get; set; }

        public DateTime? VersionDate { get; set; }

        [StringLength(255)]
        public string Branch { get; set; }

        [Column(TypeName = "ntext")]
        public string ElementXML { get; set; }
    }
}

namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_xrefuser")]
    public partial class XrefUser
    {
        [Key]
        [StringLength(255)]
        [Column("XrefID")]
        public string Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [StringLength(255)]
        public string Visibility { get; set; }

        [StringLength(255)]
        public string Namespace { get; set; }

        [StringLength(255)]
        public string Requirement { get; set; }

        [StringLength(255)]
        public string Constraint { get; set; }

        [StringLength(255)]
        public string Behavior { get; set; }

        [StringLength(255)]
        public string Partition { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(255)]
        public string Client { get; set; }

        [StringLength(255)]
        public string Supplier { get; set; }

        [StringLength(255)]
        public string Link { get; set; }

        [StringLength(50)]
        public string ToolID { get; set; }
    }
}

namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_connectortag")]
    public partial class ConnectorTag
    {
        [Key]
        [Column("PropertyID")]
        public int PropertyId { get; set; }

        [Column("ElementID")]
        public int? ElementId { get; set; }

        [StringLength(255)]
        public string Property { get; set; }

        [StringLength(255)]
        [Column("VALUE")]
        public string Value { get; set; }

        [Column("NOTES", TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(40)]
        public string ea_guid { get; set; }
    }
}

namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_operationtag")]
    public partial class OperationTag
    {
        [Key]
        [Column("PropertyID")]
        public int Id { get; set; }

        [Column("ElementID")]
        public int? ElementId { get; set; }

        [StringLength(255)]
        public string Property { get; set; }

        [StringLength(255)]
        public string VALUE { get; set; }

        [Column(TypeName = "ntext")]
        public string NOTES { get; set; }

        [StringLength(40)]
        public string ea_guid { get; set; }
    }
}

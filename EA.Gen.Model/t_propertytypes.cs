namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_propertytypes")]
    public partial class propertyType
    {
        [Key]
        [StringLength(50)]
        public string Property { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
    }
}

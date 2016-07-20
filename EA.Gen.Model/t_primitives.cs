namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_primitives")]
    public partial class primitives
    {
        [Key]
        [StringLength(50)]
        public string Datatype { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}

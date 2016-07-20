namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_constants")]
    public partial class Constant
    {
        [Key]
        [StringLength(50)]
        public string ConstantName { get; set; }

        [StringLength(255)]
        public string ConstantValue { get; set; }
    }
}

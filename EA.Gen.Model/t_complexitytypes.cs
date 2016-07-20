namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_complexitytypes")]
    public partial class ComplexityType
    {
        [Key]
        [StringLength(50)]
        public string Complexity { get; set; }

        public int? NumericWeight { get; set; }
    }
}

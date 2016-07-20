namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_secpolicies")]
    public partial class Policy
    {
        [Key]
        [StringLength(100)]
        public string Property { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }
    }
}

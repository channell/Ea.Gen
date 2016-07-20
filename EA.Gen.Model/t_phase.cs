namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_phase")]
    public partial class Phase
    {
        [Key]
        [StringLength(40)]
        [Column("PhaseID")]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string PhaseName { get; set; }

        [Column(TypeName = "ntext")]
        public string PhaseNotes { get; set; }

        [StringLength(255)]
        public string PhaseStyle { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Column(TypeName = "ntext")]
        public string PhaseContent { get; set; }
    }
}

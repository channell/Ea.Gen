namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_tasks")]
    public partial class Task
    {
        [Key]
        [Column("TaskID")]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string TaskType { get; set; }

        [Column(TypeName = "ntext")]
        public string NOTES { get; set; }

        [StringLength(255)]
        public string Priority { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [StringLength(255)]
        public string Owner { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(50)]
        public string Phase { get; set; }

        [Column(TypeName = "ntext")]
        public string History { get; set; }

        public int? Percent { get; set; }

        public int? TotalTime { get; set; }

        public int? ActualTime { get; set; }

        [StringLength(100)]
        public string AssignedTo { get; set; }
    }
}

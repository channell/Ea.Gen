namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_objectmetrics")]
    public partial class ObjectMetric
    {
        [Key]
        [Column("Object_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get;set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Metric { get; set; }

        [StringLength(12)]
        public string MetricType { get; set; }

        public double? EValue { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public virtual Element Element { get; set; }
    }
}

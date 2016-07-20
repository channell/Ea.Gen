namespace Cephei.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class th_objectresource
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Obje.ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Resource { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string Role { get; set; }

        public double? Time { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public short? PercentComplete { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        [Column(TypeName = "ntext")]
        public string History { get; set; }

        public int? ExpectedHours { get; set; }

        public int? ActualHours { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string User_Id { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime Change_Date { get; set; }
    }
}

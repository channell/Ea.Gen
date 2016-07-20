namespace Cephei.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class th_objecteffort
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Obje.ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Effort { get; set; }

        [StringLength(12)]
        public string EffortType { get; set; }

        public double? EValue { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string User_Id { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Change_Date { get; set; }
    }
}

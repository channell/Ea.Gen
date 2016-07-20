namespace Cephei.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class th_objectrequires
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReqID { get; set; }

        public int? Obje.ID { get; set; }

        [StringLength(255)]
        public string Requirement { get; set; }

        [StringLength(255)]
        public string ReqType { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(50)]
        public string Stability { get; set; }

        [StringLength(50)]
        public string Difficulty { get; set; }

        [StringLength(50)]
        public string Priority { get; set; }

        public DateTime? LastUpdate { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string User_Id { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime Change_Date { get; set; }
    }
}

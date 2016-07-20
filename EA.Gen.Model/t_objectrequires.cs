namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_objectrequires")]
    public partial class ObjecRequire
    {
        [Key]
        [Column("ReqID")]
        public int Id { get; set; }

        [Column("Object_ID")]
        public int? ElementId { get; set; }

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
    }
}

namespace Cephei.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class th_objectproperties
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PropertyID { get; set; }

        public int? Obje.ID { get; set; }

        [StringLength(255)]
        public string Property { get; set; }

        [StringLength(255)]
        public string Value { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(40)]
        public string ea_guid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string User_Id { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime Change_Date { get; set; }
    }
}

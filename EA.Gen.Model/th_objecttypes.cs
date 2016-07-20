namespace Cephei.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class th_objecttypes
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Obje.Type { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DesignObject { get; set; }

        public int? ImageID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string User_Id { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Change_Date { get; set; }
    }
}

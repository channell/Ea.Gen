namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_objecttrx")]
    public partial class ObjectTrx
    {
        [Key]
        [Column("Object_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string TRX { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(12)]
        public string TRXType { get; set; }

        public double? Weight { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public virtual Element Element { get; set; }
    }
}

namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_datatypes")]
    public partial class DataType
    {
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(50)]
        [Column("DataType")]
        public string DataTypeCode { get; set; }

        public int? Size { get; set; }

        public int? MaxLen { get; set; }

        public int? MaxPrec { get; set; }

        public int? MaxScale { get; set; }

        public int? DefaultLen { get; set; }

        public int? DefaultPrec { get; set; }

        public int? DefaultScale { get; set; }

        public int? User { get; set; }

        [StringLength(255)]
        public string PDATA1 { get; set; }

        [StringLength(255)]
        public string PDATA2 { get; set; }

        [StringLength(255)]
        public string PDATA3 { get; set; }

        [StringLength(255)]
        public string PDATA4 { get; set; }

        [StringLength(50)]
        public string HasLength { get; set; }

        [StringLength(255)]
        public string GenericType { get; set; }

        [Key]
        [Column("DatatypeID")]
        public int Id { get; set; }
    }
}

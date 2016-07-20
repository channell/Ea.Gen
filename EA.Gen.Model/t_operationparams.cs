namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_operationparams")]
    public partial class OperationParam
    {
        [Key]
        [Column("OperationID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [StringLength(255)]
        public string Default { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public int? Pos { get; set; }

        public int Const { get; set; }

        [StringLength(255)]
        public string Style { get; set; }

        [StringLength(12)]
        public string Kind { get; set; }

        [StringLength(50)]
        public string Classifier { get; set; }

        [StringLength(50)]
        public string ea_guid { get; set; }

        [Column(TypeName = "ntext")]
        public string StyleEx { get; set; }

        public virtual Operation operation { get; set; }
    }
}

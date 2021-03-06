namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_operationposts")]
    public partial class OperationPost
    {
        [Key]
        [Column("OperationID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string PostCondition { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public virtual Operation operation { get; set; }
    }
}

namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_objectfiles")]
    public partial class ObjectFile
    {
        [Key]
        [Column("Object_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [Column(TypeName = "ntext")]
        public string Note { get; set; }

        [StringLength(255)]
        public string FileSize { get; set; }

        [StringLength(255)]
        public string FileDate { get; set; }

        public virtual Element Element { get; set; }
    }
}

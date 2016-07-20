namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_seclocks")]
    public partial class Lock
    {
        [Required]
        [StringLength(40)]
        public string UserID { get; set; }

        [StringLength(40)]
        public string GroupID { get; set; }

        [Required]
        [StringLength(32)]
        public string EntityType { get; set; }

        [Key]
        [StringLength(40)]
        [Column("EntityID")]
        public string Id { get; set; }

        public DateTime Timestamp { get; set; }

        [StringLength(255)]
        public string LockType { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}

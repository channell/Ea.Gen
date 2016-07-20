namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_secusergroup")]
    public partial class UserGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserGroup()
        {
            Locks = new HashSet<Lock>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public string UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string GroupID { get; set; }

        public virtual Group secgroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lock> Locks { get; set; }

        public virtual User User { get; set; }
    }
}

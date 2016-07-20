namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_objecttypes")]
    public partial class ObjectType
    {
        [Key]
        [StringLength(50)]
        [Column("Object_Type")]
        public string ObjectTypeCode { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int DesignObject { get; set; }

        public int? ImageID { get; set; }
    }
}

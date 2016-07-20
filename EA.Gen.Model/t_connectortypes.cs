namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_connectortypes")]
    public partial class ConnectorType
    {
        [Key]
        [StringLength(50)]
        [Column("Connector_Type")]
        public string ConnectorTypeCode { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}

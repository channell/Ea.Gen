namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_template")]
    public partial class Template
    {
        [Key]
        [StringLength(40)]
        [Column("TemplateID")]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TemplateType { get; set; }

        [Required]
        [StringLength(100)]
        public string TemplateName { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(255)]
        public string Style { get; set; }

        [Column("Template ", TypeName = "ntext")]
        public string TemplateCode { get; set; }
    }
}

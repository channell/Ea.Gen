namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_rtfreport")]
    public partial class Report
    {
        [Key]
        [StringLength(200)]
        [Column("TemplateID")]
        public string Id { get; set; }

        public int? RootPackage { get; set; }

        [StringLength(255)]
        public string Filename { get; set; }

        public int Details { get; set; }

        public int ProcessChildren { get; set; }

        public int ShowDiagrams { get; set; }

        [StringLength(255)]
        public string Heading { get; set; }

        public int Requirements { get; set; }

        public int Associations { get; set; }

        public int Scenarios { get; set; }

        public int ChildDiagrams { get; set; }

        public int Attributes { get; set; }

        public int Methods { get; set; }

        public int? ImageType { get; set; }

        public int Paging { get; set; }

        [Column(TypeName = "ntext")]
        public string Intro { get; set; }

        public int Resources { get; set; }

        public int Constraints { get; set; }

        public int Tagged { get; set; }

        public int ShowTag { get; set; }

        public int ShowAlias { get; set; }

        [StringLength(255)]
        public string PDATA1 { get; set; }

        [StringLength(255)]
        public string PDATA2 { get; set; }

        [StringLength(255)]
        public string PDATA3 { get; set; }

        [Column(TypeName = "ntext")]
        public string PDATA4 { get; set; }
    }
}

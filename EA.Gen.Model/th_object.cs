namespace Cephei.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class th_object
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Obje.ID { get; set; }

        [StringLength(255)]
        public string Obje.Type { get; set; }

        public int? Diagram_ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Alias { get; set; }

        [StringLength(255)]
        public string Author { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [Column(TypeName = "ntext")]
        public string Note { get; set; }

        public int? Package_ID { get; set; }

        [StringLength(255)]
        public string Stereotype { get; set; }

        public int? NType { get; set; }

        [StringLength(50)]
        public string Complexity { get; set; }

        public int? Effort { get; set; }

        [StringLength(255)]
        public string Style { get; set; }

        public int? Backcolor { get; set; }

        public int? BorderStyle { get; set; }

        public int? BorderWidth { get; set; }

        public int? Fontcolor { get; set; }

        public int? Bordercolor { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(1)]
        public string Abstract { get; set; }

        public int? Tagged { get; set; }

        [StringLength(255)]
        public string PDATA1 { get; set; }

        [Column(TypeName = "ntext")]
        public string PDATA2 { get; set; }

        [Column(TypeName = "ntext")]
        public string PDATA3 { get; set; }

        [Column(TypeName = "ntext")]
        public string PDATA4 { get; set; }

        [StringLength(255)]
        public string PDATA5 { get; set; }

        [StringLength(50)]
        public string Concurrency { get; set; }

        [StringLength(50)]
        public string Visibility { get; set; }

        [StringLength(50)]
        public string Persistence { get; set; }

        [StringLength(50)]
        public string Cardinality { get; set; }

        [StringLength(50)]
        public string GenType { get; set; }

        [StringLength(255)]
        public string GenFile { get; set; }

        [Column(TypeName = "ntext")]
        public string Header1 { get; set; }

        [Column(TypeName = "ntext")]
        public string Header2 { get; set; }

        [StringLength(50)]
        public string Phase { get; set; }

        [StringLength(25)]
        public string Scope { get; set; }

        [Column(TypeName = "ntext")]
        public string GenOption { get; set; }

        [Column(TypeName = "ntext")]
        public string GenLinks { get; set; }

        public int? Classifier { get; set; }

        [StringLength(40)]
        public string ea_guid { get; set; }

        public int? ParentID { get; set; }

        [Column(TypeName = "ntext")]
        public string RunState { get; set; }

        [StringLength(40)]
        public string Classifier_guid { get; set; }

        public int? TPos { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsRoot { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsLeaf { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsSpec { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsActive { get; set; }

        [StringLength(255)]
        public string StateFlags { get; set; }

        [StringLength(255)]
        public string PackageFlags { get; set; }

        [StringLength(50)]
        public string Multiplicity { get; set; }

        [Column(TypeName = "ntext")]
        public string StyleEx { get; set; }

        [Column(TypeName = "ntext")]
        public string EventFlags { get; set; }

        [StringLength(255)]
        public string ActionFlags { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string User_Id { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime Change_Date { get; set; }

        public virtual element element { get; set; }
    }
}

namespace EA.Gen.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_connector")]
    public partial class Connector
    {
        [Key]
        [Column("Connector_ID")]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Direction { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(50)]
        public string Connector_Type { get; set; }

        [StringLength(50)]
        public string SubType { get; set; }

        [StringLength(50)]
        public string SourceCard { get; set; }

        [StringLength(50)]
        public string SourceAccess { get; set; }

        [StringLength(50)]
        public string SourceElement { get; set; }

        [StringLength(50)]
        public string DestCard { get; set; }

        [StringLength(50)]
        public string DestAccess { get; set; }

        [StringLength(50)]
        public string DestElement { get; set; }

        [StringLength(50)]
        public string SourceRole { get; set; }

        [StringLength(50)]
        public string SourceRoleType { get; set; }

        [Column(TypeName = "ntext")]
        public string SourceRoleNote { get; set; }

        [StringLength(50)]
        public string SourceContainment { get; set; }

        public int? SourceIsAggregate { get; set; }

        public int? SourceIsOrdered { get; set; }

        [StringLength(50)]
        public string SourceQualifier { get; set; }

        [StringLength(255)]
        public string DestRole { get; set; }

        [StringLength(50)]
        public string DestRoleType { get; set; }

        [Column(TypeName = "ntext")]
        public string DestRoleNote { get; set; }

        [StringLength(50)]
        public string DestContainment { get; set; }

        public int? DestIsAggregate { get; set; }

        public int? DestIsOrdered { get; set; }

        [StringLength(50)]
        public string DestQualifier { get; set; }

        [Column("Start_Object_ID")]
        public int? StartObjectId { get; set; }

        [Column("End_Object_ID")]
        public int? EndObjectId { get; set; }

        [StringLength(50)]
        [Column("Top_Start_Label")]
        public string TopStartLabel { get; set; }

        [StringLength(50)]
        [Column("Top_Mid_Label")]
        public string TopMidLabel { get; set; }

        [StringLength(50)]
        [Column("Top_End_Label")]
        public string TopEndLabel { get; set; }

        [StringLength(50)]
        [Column("Btm_Start_Label")]
        public string BottomStartLabel { get; set; }

        [StringLength(50)]
        [Column("Btm_Mid_Label")]
        public string BottomMidLabel { get; set; }

        [StringLength(50)]
        [Column("Btm_End_Label")]
        public string BottomEndLabel { get; set; }

        [Column("Start_Edge")]
        public int? StartEdge { get; set; }

        [Column("End_Edge")]
        public int? EndEdge { get; set; }

        public int? PtStartX { get; set; }

        public int? PtStartY { get; set; }

        public int? PtEndX { get; set; }

        public int? PtEndY { get; set; }

        public int? SeqNo { get; set; }

        public int? HeadStyle { get; set; }

        public int? LineStyle { get; set; }

        public int? RouteStyle { get; set; }

        public int? IsBold { get; set; }

        public int? LineColor { get; set; }

        [StringLength(50)]
        public string Stereotype { get; set; }

        [StringLength(1)]
        public string VirtualInheritance { get; set; }

        [StringLength(50)]
        public string LinkAccess { get; set; }

        [StringLength(255)]
        public string PDATA1 { get; set; }

        [Column(TypeName = "ntext")]
        public string PDATA2 { get; set; }

        [StringLength(255)]
        public string PDATA3 { get; set; }

        [StringLength(255)]
        public string PDATA4 { get; set; }

        [Column(TypeName = "ntext")]
        public string PDATA5 { get; set; }

        public int? DiagramID { get; set; }

        [StringLength(40)]
        public string ea_guid { get; set; }

        [StringLength(255)]
        public string SourceConstraint { get; set; }

        [StringLength(255)]
        public string DestConstraint { get; set; }

        public int SourceIsNavigable { get; set; }

        public int DestIsNavigable { get; set; }

        public int IsRoot { get; set; }

        public int IsLeaf { get; set; }

        public int IsSpec { get; set; }

        [StringLength(12)]
        public string SourceChangeable { get; set; }

        [StringLength(12)]
        public string DestChangeable { get; set; }

        [StringLength(12)]
        public string SourceTS { get; set; }

        [StringLength(12)]
        public string DestTS { get; set; }

        [StringLength(255)]
        public string StateFlags { get; set; }

        [StringLength(255)]
        public string ActionFlags { get; set; }

        public int IsSignal { get; set; }

        public int IsStimulus { get; set; }

        [StringLength(255)]
        public string DispatchAction { get; set; }

        public int? Target2 { get; set; }

        [Column(TypeName = "ntext")]
        public string StyleEx { get; set; }

        [StringLength(255)]
        public string SourceStereotype { get; set; }

        [StringLength(255)]
        public string DestStereotype { get; set; }

        [Column(TypeName = "ntext")]
        public string SourceStyle { get; set; }

        [Column(TypeName = "ntext")]
        public string DestStyle { get; set; }

        [StringLength(255)]
        public string EventFlags { get; set; }

        public int? Object_ID { get; set; }

        public virtual Element StartElement { get; set; }

        public virtual Element EndElement { get; set; }
    }
}

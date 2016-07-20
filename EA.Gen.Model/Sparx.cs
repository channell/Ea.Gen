namespace EA.Gen.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Sparx : DbContext
    {
        public Sparx(string connection)
            : base (connection)
        {
        }

        public Sparx()
            : base("name=Sparx")
        {
        }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<AttributeConstraint> AttributeConstraints { get; set; }
        public virtual DbSet<AttributeTag> AttributeTags { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Cardinality> Cardinalities { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ComplexityType> ComplexityTypes { get; set; }
        public virtual DbSet<Connector> Connectors { get; set; }
        public virtual DbSet<ConnectorConstraint> ConnectorConstraints { get; set; }
        public virtual DbSet<ConnectorTag> ConnectorTags { get; set; }
        public virtual DbSet<ConnectorType> ConnectorTypes { get; set; }
        public virtual DbSet<Constant> Constants { get; set; }
        public virtual DbSet<CnstraintTypes> ConstraintTypes { get; set; }
        public virtual DbSet<DataType> Datatypes { get; set; }
        public virtual DbSet<Diagram> Diagrams { get; set; }
        public virtual DbSet<DiagramLink> DiagramLinks { get; set; }
        public virtual DbSet<DiagramObject> DiagramObjects { get; set; }
        public virtual DbSet<DiagramType> DiagramTypes { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Ecf> Ecfs { get; set; }
        public virtual DbSet<EffortType> EffortTypes { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Glossary> Glossaries { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<MaintType> MaintTypes { get; set; }
        public virtual DbSet<Method> Methods { get; set; }
        public virtual DbSet<MetricTypes> MetricTypes { get; set; }
        public virtual DbSet<Element> Elements { get; set; }
        public virtual DbSet<ObjectConstraint> ObjectConstraints { get; set; }
        public virtual DbSet<ObjectEffort> ObjectEfforts { get; set; }
        public virtual DbSet<ObjectFile> ObjectFiles { get; set; }
        public virtual DbSet<ObjectMetric> Objectmetrics { get; set; }
        public virtual DbSet<ObjectProblem> Objectproblems { get; set; }
        public virtual DbSet<ObjectProperty> Objectproperties { get; set; }
        public virtual DbSet<ObjecRequire> ObjectRequires { get; set; }
        public virtual DbSet<ObjectResource> ObjectResource { get; set; }
        public virtual DbSet<ObjectRisk> ObjectRisks { get; set; }
        public virtual DbSet<ObjectScenario> ObjectScenarios { get; set; }
        public virtual DbSet<ObjectTest> ObjectTests { get; set; }
        public virtual DbSet<ObjectTrx> ObjectTrxs { get; set; }
        public virtual DbSet<ObjectType> ObjectTypes { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<OperationParam> OperationParams { get; set; }
        public virtual DbSet<OperationPost> OperationPosts { get; set; }
        public virtual DbSet<OperationPrecondition> OperationPreconditions { get; set; }
        public virtual DbSet<OperationTag> OperationTag { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Phase> Phases { get; set; }
        public virtual DbSet<primitives> Primitives { get; set; }
        public virtual DbSet<ProblemType> ProblemTypes { get; set; }
        public virtual DbSet<ProjectRole> ProjectRoles { get; set; }
        public virtual DbSet<propertyType> PropertyTypes { get; set; }
        public virtual DbSet<RequireType> RequireTypes { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<RiskType> RiskTypes { get; set; }
        public virtual DbSet<RoleConstraint> RoleConstraints { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<ScenarioType> ScenarioTypes { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Lock> Locks { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<Snapshot> Snapshots { get; set; }
        public virtual DbSet<StereoType> StereoTypes { get; set; }
        public virtual DbSet<TaggedValue> TaggedValues { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<tcf> Tcfs { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<TestClass> TestClasses { get; set; }
        public virtual DbSet<TestPlans> TestPlans { get; set; }
        public virtual DbSet<TestType> TestTypes { get; set; }
        public virtual DbSet<TrxType> TrxTypes { get; set; }
        public virtual DbSet<UMLPattern> UMLPatterns { get; set; }
        public virtual DbSet<Version> Versions { get; set; }
        public virtual DbSet<Xref> Xrefs { get; set; }
        public virtual DbSet<XrefSystem> Xrefsystems { get; set; }
        public virtual DbSet<XrefUser> Xrefusers { get; set; }
        public virtual DbSet<palette> Palettes { get; set; }
        public virtual DbSet<Script> Scripts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //
            // Added missing relationships
            //
            modelBuilder.Entity<Diagram>()
                .HasMany<DiagramObject>(e => e.Elements)
                .WithRequired(e => e.Diagram)
                .HasForeignKey(e => e.DiagramId);

            modelBuilder.Entity<Diagram>()
                .HasMany<DiagramLink>(e => e.Links)
                .WithRequired(e => e.Diagram)
                .HasForeignKey(e => e.DiagramID);

            modelBuilder.Entity<Element>()
                .HasMany<DiagramObject>(e => e.DiagramObjects)
                .WithRequired(e => e.Element)
                .HasForeignKey(e => e.ObjectId);

            modelBuilder.Entity<Package>()
                .HasMany<Element>(e => e.Elements)
                .WithRequired(e => e.package)
                .HasForeignKey(e => e.PackageId);

            modelBuilder.Entity<Package>()
                .HasMany<Package>(e => e.Children)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Element>()
                .HasMany<Attribute>(e => e.Attributes)
                .WithRequired(e => e.Element)
                .HasForeignKey(e => e.ObjectId);

            modelBuilder.Entity<Element>()
                .HasMany<Operation>(e => e.Operations)
                .WithRequired(e => e.Element)
                .HasForeignKey(e => e.ElementId);

            modelBuilder.Entity<Element>()
                .HasMany<Element>(e => e.Children)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentID);

            //
            // Generated relationships
            //
            modelBuilder.Entity<Attribute>()
                .HasMany(e => e.Constraints)
                .WithRequired(e => e.Attribute)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.StartConnectors)
                .WithOptional(e => e.StartElement)
                .HasForeignKey(e => e.StartObjectId);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.EndConnectors)
                .WithOptional(e => e.EndElement)
                .HasForeignKey(e => e.EndObjectId);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.Constraint)
                .WithRequired(e => e.Element)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.Efforts)
                .WithRequired(e => e.Element)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.Files)
                .WithRequired(e => e.Element)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.Metrics)
                .WithRequired(e => e.Element)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.Problems)
                .WithRequired(e => e.Element)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.Resource)
                .WithRequired(e => e.Element)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.Risks)
                .WithRequired(e => e.Element)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.Tests)
                .WithRequired(e => e.Element)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Element>()
                .HasMany(e => e.Trxs)
                .WithRequired(e => e.Element)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ObjectProblem>()
                .Property(e => e.Problem)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectProblem>()
                .Property(e => e.ProblemType)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectResource>()
                .Property(e => e.Resource)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectResource>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<Operation>()
                .HasMany(e => e.operationparams)
                .WithRequired(e => e.operation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Operation>()
                .HasMany(e => e.operationposts)
                .WithRequired(e => e.operation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Operation>()
                .HasMany(e => e.operationpres)
                .WithRequired(e => e.operation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phase>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Phase>()
                .Property(e => e.PhaseName)
                .IsUnicode(false);

            modelBuilder.Entity<Phase>()
                .Property(e => e.PhaseStyle)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.UserGroups)
                .WithRequired(e => e.secgroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Permissions)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("t_secgrouppermission").MapLeftKey("GroupID").MapRightKey("PermissionID"));

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Permissions)
                .Map(m => m.ToTable("t_secuserpermission").MapLeftKey("PermissionID").MapRightKey("UserID"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.Groups)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserGroup>()
                .HasMany(e => e.Locks)
                .WithOptional(e => e.UserGroup)
                .HasForeignKey(e => new { e.UserID, e.GroupID });

            modelBuilder.Entity<StereoType>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<StereoType>()
                .Property(e => e.AppliesTo)
                .IsUnicode(false);

            modelBuilder.Entity<StereoType>()
                .Property(e => e.VisualType)
                .IsUnicode(false);
        }
    }
}

namespace Domain.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
            Database.SetInitializer<Model1>(null);
        }

        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<certificate> certificates { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<clientcategory> clientcategories { get; set; }
        public virtual DbSet<clienttype> clienttypes { get; set; }
        public virtual DbSet<competence> competences { get; set; }
        public virtual DbSet<conversation> conversations { get; set; }
        public virtual DbSet<dayoff> dayoffs { get; set; }
        public virtual DbSet<establishment> establishments { get; set; }
        public virtual DbSet<field> fields { get; set; }
        public virtual DbSet<hibernate_sequences> hibernate_sequences { get; set; }
        public virtual DbSet<holiday> holidays { get; set; }
        public virtual DbSet<leaverequest> leaverequests { get; set; }
        public virtual DbSet<leavetype> leavetypes { get; set; }
        public virtual DbSet<level> levels { get; set; }
        public virtual DbSet<message> messages { get; set; }
        public virtual DbSet<note> notes { get; set; }
        public virtual DbSet<organigram> organigrams { get; set; }
        public virtual DbSet<project> projects { get; set; }
        public virtual DbSet<projectrequest> projectrequests { get; set; }
        public virtual DbSet<resource> resources { get; set; }
        public virtual DbSet<resume> resumes { get; set; }
        public virtual DbSet<seniority> seniorities { get; set; }
        public virtual DbSet<society> societies { get; set; }
        public virtual DbSet<term> terms { get; set; }
        public virtual DbSet<termarchive> termarchives { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<conversation_user> conversation_user { get; set; }
        public virtual DbSet<message_user> message_user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<admin>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .HasMany(e => e.projectrequests)
                .WithMany(e => e.admins)
                .Map(m => m.ToTable("projectrequest_admin").MapLeftKey("reviewedBy_idUser").MapRightKey("projectRequests_idRequest"));

            modelBuilder.Entity<certificate>()
                .Property(e => e.descriptionCertificate)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.clientName)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.clientType)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .Property(e => e.logo)
                .IsUnicode(false);

            modelBuilder.Entity<client>()
                .HasMany(e => e.projects)
                .WithOptional(e => e.client)
                .HasForeignKey(e => e.client_idUser);

            modelBuilder.Entity<client>()
                .HasMany(e => e.organigrams)
                .WithOptional(e => e.client)
                .HasForeignKey(e => e.client_idUser);

            modelBuilder.Entity<client>()
                .HasMany(e => e.projectrequests)
                .WithOptional(e => e.client)
                .HasForeignKey(e => e.client_idUser);

            modelBuilder.Entity<client>()
                .HasMany(e => e.notes)
                .WithOptional(e => e.client)
                .HasForeignKey(e => e.client_idUser);

            modelBuilder.Entity<clientcategory>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<clientcategory>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<clientcategory>()
                .HasMany(e => e.clients)
                .WithOptional(e => e.clientcategory)
                .HasForeignKey(e => e.clientCategory_idCategory);

            modelBuilder.Entity<clienttype>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<clienttype>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<competence>()
                .Property(e => e.Label)
                .IsUnicode(false);

            modelBuilder.Entity<competence>()
                .HasMany(e => e.levels)
                .WithRequired(e => e.competence)
                .HasForeignKey(e => e.idCompetence)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<competence>()
                .HasMany(e => e.resumes)
                .WithMany(e => e.competences)
                .Map(m => m.ToTable("resume_competence", "map").MapRightKey("Resume_idResume"));

            modelBuilder.Entity<competence>()
                .HasMany(e => e.projects)
                .WithMany(e => e.competences)
                .Map(m => m.ToTable("project_competence").MapLeftKey("competences_idCompetence").MapRightKey("Project_idProject"));

            modelBuilder.Entity<conversation>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<conversation>()
                .HasMany(e => e.conversation_user)
                .WithRequired(e => e.conversation)
                .HasForeignKey(e => e.Conversation_idConversation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<conversation>()
                .HasMany(e => e.messages)
                .WithOptional(e => e.conversation)
                .HasForeignKey(e => e.conversation_idConversation);

            modelBuilder.Entity<establishment>()
                .Property(e => e.nameEstablishment)
                .IsUnicode(false);

            modelBuilder.Entity<establishment>()
                .HasMany(e => e.resumes)
                .WithMany(e => e.establishments)
                .Map(m => m.ToTable("resume_establishment", "map").MapLeftKey("etablissement_idEstablishment").MapRightKey("Resume_idResume"));

            modelBuilder.Entity<field>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<field>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<field>()
                .HasMany(e => e.resources)
                .WithMany(e => e.fields)
                .Map(m => m.ToTable("field_resource").MapLeftKey("fields_idField").MapRightKey("resources_idUser"));

            modelBuilder.Entity<hibernate_sequences>()
                .Property(e => e.sequence_name)
                .IsUnicode(false);

            modelBuilder.Entity<holiday>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<holiday>()
                .HasMany(e => e.resources)
                .WithMany(e => e.holidays)
                .Map(m => m.ToTable("resource_holidays", "map").MapLeftKey("holidays_idHolidays").MapRightKey("Resource_idUser"));

            modelBuilder.Entity<leaverequest>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<leavetype>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<leavetype>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<leavetype>()
                .HasMany(e => e.dayoffs)
                .WithOptional(e => e.leavetype)
                .HasForeignKey(e => e.leaveType_idLeaveType);

            modelBuilder.Entity<leavetype>()
                .HasMany(e => e.leaverequests)
                .WithOptional(e => e.leavetype)
                .HasForeignKey(e => e.leaveType_idLeaveType);

            modelBuilder.Entity<level>()
                .HasMany(e => e.resources)
                .WithMany(e => e.levels1)
                .Map(m => m.ToTable("resource_level", "map").MapLeftKey(new[] { "levels_idCompetence", "levels_idLevel", "levels_idResource" }).MapRightKey("Resource_idUser"));

            modelBuilder.Entity<level>()
                .HasMany(e => e.competences)
                .WithMany(e => e.levels1)
                .Map(m => m.ToTable("competence_level").MapLeftKey(new[] { "levels_idCompetence", "levels_idLevel", "levels_idResource" }).MapRightKey("Competence_idCompetence"));

            modelBuilder.Entity<message>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<message>()
                .HasMany(e => e.message_user)
                .WithRequired(e => e.message)
                .HasForeignKey(e => e.Message_idMessage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<note>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<note>()
                .HasMany(e => e.terms)
                .WithOptional(e => e.note)
                .HasForeignKey(e => e.note_idNote);

            modelBuilder.Entity<note>()
                .HasMany(e => e.resources)
                .WithMany(e => e.notes)
                .Map(m => m.ToTable("resource_note", "map").MapLeftKey("notes_idNote").MapRightKey("Resource_idUser"));

            modelBuilder.Entity<organigram>()
                .Property(e => e.assignmentManager)
                .IsUnicode(false);

            modelBuilder.Entity<organigram>()
                .Property(e => e.financialManager)
                .IsUnicode(false);

            modelBuilder.Entity<organigram>()
                .Property(e => e.programName)
                .IsUnicode(false);

            modelBuilder.Entity<organigram>()
                .Property(e => e.projectManagerName)
                .IsUnicode(false);

            modelBuilder.Entity<organigram>()
                .Property(e => e.projectName)
                .IsUnicode(false);

            //modelBuilder.Entity<project>()
            //    .Property(e => e.description)
            //    .IsUnicode(false);

            modelBuilder.Entity<project>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<project>()
                .Property(e => e.picture)
                .IsUnicode(false);

            modelBuilder.Entity<project>()
                .Property(e => e.projectType)
                .IsUnicode(false);

            modelBuilder.Entity<project>()
                .HasMany(e => e.terms)
                .WithRequired(e => e.project)
                .HasForeignKey(e => e.idProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<project>()
                .HasMany(e => e.terms1)
                .WithMany(e => e.projects)
                .Map(m => m.ToTable("project_term").MapLeftKey("Project_idProject").MapRightKey(new[] { "terms_idProject", "terms_idResource" }));

            modelBuilder.Entity<projectrequest>()
                .Property(e => e.comments)
                .IsUnicode(false);

            modelBuilder.Entity<projectrequest>()
                .Property(e => e.descriptionProject)
                .IsUnicode(false);

            modelBuilder.Entity<projectrequest>()
                .Property(e => e.nameProject)
                .IsUnicode(false);

            modelBuilder.Entity<projectrequest>()
                .Property(e => e.presentedBy)
                .IsUnicode(false);

            modelBuilder.Entity<projectrequest>()
                .HasMany(e => e.competences)
                .WithMany(e => e.projectrequests)
                .Map(m => m.ToTable("projectrequest_competence").MapLeftKey("projectRequests_idRequest").MapRightKey("competences_idCompetence"));

            modelBuilder.Entity<resource>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<resource>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<resource>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<resource>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<resource>()
                .Property(e => e.availability)
                .IsUnicode(false);

            modelBuilder.Entity<resource>()
                .Property(e => e.contractType)
                .IsUnicode(false);

            modelBuilder.Entity<resource>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<resource>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<resource>()
                .Property(e => e.photo)
                .IsUnicode(false);

            modelBuilder.Entity<resource>()
                .HasMany(e => e.dayoffs)
                .WithOptional(e => e.resource)
                .HasForeignKey(e => e.resource_idUser);

            modelBuilder.Entity<resource>()
                .HasMany(e => e.leaverequests)
                .WithOptional(e => e.resource)
                .HasForeignKey(e => e.resource_idUser);

            modelBuilder.Entity<resource>()
                .HasMany(e => e.levels)
                .WithRequired(e => e.resource)
                .HasForeignKey(e => e.idResource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<resource>()
                .HasMany(e => e.terms)
                .WithRequired(e => e.resource)
                .HasForeignKey(e => e.idResource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<resource>()
                .HasMany(e => e.leaverequests1)
                .WithMany(e => e.resources)
                .Map(m => m.ToTable("resource_leaverequest", "map").MapLeftKey("Resource_idUser").MapRightKey("leaveRequests_idLeaveRequest"));

            modelBuilder.Entity<resource>()
                .HasMany(e => e.terms1)
                .WithMany(e => e.resources)
                .Map(m => m.ToTable("resource_term", "map").MapLeftKey("Resource_idUser").MapRightKey(new[] { "terms_idProject", "terms_idResource" }));

            modelBuilder.Entity<resume>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<resume>()
                .HasMany(e => e.resources)
                .WithOptional(e => e.resume)
                .HasForeignKey(e => e.resume_idResume);

            modelBuilder.Entity<resume>()
                .HasMany(e => e.certificates)
                .WithMany(e => e.resumes)
                .Map(m => m.ToTable("resume_certificate", "map").MapRightKey("certificates_idCertificate"));

            modelBuilder.Entity<seniority>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<seniority>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<seniority>()
                .HasMany(e => e.resources)
                .WithOptional(e => e.seniority)
                .HasForeignKey(e => e.seniority_idSeniority);

            modelBuilder.Entity<society>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<society>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<society>()
                .HasMany(e => e.resumes)
                .WithMany(e => e.societies)
                .Map(m => m.ToTable("resume_society", "map").MapRightKey("Resume_idResume"));

            modelBuilder.Entity<term>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<term>()
                .HasMany(e => e.termarchives)
                .WithOptional(e => e.term)
                .HasForeignKey(e => new { e.term_idProject, e.term_idResource });

            modelBuilder.Entity<user>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);
        }
    }
}

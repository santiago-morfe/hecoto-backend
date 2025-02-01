using Microsoft.EntityFrameworkCore;
using hecotoBackend.Models;
using System.Text.Json;

namespace hecotoBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Sobrescribir SaveChanges para manejar las fechas automáticas
        public override int SaveChanges()
        {
            
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseModel>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de claves compuestas
            modelBuilder.Entity<TagsColiseumModel>().HasKey(tc => new { tc.ColiseumId, tc.TagId });
            modelBuilder.Entity<UserMedalsModel>().HasKey(um => new { um.UserId, um.MedalId });
            modelBuilder.Entity<TagLaboratoryModel>().HasKey(tl => new { tl.LaboratoryId, tl.TagId });
            modelBuilder.Entity<LaboratoryParticipantsModel>().HasKey(lp => new { lp.UserId, lp.LaboratoryId });
            modelBuilder.Entity<UserTermsAcceptanceModel>().HasKey(uta => new { uta.UserId, uta.TermsId });

            // Configuración de relaciones y comportamientos de eliminación
            modelBuilder.Entity<RefreshTokensModel>()
                .HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LaboratoriesModel>()
                .HasOne(l => l.Creator)
                .WithMany()
                .HasForeignKey(l => l.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActivitiesModel>()
                .Property(a => a.Content)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonDocument.Parse(v, new JsonDocumentOptions()));

            modelBuilder.Entity<ActivityResponsesModel>()
                .Property(ar => ar.UserResponse)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonDocument.Parse(v, new JsonDocumentOptions()));

            // Configuración de nombres de tablas para coincidir con PostgreSQL
            modelBuilder.Entity<LaboratoryParticipantsModel>().ToTable("laboratoryParticipants");
            modelBuilder.Entity<TagsColiseumModel>().ToTable("TagsColiseum");
            modelBuilder.Entity<TagLaboratoryModel>().ToTable("TagLaboratory");
        }

        // DbSets para todas las entidades
        public DbSet<UsersModel> Users { get; set; }
        public DbSet<RefreshTokensModel> RefreshTokens { get; set; }
        public DbSet<LaboratoriesModel> Laboratories { get; set; }
        public DbSet<ClassesModel> Classes { get; set; }
        public DbSet<ActivityTypesModel> ActivityTypes { get; set; }
        public DbSet<ActivitiesModel> Activities { get; set; }
        public DbSet<ActivityResponsesModel> ActivityResponses { get; set; }
        public DbSet<ColiseumsModel> Coliseums { get; set; }
        public DbSet<TagsModel> Tags { get; set; }
        public DbSet<ArenasModel> Arenas { get; set; }
        public DbSet<BattlesModel> Battles { get; set; }
        public DbSet<BattleResponsesModel> BattleResponses { get; set; }
        public DbSet<PermissionTypesModel> PermissionTypes { get; set; }
        public DbSet<PermissionsModel> Permissions { get; set; }
        public DbSet<MedalsModel> Medals { get; set; }
        public DbSet<TermsAndConditionsModel> TermsAndConditions { get; set; }
        public DbSet<TagsColiseumModel> TagsColiseum { get; set; }
        public DbSet<UserMedalsModel> UserMedals { get; set; }
        public DbSet<TagLaboratoryModel> TagLaboratory { get; set; }
        public DbSet<UserTermsAcceptanceModel> UserTermsAcceptance { get; set; }
        public DbSet<LaboratoryParticipantsModel> laboratoryParticipants { get; set; }
        public DbSet<LaboratoryInvitationsModel> LaboratoryInvitations { get; set; }
    }
}
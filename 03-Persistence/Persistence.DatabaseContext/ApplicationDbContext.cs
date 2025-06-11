using Microsoft.AspNet.Identity.EntityFramework;
using Common;
using System.Data.Entity;
using Model.Domain;
using System.Reflection;
using System.Linq;
using Model.Helper;
using System;
using EntityFramework.DynamicFilters;
using Common.CustomFilters;
using Model.Auth;
using Model.Domain.Area;
using Model.Domain.Cargo;
using Model.Domain.Evaluacion;
using Model.Domain.Formulario;
using Model.Domain.Pegunta;
using Model.Domain.Respuesta;
using Model.Domain.Usuario;
using Model.Domain.Utils;

namespace Persistence.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        #region DbSets
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
        public DbSet<UsuariosAsignado> UsuarioAsignado { get; set; }

        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentPerCourse> StudentPerCourse { get; set; }
        public virtual DbSet<Course> Course { get; set; }

        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<PreguntaDetalle> PreguntaDetalle { get; set; }
        

        public virtual DbSet<Respuesta> Respuesta { get; set; }
        public virtual DbSet<RespuestaDetalle> RespuestaDetalle { get; set; }       

        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
        public virtual DbSet<EvaluacionDetalle> EvaluacionDetalle { get; set; }
        public virtual DbSet<EvaluacionConfig> EvaluacionConfig { get; set; }
        public virtual DbSet<EvaluacionUsuario> EvaluacionRol { get; set; }
        public virtual DbSet<EvaluacionAsignada> EvaluacionAsignada { get; set; }

        public virtual DbSet<Formulario> Formulario { get; set; }
        public virtual DbSet<FormularioDetalle> FormularioDetalle { get; set; }
        public virtual DbSet<FormularioConfig> FormularioConfig { get; set; }

        public virtual DbSet<Area> Area { get; set; }

        public virtual DbSet<Cargo> Cargo { get; set; }

        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Catalogo> Catalogo { get; set; }


        
        #endregion

        public ApplicationDbContext()
            : base($"name={Parameters.AppContext}")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            AddMyFilters(ref modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)));

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            MakeAudit();
            return base.SaveChanges();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        private void MakeAudit()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(
                x => x.Entity is AuditEntity
                    && (
                    x.State == EntityState.Added
                    || x.State == EntityState.Modified
                    || x.State == EntityState.Deleted
                )
            );

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as AuditEntity;
                if (entity != null)
                {
                    var date = DateTime.Now;
                    var userId = CurrentUserHelper.Get != null ? CurrentUserHelper.Get.UserId : null;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = date;
                        entity.CreatedBy = userId;
                    }
                    else if (entity is ISoftDeleted && ((ISoftDeleted)entity).Deleted)
                    {
                        entity.DeletedAt = date;
                        entity.DeletedBy = userId;
                    }

                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;

                    entity.UpdatedAt = date;
                    entity.UpdatedBy = userId;
                }
            }
        }

        private void AddMyFilters(ref DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter(Enums.MyFilters.IsDeleted.ToString(), (ISoftDeleted ea) => ea.Deleted, false);
        }
    }
}

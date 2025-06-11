namespace Persistence.DatabaseContext.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Identification = c.Long(),
                        Name = c.String(),
                        LastName = c.String(),
                        Enable = c.Boolean(),
                        Area = c.Long(),
                        Cargo = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Identification, unique: true, name: "IX_IdentificationNumber");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        IdArea = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Area_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdArea)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        CargoId = c.Long(nullable: false, identity: true),
                        Nombre = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Cargo_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.CargoId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.Catalogo",
                c => new
                    {
                        IdCatalogo = c.Int(nullable: false),
                        Nombre = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Catalogo_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdCatalogo)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Course_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.StudentPerCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SuscribedAt = c.DateTime(nullable: false),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                        Birthday = c.DateTime(nullable: false),
                        Genre = c.Int(nullable: false),
                        CurrentStatus = c.Int(nullable: false),
                        LastVisit = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Student_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.Evaluacion",
                c => new
                    {
                        IdEvaluacion = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Evaluacion_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdEvaluacion)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.EvaluacionAsignadas",
                c => new
                    {
                        IdEvaluacionAsignada = c.Int(nullable: false, identity: true),
                        IdEvaluacion = c.Int(nullable: false),
                        IdEvaluador = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EvaluacionAsignada_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdEvaluacionAsignada)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.EvaluacionConfig",
                c => new
                    {
                        IdEvaluacionConfig = c.Int(nullable: false, identity: true),
                        IdEvaluacion = c.Int(nullable: false),
                        IdFormulario = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EvaluacionConfig_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdEvaluacionConfig)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.EvaluacionDetalle",
                c => new
                    {
                        IdEvaluacionDetalle = c.Int(nullable: false, identity: true),
                        IdEvaluacion = c.Int(nullable: false),
                        EvaluacionDetalleItem = c.Int(nullable: false),
                        Valor = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EvaluacionDetalle_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdEvaluacionDetalle)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.EvaluacionUsuarios",
                c => new
                    {
                        IdEvaluacionUsuario = c.Int(nullable: false, identity: true),
                        IdEvaluacion = c.Int(nullable: false),
                        IdEvaluador = c.String(),
                        IdEvaluado = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EvaluacionUsuario_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdEvaluacionUsuario)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.Formulario",
                c => new
                    {
                        IdFormulario = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Formulario_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdFormulario)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.FormularioConfig",
                c => new
                    {
                        IdFormularioConfig = c.Int(nullable: false, identity: true),
                        IdFormulario = c.Int(nullable: false),
                        IdPregunta = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FormularioConfig_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdFormularioConfig)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.FormularioDetalle",
                c => new
                    {
                        IdFormularioDetalle = c.Int(nullable: false, identity: true),
                        IdFormulario = c.Int(nullable: false),
                        FormDetalleItem = c.Int(nullable: false),
                        Valor = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FormularioDetalle_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdFormularioDetalle)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        IdItem = c.Int(nullable: false),
                        IdCatalogo = c.Int(nullable: false),
                        ItemName = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Item_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdItem)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.Pregunta",
                c => new
                    {
                        IdPregunta = c.Int(nullable: false, identity: true),
                        IdFormulario = c.Int(nullable: false),
                        Titulo = c.String(nullable: false),
                        Descripcion = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Pregunta_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdPregunta)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.PreguntaDetalle",
                c => new
                    {
                        IdPreguntaDetalle = c.Int(nullable: false, identity: true),
                        IdPregunta = c.Int(nullable: false),
                        DetalleItem = c.Int(nullable: false),
                        Valor = c.String(),
                        ValorLargo = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PreguntaDetalle_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdPreguntaDetalle)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .ForeignKey("dbo.Pregunta", t => t.IdPregunta, cascadeDelete: true)
                .Index(t => t.IdPregunta)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.Respuestas",
                c => new
                    {
                        IdRespuesta = c.Int(nullable: false, identity: true),
                        IdEvaluador = c.String(),
                        IdEvaluado = c.String(),
                        IdEvaluacion = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Respuesta_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdRespuesta)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.RespuestaDetalles",
                c => new
                    {
                        IdRespuestaDetalle = c.Int(nullable: false, identity: true),
                        IdREspuesta = c.Int(nullable: false),
                        IdPreguntaDetalle = c.Int(nullable: false),
                        ValorRespuesta = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RespuestaDetalle_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdRespuestaDetalle)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.UsuariosAsignado",
                c => new
                    {
                        IdUsuarioAsignado = c.Int(nullable: false, identity: true),
                        IdEvaluacionAsignada = c.Int(nullable: false),
                        IdEvaluado = c.String(),
                        IsEvaluado = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UsuariosAsignado_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.IdUsuarioAsignado)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuariosAsignado", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsuariosAsignado", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsuariosAsignado", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RespuestaDetalles", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.RespuestaDetalles", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.RespuestaDetalles", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Respuestas", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Respuestas", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Respuestas", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pregunta", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.PreguntaDetalle", "IdPregunta", "dbo.Pregunta");
            DropForeignKey("dbo.PreguntaDetalle", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.PreguntaDetalle", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.PreguntaDetalle", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pregunta", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pregunta", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Item", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Item", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Item", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.FormularioDetalle", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.FormularioDetalle", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.FormularioDetalle", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.FormularioConfig", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.FormularioConfig", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.FormularioConfig", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Formulario", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Formulario", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Formulario", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionUsuarios", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionUsuarios", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionUsuarios", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionDetalle", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionDetalle", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionDetalle", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionConfig", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionConfig", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionConfig", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionAsignadas", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionAsignadas", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.EvaluacionAsignadas", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Evaluacion", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Evaluacion", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Evaluacion", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Students", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentPerCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Students", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentPerCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Catalogo", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Catalogo", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Catalogo", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cargoes", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cargoes", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cargoes", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Areas", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Areas", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Areas", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.UsuariosAsignado", new[] { "DeletedBy" });
            DropIndex("dbo.UsuariosAsignado", new[] { "UpdatedBy" });
            DropIndex("dbo.UsuariosAsignado", new[] { "CreatedBy" });
            DropIndex("dbo.RespuestaDetalles", new[] { "DeletedBy" });
            DropIndex("dbo.RespuestaDetalles", new[] { "UpdatedBy" });
            DropIndex("dbo.RespuestaDetalles", new[] { "CreatedBy" });
            DropIndex("dbo.Respuestas", new[] { "DeletedBy" });
            DropIndex("dbo.Respuestas", new[] { "UpdatedBy" });
            DropIndex("dbo.Respuestas", new[] { "CreatedBy" });
            DropIndex("dbo.PreguntaDetalle", new[] { "DeletedBy" });
            DropIndex("dbo.PreguntaDetalle", new[] { "UpdatedBy" });
            DropIndex("dbo.PreguntaDetalle", new[] { "CreatedBy" });
            DropIndex("dbo.PreguntaDetalle", new[] { "IdPregunta" });
            DropIndex("dbo.Pregunta", new[] { "DeletedBy" });
            DropIndex("dbo.Pregunta", new[] { "UpdatedBy" });
            DropIndex("dbo.Pregunta", new[] { "CreatedBy" });
            DropIndex("dbo.Item", new[] { "DeletedBy" });
            DropIndex("dbo.Item", new[] { "UpdatedBy" });
            DropIndex("dbo.Item", new[] { "CreatedBy" });
            DropIndex("dbo.FormularioDetalle", new[] { "DeletedBy" });
            DropIndex("dbo.FormularioDetalle", new[] { "UpdatedBy" });
            DropIndex("dbo.FormularioDetalle", new[] { "CreatedBy" });
            DropIndex("dbo.FormularioConfig", new[] { "DeletedBy" });
            DropIndex("dbo.FormularioConfig", new[] { "UpdatedBy" });
            DropIndex("dbo.FormularioConfig", new[] { "CreatedBy" });
            DropIndex("dbo.Formulario", new[] { "DeletedBy" });
            DropIndex("dbo.Formulario", new[] { "UpdatedBy" });
            DropIndex("dbo.Formulario", new[] { "CreatedBy" });
            DropIndex("dbo.EvaluacionUsuarios", new[] { "DeletedBy" });
            DropIndex("dbo.EvaluacionUsuarios", new[] { "UpdatedBy" });
            DropIndex("dbo.EvaluacionUsuarios", new[] { "CreatedBy" });
            DropIndex("dbo.EvaluacionDetalle", new[] { "DeletedBy" });
            DropIndex("dbo.EvaluacionDetalle", new[] { "UpdatedBy" });
            DropIndex("dbo.EvaluacionDetalle", new[] { "CreatedBy" });
            DropIndex("dbo.EvaluacionConfig", new[] { "DeletedBy" });
            DropIndex("dbo.EvaluacionConfig", new[] { "UpdatedBy" });
            DropIndex("dbo.EvaluacionConfig", new[] { "CreatedBy" });
            DropIndex("dbo.EvaluacionAsignadas", new[] { "DeletedBy" });
            DropIndex("dbo.EvaluacionAsignadas", new[] { "UpdatedBy" });
            DropIndex("dbo.EvaluacionAsignadas", new[] { "CreatedBy" });
            DropIndex("dbo.Evaluacion", new[] { "DeletedBy" });
            DropIndex("dbo.Evaluacion", new[] { "UpdatedBy" });
            DropIndex("dbo.Evaluacion", new[] { "CreatedBy" });
            DropIndex("dbo.Students", new[] { "DeletedBy" });
            DropIndex("dbo.Students", new[] { "UpdatedBy" });
            DropIndex("dbo.Students", new[] { "CreatedBy" });
            DropIndex("dbo.StudentPerCourses", new[] { "CourseId" });
            DropIndex("dbo.StudentPerCourses", new[] { "StudentId" });
            DropIndex("dbo.Courses", new[] { "DeletedBy" });
            DropIndex("dbo.Courses", new[] { "UpdatedBy" });
            DropIndex("dbo.Courses", new[] { "CreatedBy" });
            DropIndex("dbo.Catalogo", new[] { "DeletedBy" });
            DropIndex("dbo.Catalogo", new[] { "UpdatedBy" });
            DropIndex("dbo.Catalogo", new[] { "CreatedBy" });
            DropIndex("dbo.Cargoes", new[] { "DeletedBy" });
            DropIndex("dbo.Cargoes", new[] { "UpdatedBy" });
            DropIndex("dbo.Cargoes", new[] { "CreatedBy" });
            DropIndex("dbo.Areas", new[] { "DeletedBy" });
            DropIndex("dbo.Areas", new[] { "UpdatedBy" });
            DropIndex("dbo.Areas", new[] { "CreatedBy" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "IX_IdentificationNumber");
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.UsuariosAsignado",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UsuariosAsignado_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.RespuestaDetalles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RespuestaDetalle_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Respuestas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Respuesta_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.PreguntaDetalle",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PreguntaDetalle_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Pregunta",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Pregunta_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Item",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Item_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.FormularioDetalle",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FormularioDetalle_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.FormularioConfig",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FormularioConfig_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Formulario",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Formulario_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.EvaluacionUsuarios",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EvaluacionUsuario_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.EvaluacionDetalle",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EvaluacionDetalle_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.EvaluacionConfig",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EvaluacionConfig_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.EvaluacionAsignadas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EvaluacionAsignada_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Evaluacion",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Evaluacion_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Students",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Student_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.StudentPerCourses");
            DropTable("dbo.Courses",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Course_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Catalogo",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Catalogo_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Cargoes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Cargo_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Areas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Area_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}

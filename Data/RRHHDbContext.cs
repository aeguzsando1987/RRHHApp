
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Interfaces;

namespace RRHH.WebApi.Data {

    /// <summary>
    /// Clase que hereda de DbContext y que sera usada para interactuar con la base de datos
    /// </summary>
    public class RRHHDbContext : DbContext {
        
        /// <summary>
        /// Constructor que recibe las opciones de configuracion para la base de datos
        /// </summary>
        /// <param name="options">Opciones de configuracion para la base de datos</param>
        public RRHHDbContext(DbContextOptions<RRHHDbContext> options) : base(options) { }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Organizaciones
        /// </summary>
        public DbSet<Organizacion> Organizaciones { get; set; }
        /// <summary>
        /// Propiedad que permite acceder a la tabla Empresas
        /// </summary>
        public DbSet<Empresa> Empresas { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla de direcciones de empresas
        /// </summary>
        public DbSet<Empresas_Direccion> Empresas_Direcciones { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Contactos de empresas
        /// </summary>
        public DbSet<ContactosEmpresa> ContactosEmpresas { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Areas
        /// </summary>
        public DbSet<Area> Areas { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Departamentos
        /// </summary>
        public DbSet<Departamento> Departamentos { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Jerarquias
        /// </summary>
        public DbSet<Jerarquia> Jerarquias { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Puestos
        /// </summary>
        public DbSet<Puesto> Puestos { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Puestos Descriptivo
        /// </summary>
        public DbSet<PuestosDescriptivo> PuestosDescriptivo { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Puestos Actividad
        /// </summary>
        public DbSet<PuestosActividad> PuestosActividad { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Empleados
        /// </summary>
        public DbSet<Empleado> Empleados { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Empleados Perfil
        /// </summary>
        public DbSet<Empleado_Perfil> Empleados_Perfil { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Empleados Tipo
        /// </summary>
        public DbSet<Empleado_Tipo> Empleados_Tipo { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla de direcciones de empleados
        /// </summary>
        public DbSet<Empleados_Direccion> Empleados_Direcciones { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Ubicaciones
        /// </summary>
        
        public DbSet<Ubicacion> Ubicaciones { get; set; } 

        /// <summary>
        /// Propiedad que permite acceder a la tabla Status
        /// </summary>
        public DbSet<Status> Statuses { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Propiedad que permite acceder a la tabla Contactos
        /// </summary>
        public DbSet<ContactosEmpleado> ContactosEmpleados { get; set; }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.
        /// This can include state entries for entities and/or relationships.
        /// Relationships are included in the count.
        /// </returns>
        public override int SaveChanges()
        {
            UpdateAuditableEntities();
            return base.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Actualiza las propiedades de Fecha_Modificacion de las entidades que implementan IAuditable
        /// </summary>
        private void UpdateAuditableEntities()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((IAuditable)entityEntry.Entity).Fecha_Modificacion = DateTime.Now;
            }
        }

        /// <summary>
        /// Metodo que se llama al crear la base de datos y que permite definir las relaciones y claves entre las tablas
        /// </summary>
        /// <param name="modelBuilder">Modelo que representa la base de datos</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
    
            // Relacion entre Organizacion y Empresa: una Organizacion puede tener varias Empresas (1:N)
            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.Organizacion)
                .WithMany(o => o.Empresas)
                .HasForeignKey(e => e.Id_Org)
                // Si se elimina una Organizacion, se eliminaran todas las Empresas relacionadas
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Empresa y Empresas_Direccion: una Empresa puede tener varias direcciones (1:N)
            modelBuilder.Entity<Empresas_Direccion>()
                .HasOne(ed => ed.Empresa)
                .WithMany(e => e.Empresas_Direcciones)
                .HasForeignKey(ed => ed.Id_Empresa)
                .OnDelete(DeleteBehavior.Cascade);


            // Relacion entre Empresa y ContactosEmpresa: una Empresa puede tener varios Contactos (1:N)
            modelBuilder.Entity<ContactosEmpresa>()
                .HasOne(c => c.Empresa)
                .WithMany(e => e.Contactos)
                .HasForeignKey(c => c.Id_Empresa)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Empresa y Area: una Empresa puede tener varias Areas (1:N)
            modelBuilder.Entity<Area>()
                .HasOne(a => a.Empresa)
                .WithMany(e => e.Areas)
                .HasForeignKey(a => a.Id_Empresa)
                // Si se elimina una Empresa, se eliminaran todas las Areas relacionadas
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Area y Departamento: un Area puede tener varios Departamentos (1:N)
            modelBuilder.Entity<Departamento>()
                .HasOne(d => d.Area)
                .WithMany(a => a.Departamentos)
                .HasForeignKey(d => d.Id_Area)
                // Si se elimina un Area, se eliminaran todos los Departamentos relacionados
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Departamento y Puesto: un Departamento puede tener varios Puestos (1:N)
            modelBuilder.Entity<Puesto>()
                .HasOne(p => p.Departamento)
                .WithMany(d => d.Puestos)
                .HasForeignKey(p => p.Id_Departamento)
                // Si se elimina un Departamento, se eliminaran todos los Puestos relacionados
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Puesto y Jerarquia: un Puesto tiene una Jerarquia (N:1)
            modelBuilder.Entity<Puesto>()
                .HasOne(p => p.Jerarquia)
                .WithMany(j => j.Puestos)
                .HasForeignKey(p => p.Id_Jerarquia)
                // Si se elimina una Jerarquia, se eliminaran todos los Puestos relacionados
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Puesto y Puestos Descriptivo: un puesto tiene su correspondiente descriptivo (1:1)
            modelBuilder.Entity<Puesto>()
                .HasOne(p => p.PuestosDescriptivo)    
                .WithOne(pd => pd.Puesto)
                .HasForeignKey<PuestosDescriptivo>(pd => pd.ID_Puesto)
                // Si se elimina un Puesto, se eliminara su correspondiente Puestos Descriptivo
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Puestos Descriptivo y Puestos Actividad: un Puestos Descriptivo puede tener varios Puestos Actividad (1:N)
            modelBuilder.Entity<PuestosActividad>()
                .HasOne(pa => pa.PuestosDescriptivo)
                .WithMany(pd => pd.PuestosActividad)
                .HasForeignKey(pa => pa.ID_PuestosDescriptivo)
                // Si se elimina un Puestos Descriptivo, se eliminaran todos los Puestos Actividad relacionados
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Empleado y Puesto: un Empleado tiene un Puesto (1:1)
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Puesto)
                .WithMany(p => p.Empleados)
                .HasForeignKey(e => e.Id_Puesto)
                // Si se elimina un Puesto, se eliminara el Empleado relacionado
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relacion entre Empleado y Status: un Empleado tiene un Status (N:1)
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Status)
                .WithMany(s => s.Empleados)
                .HasForeignKey(e => e.Id_Status)
                // Si se elimina un Status, no se eliminaran los Empleados relacionados
                .OnDelete(DeleteBehavior.Restrict);

            // Relacion entre Empleado y Jefe: un Empleado puede tener un Jefe (self-reference, N:1)
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Jefe)
                .WithMany()
                .HasForeignKey(e => e.Id_Jefe)
                // OnDelete con DeleteBehavior.Restrict indica que si se elimina un Empleado,
                // su jefe no sera eliminado, solo se borrara el enlace.
                .OnDelete(DeleteBehavior.Restrict);

            // Relacion entre Empleado y Ubicacion: un Empleado puede tener una Ubicacion (N:1)
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Ubicacion)
                .WithMany(u => u.Empleados)
                .HasForeignKey(e => e.Id_Ubicacion)
                // Si se elimina una Ubicacion, no se eliminaran los Empleados relacionados
                .OnDelete(DeleteBehavior.Restrict);

            // Relacion entre Empleado y User: un Empleado tiene un User (1:1)
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.User)
                .WithOne(u => u.Empleado)
                .HasForeignKey<User>(u => u.Id_Empleado)
                // Si se elimina un Empleado, se eliminara el User relacionado
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Empleado y Contactos: un Empleado puede tener varios Contactos (N:1)
            modelBuilder.Entity<ContactosEmpleado>()
                .HasOne(c => c.Empleado)
                .WithMany(e => e.Contactos)
                .HasForeignKey(c => c.Id_Empleado)
                // OnDelete con DeleteBehavior.Cascade indica que si se elimina un Empleado
                // tambien se eliminaran todos sus Contactos
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Empresa y Ubicacion: una empresa puede tener varias ubicaciones (1:N)
            modelBuilder.Entity<Ubicacion>()
                .HasOne(u => u.Empresa)
                .WithMany(e => e.Ubicaciones)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relacion entre Ubicacion y Empleado: una ubicacion puede tener varios empleados (N:1)
            // OnDelete con DeleteBehavior.SetNull indica que si se elimina una Ubicacion,
            // los Empleados relacionados no seran eliminados, solo se borrara el enlace.
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Ubicacion)
                .WithMany(u => u.Empleados)
                .HasForeignKey(e => e.Id_Ubicacion)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacion entre Empleado y Direcciones: un Empleado puede tener varias direcciones (N:1)
            modelBuilder.Entity<Empleados_Direccion>()
                .HasOne(ed => ed.Empleado)
                .WithMany(e => e.Direcciones)
                .HasForeignKey(ed => ed.Id_Empleado)
                // OnDelete con DeleteBehavior.Cascade indica que si se elimina un Empleado
                // tambien se eliminaran todos sus direcciones
                .OnDelete(DeleteBehavior.Cascade);


            // Relacion entre Empleado y Perfil: un Empleado tiene un Perfil (1:1)
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Perfil)
                .WithOne(p => p.Empleado)
                .HasForeignKey<Empleado_Perfil>(p => p.Id_Empleado)
                // Si se elimina un Empleado, se eliminara su Perfil relacionado
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion entre Perfil y Tipo: un Tipo tiene muchos Perfiles (N:1)
            modelBuilder.Entity<Empleado_Perfil>()
                .HasOne(ep => ep.Tipo)
                .WithMany(et => et.Perfiles)
                .HasForeignKey(ep => ep.Id_Tipo_Empleado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Empleado_Tipo>().HasData(
                new Empleado_Tipo { ID = 1, Titulo = "DE CONFIANZA", Descripcion = "COLABORADOR DE CONFIANZA", Prefijo = "CNF" },
                new Empleado_Tipo { ID = 2, Titulo = "DE PLANTA", Descripcion = "TECNICO OPERATIVO CON HRS. EXTRA", Prefijo = "PLA" },
                new Empleado_Tipo { ID = 3, Titulo = "EVENTUAL", Descripcion = "EMPLEADO EVENTUAL CON CONTRATO TEMPORAL", Prefijo = "EVT" },
                new Empleado_Tipo { ID = 4, Titulo = "BECARIO", Descripcion = "BENEFICIARIO DE FORMACION PROFESIONAL", Prefijo = "BEC" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status {ID = 1, Status_Emp = "ACTIVO", Descripcion_Status = "EMPLEADO ACTIVO"},
                new Status {ID = 2, Status_Emp = "SUSPENDIDO", Descripcion_Status = "EMPLEADO SUSPENDIDO"},
                new Status {ID = 3, Status_Emp = "BAJA VOLUNTARIA", Descripcion_Status = "EMPLEADO INACTIVO POR BAJA VOLUNTARIA"},
                new Status {ID = 4, Status_Emp = "BAJA INVOLUNTARIA", Descripcion_Status = "EMPLEADO INACTIVO POR BAJA INVOLUNTARIA"},
                new Status {ID = 5, Status_Emp = "INCAPACIDAD", Descripcion_Status = "EMPLEADO INACTIVO POR INCAPACIDAD MEDICA"}
            );

            modelBuilder.Entity<Organizacion>().HasData(
                new Organizacion {Id = 1, Clave = "ORG1", Nombre = "CONGLOMERADO", Fecha_Creacion = DateTime.Parse("2000-01-01")}
            );

            modelBuilder.Entity<Empresa>().HasData(
                new Empresa {ID = 1, Id_Org = 1, Clave = "EMP1", Razon_Social = "EMPRESA PRUEBA", Fecha_Creacion = DateTime.Parse("2000-01-01")}
            );
            
            modelBuilder.Entity<Area>().HasData(
                new Area {ID = 1, Id_Empresa = 1, Clave = "AR1", Nombre = "AREA PRUEBA", Descripcion = "AREA PRUEBA"}
            );

            modelBuilder.Entity<Departamento>().HasData(
                new Departamento {ID = 1, Id_Area = 1, Clave = "DEP1", Nombre = "DEPARTAMENTO PRUEBA", Descripcion = "DEPARTAMENTO PRUEBA"}
            );

            modelBuilder.Entity<Jerarquia>().HasData(
                new Jerarquia {ID = 1, Clave = "DIR", Titulo = "DIRECCION GENERAL", Descripcion = "DIRECCION GENERAL", Nivel = 0},
                new Jerarquia {ID = 2, Clave = "DAR", Titulo = "DIRECCION", Descripcion = "DIRECCION DE AREA O UNIDAD", Nivel = 1},
                new Jerarquia {ID = 3, Clave = "GER", Titulo = "GERENCIA", Descripcion = "GERENCIA DE AREA O DEPARTAMENTO", Nivel = 2},
                new Jerarquia {ID = 4, Clave = "CRD", Titulo = "COORDINACION", Descripcion = "COORDINACION/JEFATURA DE DEPTO. U OFICINA", Nivel = 3},
                new Jerarquia {ID = 6, Clave = "SUP", Titulo = "SUPERVISION", Descripcion = "SUPERVISION DE DEPTO. U OFICINA", Nivel = 5},
                new Jerarquia {ID = 7, Clave = "AUX", Titulo = "AUXILIAR", Descripcion = "COLABORACION AUX. ADMVA. U OPERATIVA", Nivel = 6}
            );
        }
    }
}
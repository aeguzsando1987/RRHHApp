
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Models;

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
                .HasForeignKey(pa => pa.ID_PuestoDescriptivo)
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

            
        }
    }
}
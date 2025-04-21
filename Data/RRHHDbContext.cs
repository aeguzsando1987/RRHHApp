
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Data {

    public class RRHHDbContext : DbContext {
        public RRHHDbContext(DbContextOptions<RRHHDbContext> options) : base(options) { }

        public DbSet<Organizacion> Organizaciones { get; set; }
        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Jerarquia> Jerarquias { get; set; }

        public DbSet<Puesto> Puestos { get; set; }

        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<Ubicacion> Ubicaciones { get; set; } 

        public DbSet<Status> Statuses { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
    
           // Relación Organizacion -> Empresa (1:N)
            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.Organizacion)
                .WithMany(o => o.Empresas)
                .HasForeignKey(e => e.Id_Org);

            // Relación Empresa -> Area (1:N)
            modelBuilder.Entity<Area>()
                .HasOne(a => a.Empresa)
                .WithMany(e => e.Areas)
                .HasForeignKey(a => a.Id_Empresa);

            // Relación Area -> Departamento (1:N)
            modelBuilder.Entity<Departamento>()
                .HasOne(d => d.Area)
                .WithMany(a => a.Departamentos)
                .HasForeignKey(d => d.Id_Area);

            // Relación Departamento -> Puesto (1:N)
            modelBuilder.Entity<Puesto>()
                .HasOne(p => p.Departamento)
                .WithMany(d => d.Puestos)
                .HasForeignKey(p => p.Id_Departamento);

            // Un Puesto puede tener varios Departamentos, y un Departamento puede tener varios Puestos.
            // Relación Puesto -> Departamento (N:N)

            // Relación Puesto -> Jerarquia (N:1)
            modelBuilder.Entity<Puesto>()
                .HasOne(p => p.Jerarquia)
                .WithMany(j => j.Puestos)
                .HasForeignKey(p => p.Id_Jerarquia);
        }
    }
}
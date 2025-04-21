
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


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Empresa>()
            .HasOne(e => e.Organizacion)
            .WithMany(o => o.Empresas)
            .HasForeignKey(e => e.Id_Org);
    }



    }

   
}
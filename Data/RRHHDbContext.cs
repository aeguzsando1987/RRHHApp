
using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Data {

    public class RRHHDbContext : DbContext {
        public RRHHDbContext(DbContextOptions<RRHHDbContext> options) : base(options) { }

        public DbSet<Organizacion> Organizaciones { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
    }

}
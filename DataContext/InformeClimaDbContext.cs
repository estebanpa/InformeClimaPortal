using InformeClimaPortal.Entities;
using Microsoft.EntityFrameworkCore;

namespace InformeClimaPortal.DataContext
{
    public class InformeClimaDbContext: DbContext
    {
        public InformeClimaDbContext(DbContextOptions<InformeClimaDbContext> options) : base(options)
        {

        }

        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Informe> Informes { get; set; }
        public DbSet<Pais> Paises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ciudad>().
                ToTable("Ciudad");

            modelBuilder.Entity<Informe>()
                .ToTable("Informe");

            modelBuilder.Entity<Pais>()
                .ToTable("Pais");
        }
    }
}

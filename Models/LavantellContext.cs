using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LavantellAPIS.Models
{
    public partial class LavantellContext : DbContext
    {
        public LavantellContext()
        {
        }

        public LavantellContext(DbContextOptions<LavantellContext> options)
            : base(options)
        {
        }


        //public virtual DbSet<Servicios> Servicios { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=servidorpruebal.database.windows.net,1433;Database=practica;User Id=lavantell;Password=blakenanaypruebas123@;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");



            modelBuilder.Entity<Servicios>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasDefaultValueSql("(N'')");
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

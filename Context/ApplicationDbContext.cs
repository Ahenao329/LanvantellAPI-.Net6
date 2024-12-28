using LavantellAPIS.Models;
using Microsoft.EntityFrameworkCore;

namespace LavantellAPIS.Context
{
    public class ApplicationDbContext: DbContext
    {



        public DbSet<Nosotros> Nosotros { get; set; }
        public DbSet<Contactanos> Contactanos { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<Sucursales> Sucursales { get; set; }
        public DbSet<Tarjeta> Tarjeta { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        }


}

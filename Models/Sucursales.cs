using System.ComponentModel.DataAnnotations;

namespace LavantellAPIS.Models
{
    public class Sucursales
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Horario { get; set; }
        [Required]
        public string Administrador { get; set; }


    }
}

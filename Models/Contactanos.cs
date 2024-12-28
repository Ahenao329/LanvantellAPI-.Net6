using System.ComponentModel.DataAnnotations;

namespace LavantellAPIS.Models
{
    public class Contactanos
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        [Required]
        public string Mensaje { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace LavantellAPIS.Models
{
    public class Nosotros
    {
        public int Id { get; set; }
        [Required]

        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
    }
}

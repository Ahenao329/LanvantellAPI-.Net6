using System.ComponentModel.DataAnnotations;

namespace LavantellAPIS.Models
{
    public class Servicios
    {
        public int Id { get; set; }
        [Required]

        public string Titulo { get; set; }
        [Required]

        public string Descripcion { get; set; }
    }
}

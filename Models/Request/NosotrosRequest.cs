using System.ComponentModel.DataAnnotations;

namespace LavantellAPIS.Models.Request
{
    public class NosotrosRequest
    {
        [Required]
        public string Descripcion { get; set; }
        public IFormFile Image { get; set; }

    }
}

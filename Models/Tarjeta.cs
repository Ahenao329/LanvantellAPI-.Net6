using LavantellAPIS.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace LavantellAPIS.Models
{
    public class Tarjeta
    {
        public int Id { get; set; }
        [Required]
        public string Titular { get; set; }
        [Required]
        [PrimeraLetraMayuscula]
        public string NumeroTarjeta { get; set; }
        [Required] 
        public string FeachaExpedicion { get; set; }
        [Required]

        public string Cvv { get; set; }

    }
}

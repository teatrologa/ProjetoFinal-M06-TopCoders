using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.M06.Core.Models
{
    public class CityEvent
    {
        public long IdEvent { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "A data é uma informação obrigatória")]
        public DateTime? DateHourEvent { get; set; }

        [Required]
        public string Local { get; set; }
        
        public string? Address { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Insira um valor maior ou igual 0 (para eventos gratuitos)")]
        public decimal? Price { get; set; }

        [Required]
        public bool Status { get; set; } = true;

    }
}
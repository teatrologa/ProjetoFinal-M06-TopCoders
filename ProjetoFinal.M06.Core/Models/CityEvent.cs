using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.M06.Core.Models
{
    public class CityEvent
    {
        [DefaultValue(0)]
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "O t�tulo do evento � obrigat�rio.")]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "A data � uma informa��o obrigat�ria")]
        public DateTime? DateHourEvent { get; set; }

        [Required(ErrorMessage = "O local do evento � obrigat�rio")]
        public string Local { get; set; }
        
        public string? Address { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Insira um valor maior ou igual 0 (para eventos gratuitos)")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "O status � um informa��o obrigat�ria.")]
        [DefaultValue(true)]
        public bool Status { get; set; }

    }
}
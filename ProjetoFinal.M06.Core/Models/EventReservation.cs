using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.M06.Core.Models
{
    public class EventReservation
    {
        public long IdReservation { get; set; }

        [Required(ErrorMessage = "O ID do evento é uma informação obrigatória")]
        public long? IdEvent { get; set; }

        [Required(ErrorMessage = "O nome é um informação obrigatória")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "O valor da quantidade de uma reserva é uma informação obrigatória")]
        [Range(1, long.MaxValue, ErrorMessage = "Insira uma quantidade significativa de reservas.")]
        public long? Quantity { get; set; }

    }
}

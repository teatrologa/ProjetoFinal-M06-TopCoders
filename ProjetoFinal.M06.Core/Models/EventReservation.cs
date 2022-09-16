using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.M06.Core.Models
{
    public class EventReservation
    {
        public long IdReservation { get; set; }

        [Required(ErrorMessage = "O ID do evento é uma informação obrigatória")]
        public long? IdEvent { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "A quantidade de uma reserva é obrigatória")]
        [Range(1, long.MaxValue, ErrorMessage = "Insira uma quantidade significativa de reservas.")]
        public long? Quantity { get; set; }

    }
}

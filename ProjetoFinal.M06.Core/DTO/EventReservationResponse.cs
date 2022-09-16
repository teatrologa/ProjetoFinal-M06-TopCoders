using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal.M06.Core.DTO
{
    public class EventReservationResponse
    {
        public long IdEvent { get; set; }

        public string PersonName { get; set; }

        public long Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal.M06.Core.DTO
{
    public class CityEventResponse
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateHourEvent { get; set; }

        public string Local { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public bool Status { get; set; }
    }
}

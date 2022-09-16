using ProjetoFinal.M06.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal.M06.Core.Interface
{
    public interface IEventReservationRepository
    {
        public List<EventReservation> GetAllReservations();

        public EventReservation GetIdReservation(long idReservation);

        public List<EventReservation> GetPersonTitleReservation(string personName, string title);

        public List<EventReservation> GetEventReservations(long idEvent);

        public bool InsertNewReservation(EventReservation eventReservation);

        public bool ChangeReservation(long idReservation, EventReservation eventReservation);

        public bool DeleteReservation(long idReservation);

    }
}

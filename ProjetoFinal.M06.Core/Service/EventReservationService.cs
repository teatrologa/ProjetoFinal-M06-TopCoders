using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal.M06.Core.Service
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;
        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }
        public List<EventReservation> GetAllReservations() 
        { 
            return _eventReservationRepository.GetAllReservations(); 
        }

        public EventReservation GetIdReservation(long idReservation) 
        { 
            return _eventReservationRepository.GetIdReservation(idReservation);
        }

        public List<EventReservation> GetPersonReservations(string personName) 
        { 
            return _eventReservationRepository.GetPersonReservations(personName); 
        }

        public List<EventReservation> GetEventReservations(long idEvent)
        {
            return _eventReservationRepository.GetEventReservations(idEvent);
        }

        public bool InsertNewReservation(EventReservation eventReservation)
        {
            return _eventReservationRepository.InsertNewReservation(eventReservation);
        }

        public bool ChangeReservation(long idReservation, EventReservation eventReservation) 
        { 
            return _eventReservationRepository.ChangeReservation(idReservation, eventReservation); 
        }

        public bool DeleteReservation(long idReservation) 
        { 
            return _eventReservationRepository.DeleteReservation(idReservation); 
        }
    }
}

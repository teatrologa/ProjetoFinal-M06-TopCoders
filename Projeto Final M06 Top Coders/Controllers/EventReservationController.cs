using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;
using ProjetoFinal.M06.Core.Service;

namespace ProjetoFinal.M06.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;
        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/Reservations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<CityEvent>> GetAllReservations()
        {
            var allReservations = _eventReservationService.GetAllReservations();
            if (allReservations.Any() == true)
            {
                return Ok(allReservations);
            }
            return NoContent();
        }

        [HttpGet("/Reservations/Id/{idReservation}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventReservation> GetIdReservation(long idReservation)
        {
            var reservation = _eventReservationService.GetIdReservation(idReservation);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpGet("/Reservations/Nome/{personName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<EventReservation>> GetPersonReservations(string personName)
        {
            var personReservations = _eventReservationService.GetPersonReservations(personName);
            if (personReservations == null)
            {
                return NotFound();
            }else if (personReservations.Any() == false)
            {
                return NoContent();
            }
            return Ok(personReservations);
        }

        [HttpGet("/Reservations/Evento/{idEvent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<EventReservation>> GetEventReservations(long idEvent)
        {
            var eventReservation = _eventReservationService.GetEventReservations(idEvent);
            if (eventReservation == null)
            {
                return NotFound();
            } else if (eventReservation.Any() == false)
            {
                return NoContent();
            }
            return Ok(eventReservation);
        }

        [HttpPost("/Reservations/New")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<EventReservation> InsertNewReservation(EventReservation eventReservation)
        {
            //_eventReservationService.InsertNewReservation(eventReservation)
            if (!_eventReservationService.InsertNewReservation(eventReservation))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(InsertNewReservation), eventReservation);
        }

        [HttpPut("/Reservations/{idReservation}/Update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ChangeReservation(long idReservation, EventReservation eventReservation)
        {
            if (!_eventReservationService.ChangeReservation(idReservation, eventReservation))
            {
                return NotFound();
            }
            //testando essa devolutiva
            var reservation = _eventReservationService.ChangeReservation(idReservation, eventReservation);
            return Accepted(reservation);
        }

        [HttpDelete("/Reservations/{idReservation}/Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteReservation(long idReservation)
        {
            if (!_eventReservationService.DeleteReservation(idReservation))
            {
                return NotFound();
            }
            _eventReservationService.DeleteReservation(idReservation);
            return NoContent();
        }
    }
}
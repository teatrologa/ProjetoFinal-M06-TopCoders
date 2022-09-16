using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;
using ProjetoFinal.M06.Core.Service;
using ProjetoFinal.M06.Filters;

namespace ProjetoFinal.M06.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;
        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/Reservations/Nome/Local")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<EventReservation>> GetPersonTitleReservation(string personName, string title)
        {
            var personReservations = _eventReservationService.GetPersonTitleReservation(personName, title);
            
            if (personReservations.Any() == false)
            {
                return NoContent();
            }
            return Ok(personReservations);
        }


        [HttpPost("/Reservations/New")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ServiceFilter(typeof(CheckIdEventActionFilter_ER))]
        public ActionResult<EventReservation> InsertNewReservation([FromBody] EventReservation eventReservation)
        {   
            if (!_eventReservationService.InsertNewReservation(eventReservation))
            {
                return Conflict("Houve um conflito entre as informações passadas.");
            }
            return CreatedAtAction(nameof(InsertNewReservation), eventReservation);
        }


        [HttpPatch("/Reservations/{idReservation}/Update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(CheckIdReservationActionFilter))]
        public ActionResult ChangeReservation(long idReservation, [FromBody] EventReservation eventReservation)
        {
            if (!_eventReservationService.ChangeReservation(idReservation, eventReservation))
            {
                return BadRequest("Não foi possível atualizar a reserva.");
            }

            var reservation = _eventReservationService.ChangeReservation(idReservation, eventReservation);
            
            return Accepted(reservation);
        }

        [HttpDelete("/Reservations/{idReservation}/Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(CheckIdReservationActionFilter))]
        public ActionResult DeleteReservation(long idReservation)
        {   
            _eventReservationService.DeleteReservation(idReservation);
            
            return NoContent();
        }
    }
}
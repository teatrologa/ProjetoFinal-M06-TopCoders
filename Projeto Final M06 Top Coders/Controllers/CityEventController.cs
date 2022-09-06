using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;

namespace ProjetoFinal.M06.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/Events")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<CityEvent>> GetAllEvents()
        {
            var allEvents = _cityEventService.GetAllEvents();
            if (allEvents.Any() == true)
            {
                return Ok(allEvents);
            }
            return NoContent();
        }

        [HttpGet("/Events/{idEvent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CityEvent> GetIdEvent(long idEvent)
        {
            var eventId = _cityEventService.GetIdEvent(idEvent);
            if (eventId == null)
            {
                return NotFound();
            }
            return Ok(eventId);
        }

        [HttpGet("/Events/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<CityEvent>> GetTitleEvent(string title)
        {
            var titleEvent = _cityEventService.GetTitleEvent(title);
            if (titleEvent == null)
            {
                return NotFound();
            }
            return Ok(titleEvent);
        }

    }
}
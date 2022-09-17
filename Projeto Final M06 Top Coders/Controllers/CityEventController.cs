using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;
using ProjetoFinal.M06.Filters;

namespace ProjetoFinal.M06.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]

    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }



        [HttpGet("/Events/Titulo/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<CityEvent>> GetTitleEvent(string title)
        {
            var titleEvent = _cityEventService.GetTitleEvent(title);

            if (titleEvent.Any() == false)
            {
                return NoContent();
            }
            return Ok(titleEvent);
        }



        [HttpGet("/Events/Local/Date/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(CheckDateActionFilter_CE))]
        public ActionResult<List<CityEvent>> GetLocalDateEvent(string local, DateTime dateHourEvent)
        {

            var selectEvents = _cityEventService.GetLocalDateEvent(local, dateHourEvent);

            if (selectEvents.Any() == false)
            {
                return NoContent();
            }

            return Ok(selectEvents);
        }



        [HttpGet("/Events/Price/Date/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(CheckDateActionFilter_CE))]
        [ServiceFilter(typeof(CheckPriceValuesActionFilter_CE))]
        public ActionResult<List<CityEvent>> GetPriceDateEvent(decimal priceMin, decimal priceMax, DateTime dateHourEvent)
        {
            var selectEvents = _cityEventService.GetPriceDateEvent(priceMin, priceMax, dateHourEvent);

            if (selectEvents.Any() == false)
            {
                return NoContent();
            }

            return Ok(selectEvents);
        }



        [HttpPost("/Events/New")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ServiceFilter(typeof(CheckDateActionFilter_CE))]
        public ActionResult<CityEvent> InsertNewEvent(CityEvent cityEvent)
        {
            if (!_cityEventService.InsertNewEvent(cityEvent))
            {
                return BadRequest("Falha ao criar evento. O campo de 'Title', 'DateHourEvent', 'Local' e 'Status' são obrigatórios, revise sua requisição.");
            }
            return CreatedAtAction(nameof(InsertNewEvent), cityEvent);
        }



        [HttpPut("/Events/{idEvent}/Update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(CheckIdEventActionFilter_CE))]
        [ServiceFilter(typeof(CheckDateActionFilter_CE))]
        public ActionResult ChangeEvent(long idEvent, [FromBody] CityEvent cityEvent)
        {
            if(!_cityEventService.ChangeEvent(idEvent, cityEvent))
            {
                return BadRequest("Não foi possível atualizar o evento.");
            }
            _cityEventService.ChangeEvent(idEvent, cityEvent);
            return Accepted(cityEvent);
        }

        
        
        [HttpDelete("/Events/{idEvent}/DeleteOrDisable")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(CheckIdEventActionFilter_CE))]
        public ActionResult DeleteorDisableEvent(long idEvent)
        {
            if (_cityEventService.IsThereAnyReservation(idEvent))
            {
                var selEvent = _cityEventService.GetIdEvent(idEvent);
                selEvent.Status = false;
                _cityEventService.ChangeEvent(idEvent, selEvent);
                return Accepted();
            }
            else
            {
                _cityEventService.DeleteEvent(idEvent);
                return NoContent();
            }
        }

    }
}
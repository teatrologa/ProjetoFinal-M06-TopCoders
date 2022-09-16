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
        public ActionResult<List<CityEvent>> GetLocalDateEvent(string local, DateTime dateHourEvent)
        {
            if (dateHourEvent.Year < 1753 || dateHourEvent.Year > 9999)
            {
                return BadRequest("A data está no formato errado, favor inserir um valor entre os anos de 1753 e 9999");
            }

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
        public ActionResult<List<CityEvent>> GetPriceDateEvent(decimal priceMin, decimal priceMax, DateTime dateHourEvent)
        {
            if (priceMin < 0 || priceMax < 0)
            {
                return BadRequest("Insira os preços no valores correto de 0 para cima.");
            } else if (priceMin > priceMax)
            {
                return BadRequest("Insira os valores nos locais corretos, o preço máximo sempre será superior ao minimo");
            } else if (priceMin == priceMax)
            {
                return BadRequest("Sinto muito, os valores min e max não podem ser iguais. Escolha outro range.");
            }

            if (dateHourEvent.Year < 1753 || dateHourEvent.Year > 9999)
            {
                return BadRequest("A data está no formato errado, favor inserir um valor entre os anos de 1753 e 9999");
            }

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
        public ActionResult<CityEvent> InsertNewEvent(CityEvent cityEvent)
        {
            //verificar como usar "conflito" quando o evento já existir.
            if (!_cityEventService.InsertNewEvent(cityEvent))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(InsertNewEvent), cityEvent);
        }

        [HttpPut("/Events/{idEvent}/Update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ChangeEvent(long idEvent, CityEvent cityEvent)
        {
            if(!_cityEventService.ChangeEvent(idEvent, cityEvent))
            {
                return NotFound();
            }
            _cityEventService.ChangeEvent(idEvent, cityEvent);
            return Accepted(cityEvent);
        }

        [HttpDelete("/Events/{idEvent}/DeleteOrDisable")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteEvent(long idEvent)
        {
            if (_cityEventService.GetIdEvent(idEvent) != null)
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
            
            
            
            //if (!_cityEventService.DeleteEvent(idEvent))
            //{
            //    return NotFound();
            //}
            //_cityEventService.DeleteEvent(idEvent);
            return NoContent();
        }

    }
}
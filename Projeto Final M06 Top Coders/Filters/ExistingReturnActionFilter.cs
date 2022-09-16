using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinal.M06.Core.Interface;

namespace ProjetoFinal.M06.Filters
{
    public class ExistingReturnActionFilter : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;
        public IEventReservationService _eventReservationService;
        public ExistingReturnActionFilter(ICityEventService cityEventService, IEventReservationService eventReservationService)
        {
            _cityEventService = cityEventService;
            _eventReservationService = eventReservationService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var data = context.ActionArguments["dateHourEvent"];


        }
    }
}

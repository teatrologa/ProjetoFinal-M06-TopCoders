using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;

namespace ProjetoFinal.M06.Filters
{
    public class CheckIdEventActionFilter_ER : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;

        public CheckIdEventActionFilter_ER(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 406,
                Title = "IdEvent inválido",
                Detail = @"Não existe nenhum evento com o ID inserido. Revise sua requisição."
            };

            EventReservation existEvent = (EventReservation)context.ActionArguments["eventReservation"];

            if (_cityEventService.GetIdEventBool((long)existEvent.IdEvent) == false)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                context.Result = new ObjectResult(problem);
            }
        }
    }
}

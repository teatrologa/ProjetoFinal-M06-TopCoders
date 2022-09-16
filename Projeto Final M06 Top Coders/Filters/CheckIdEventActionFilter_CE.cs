using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;

namespace ProjetoFinal.M06.Filters
{
    public class CheckIdEventActionFilter_CE : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;

        public CheckIdEventActionFilter_CE(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 404,
                Title = "IdEvent inválido",
                Detail = @"Não existe nenhum evento com o ID inserido. Revise sua requisição."
            };

            CityEvent existEvent = (CityEvent)context.ActionArguments["cityEvent"];

            if (_cityEventService.GetIdEvent(existEvent.IdEvent) == null)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Result = new ObjectResult(problem);
            }
        }
    }
}

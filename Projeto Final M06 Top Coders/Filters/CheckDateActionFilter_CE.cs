using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;
using System.Linq;

namespace ProjetoFinal.M06.Filters
{
    public class CheckDateActionFilter_CE : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;
        public CheckDateActionFilter_CE(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 400,
                Title = "Data no formato inválido",
                Detail = "A data está no formato errado, você pode usar um dos formatos 'aaaa-mm-dd', " +
                "'mm-dd-aaaa' com hifens (-) ou barras (/) e " +
                "lembre-se que ela deve estar entre os anos de 1753 e 9999."
            };
            if (context.ActionArguments.ContainsKey("cityEvent"))
            {
                CityEvent cityEvent = (CityEvent)context.ActionArguments["cityEvent"];
                DateTime data = Convert.ToDateTime(cityEvent.DateHourEvent);
                if (_cityEventService.CheckDateEvent(data))
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = new ObjectResult(problem);
                }
            }else if (!context.ActionArguments.ContainsKey("dateHourEvent"))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new ObjectResult(problem);
            }
            else if (context.ActionArguments.ContainsKey("dateHourEvent"))
            {
                DateTime dateHourEvent = (DateTime)context.ActionArguments["dateHourEvent"];
                if (_cityEventService.CheckDateEvent(dateHourEvent))
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = new ObjectResult(problem);
                }

            }

        }
    }

}

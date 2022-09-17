using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinal.M06.Core.Interface;

namespace ProjetoFinal.M06.Filters
{
    public class CheckPriceValuesActionFilter_CE : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;
        public CheckPriceValuesActionFilter_CE(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 400,
                Title = "Valores de preço incorretos",
                Detail = "Insira valores aceitaveis de preço, eles não devem ser nulos nem negativos. " +
                "O preço máximo é sempre superior ao preço minimo e ambos não devem ser iguais."
            };

            if (!context.ActionArguments.ContainsKey("priceMin") || !context.ActionArguments.ContainsKey("priceMax"))
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new ObjectResult(problem);
            }
            else
            {
                decimal priceMax = (decimal)context.ActionArguments["priceMax"];
                decimal priceMin = (decimal)context.ActionArguments["priceMin"];

                if(_cityEventService.CheckPriceValues(priceMin, priceMax))
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = new ObjectResult(problem);
                }
            }
        }


    }
}

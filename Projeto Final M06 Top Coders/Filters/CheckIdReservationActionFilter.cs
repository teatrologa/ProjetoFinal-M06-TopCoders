using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;
using ProjetoFinal.M06.Core.Service;

namespace ProjetoFinal.M06.Filters
{
    public class CheckIdReservationActionFilter : ActionFilterAttribute
    {
        public IEventReservationService _eventReservationService;
        public CheckIdReservationActionFilter(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idReservation = (long)context.ActionArguments["idReservation"];

            var problem = new ProblemDetails
            {
                Status = 404,
                Title = "IdReservation não encontrado",
                Detail = "Não há nenhuma reserva com este ID, lembre-se sempre de que nossos " +
                "IDs são sequencias de números positivos maiores que 0."
            };

            if (_eventReservationService.GetIdReservation(idReservation) == null)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Result = new ObjectResult(problem);
            }

        }
    }
}

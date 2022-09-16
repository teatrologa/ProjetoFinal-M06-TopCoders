using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ProjetoFinal.M06.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var defaultProblem = new ProblemDetails
            {
                Status = 500,
                Title = "Erro inesperado",
                Detail = "Ocorreu um erro inesperado na solicitação",
                Type = context.Exception.GetType().Name,
            };

            var sqlExceptionProblem = new ProblemDetails
            {
                Status = 503,
                Title = "Serviço indisponível",
                Detail = "Erro inesperado ao se comunicar com o banco de dados",
                Type = context.Exception.GetType().Name,
            };

            var nullReferenceExceptionProblem = new ProblemDetails
            {
                Status = 417,
                Title = "Falha na resposta",
                Detail = "Erro inesperado no sistema",
                Type = context.Exception.GetType().Name,
            };

            Console.WriteLine($"Tipo da exceção: {context.Exception.GetType().Name}. " +
                   $"Mensagem: {context.Exception.Message}. " +
                   $"Stack trace: {context.Exception.StackTrace}.");

            switch (context.Exception)
            {
                case SqlException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    context.Result = new ObjectResult(sqlExceptionProblem);
                    break;

                case NullReferenceException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status417ExpectationFailed;
                    context.Result = new ObjectResult(nullReferenceExceptionProblem);
                    break;

                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(defaultProblem);
                    break;
            }
        }
    }
}

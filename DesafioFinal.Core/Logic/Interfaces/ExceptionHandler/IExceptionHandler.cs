using Microsoft.AspNetCore.Http;

namespace DesafioFinal.Core.Logic.Interfaces.ExceptionHandler
{
    public interface IExceptionHandler
    {
        Task<bool> HandleAsync(Exception exception, HttpContext context);
    }
}

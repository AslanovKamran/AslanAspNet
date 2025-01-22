using System.Net;
using AslanAspNet.Server.Handlers;
using AslanAspNet.Server.Middlewares.Abstract;

namespace AslanAspNet.Server.Middlewares.Concrete;

public class LoggerMiddleware : IMiddleware
{
    public HttpHandler Next { get; set; }
    public LoggerMiddleware(HttpHandler next) => Next = next;
    public void RunTask(HttpListenerContext context)
    {
        var method = context.Request.HttpMethod;
        var rawUrl = context.Request.RawUrl;
        var requestTime = DateTime.Now;
        Console.WriteLine($"Log: {method} {rawUrl} \t\t{requestTime}");

        Next?.Invoke(context); // <- Вызов след. m\w в цепи
    }
}

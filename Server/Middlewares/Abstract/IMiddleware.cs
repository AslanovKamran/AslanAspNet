using AslanAspNet.Server.Handlers;
using System.Net;

namespace AslanAspNet.Server.Middlewares.Abstract;

public interface IMiddleware
{
    public HttpHandler Next { get; set; }
    void RunTask(HttpListenerContext context);
}

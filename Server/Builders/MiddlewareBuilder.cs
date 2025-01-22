
using System.Net;
using AslanAspNet.Server.Handlers;
using AslanAspNet.Server.Middlewares.Abstract;

namespace AslanAspNet.Server.Builders;

public class MiddlewareBuilder
{
    private Stack<Type> _types = new();

    public MiddlewareBuilder Use<T>() where T : IMiddleware 
    {
        _types.Push(typeof(T));
        return this;
    }

    public HttpHandler Build() 
    {
        HttpHandler handler = TerminalMiddleware;

        while (_types.Count > 0) 
        { 
            Type type = _types.Pop();
            var middleware = Activator.CreateInstance(type, handler) as IMiddleware;
            handler = middleware!.RunTask;
        }
        return handler;
    }
    private void TerminalMiddleware(HttpListenerContext context) => context.Response.Close();
    
}

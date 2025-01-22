using AslanAspNet.Server.Middlewares.Abstract;
using AslanAspNet.Server.Handlers;
using System.Net;

namespace AslanAspNet.Server.Middlewares.Concrete;

public class HtmlFileMiddleware : IMiddleware
{
    public HttpHandler Next { get; set ; }
    public HtmlFileMiddleware(HttpHandler next) => Next = next ;

    public void RunTask(HttpListenerContext context)
    {
        var url = context.Request.RawUrl;
        var page = url?.Split('/')[1];
        var fileLocation = $"./wwwroot/{page}.html";

        if (!File.Exists(fileLocation)) fileLocation = "./wwwroot/error.html";

        var htmlRaw = File.ReadAllText(fileLocation);
        context.Response.ContentType = "text/html";

        using (var stream = context.Response.OutputStream)
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(htmlRaw);
            }
        }

        Next?.Invoke(context); // <- Вызов след. m\w в цепи
    }
}

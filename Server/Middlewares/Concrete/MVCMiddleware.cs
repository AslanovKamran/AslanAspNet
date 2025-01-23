using System.Net;
using System.Reflection;
using AslanAspNet.Server.Handlers;
using AslanAspNet.Server.Middlewares.Abstract;

namespace AslanAspNet.Server.Middlewares.Concrete;

internal class MVCMiddleware : IMiddleware
{
    public HttpHandler Next { get; set; }
    public MVCMiddleware(HttpHandler next) => Next = next;

    //Ex: /Home/Index
    public void RunTask(HttpListenerContext context)
    {
        var request = context.Request;
        var response = context.Response;
        try
        {
            var controllerName = request.RawUrl!.Split('/')[1] + "Controller"; // HomeController
            var actionName = request.RawUrl.Split('/')[2]; //Index

            // Ищем класс по названию через Reflection;
            var controllerType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name.ToLower().Contains(controllerName.ToLower()));

            if (controllerType == null) { Error(context); return; }

            //Ищем метод из контроллера по названию
            var actionType = controllerType.GetMethods()
                .FirstOrDefault(x => x.Name.ToLower() == actionName.ToLower());

            if (actionType == null) { Error(context); return; }

            //Создание объекта контроллера
            var controller = Activator.CreateInstance(controllerType);
            var result = actionType.Invoke(controller, null); // null для параметров метода
            //result хранит в себе "<h1>Index from Home</h1>"; 
            response.ContentType = "text/html";

            using (var writer = new StreamWriter(response.OutputStream))
            {
                writer.Write(result);
            }
        }
        catch (Exception)
        {
            Next?.Invoke(context);
        }
        finally
        {
            Next?.Invoke(context);
        }
    }

    private void Error(HttpListenerContext context)
    {
        context.Response.Redirect($@"http://localhost:8080/error.html");
        context.Response.Close();
    }
}

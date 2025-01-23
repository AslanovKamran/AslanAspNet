using AslanAspNet.Server.Middlewares.Abstract;
using AslanAspNet.Server.Handlers;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace AslanAspNet.Server.Middlewares.Concrete;

public class StaticFilesMiddleware : IMiddleware
{
    public HttpHandler Next { get; set; }
    public StaticFilesMiddleware(HttpHandler next) => Next = next;

    public void RunTask(HttpListenerContext context)
    {
        var request = context.Request; var response = context.Response;

        // Return early if the request URL doesn't have an extension
        if (!Path.HasExtension(request.RawUrl)) { Next?.Invoke(context); return; }

        var file = request.RawUrl.Split('/')[1]; // <- Берем лишь имя файла cat.jpg
        var currentDir = Directory.GetCurrentDirectory();
        var fileLocation = Path.Combine(currentDir, "wwwroot", file); // Полный путь к файлу cat.jpg

        // Return early if the file doesn't exist
        if (!File.Exists(fileLocation)) { Next?.Invoke(context); return; }

        var bytes = File.ReadAllBytes(fileLocation);
        var fileExtension = Path.GetExtension(fileLocation);

        response.ContentType = fileExtension switch
        {
            ".html" => "text/html",
            ".css" => "text/css",
            ".jpg" => "image/jpeg",
            ".ico" => "image/x-icon",
            _ => "application/octet-stream" // Default 
        };
        using (var binWriter = new BinaryWriter(response.OutputStream))
        {
            binWriter.Write(bytes);
        }
        Next?.Invoke(context);
    }
}

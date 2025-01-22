using AslanAspNet.Server.Handlers;
namespace AslanAspNet.Server;
using System.Net;
using System.Net.Http.Headers;
using AslanAspNet.Server.Builders;
using AslanAspNet.Server.Configurations.Abstract;

public class MyServer
{
  
    public string Domain { get; set; }
    public int Port { get; set; }

    private HttpListener _listener; 
    private HttpHandler _middleware;

    public MyServer(string domain, int port)
    {
        Domain = domain;
        Port = port;

        _listener = new HttpListener();
        _listener.Prefixes.Add($"http://{Domain}:{Port}/");
     
    }

    public void Configure<T>()  where T : IConfigurator, new()
    {
        var config = new T();
        var builder = new MiddlewareBuilder();
        config.Configure(builder);
        _middleware = builder.Build();
    }

    public void Run()
    {
        _listener.Start();
        while (true)
        {
            var context = _listener.GetContext();
            Process(context);
        }
    }

    public void Process(HttpListenerContext context)
    {
        _middleware?.Invoke(context);
        context.Response.Close();
    }

    
}

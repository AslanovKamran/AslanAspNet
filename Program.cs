using AslanAspNet.Server;
using AslanAspNet.Server.Configurations.Concrete;

var domain = "localhost";
var port = 8080;

var myServer = new MyServer(domain, port);
myServer.Configure<StartupConfigurator>();
Console.WriteLine("Server started ...\n");
myServer.Run();


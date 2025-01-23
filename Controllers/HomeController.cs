using AslanAspNet.Server.ControllerBase;

namespace AslanAspNet.Controllers;

public class HomeController : Controller
{
    //Home/Index
    public string Index()
    {
        //Зашли в БД и вытянули данные юзера
        var response = @"
                    <ul>
                    <li><p>Name: Aslan</p></li>
                    <li><p>Surname: Aslanov</p></li>
                    </ul>";
        return response;
    }

    //Home/About
    public string About() => "<h1>About from Home</h1>";
}

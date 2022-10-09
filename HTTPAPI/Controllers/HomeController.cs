using Microsoft.AspNetCore.Mvc;

namespace HTTPAPI.Controllers
{
   [Route("/")]
   public class HomeController : Controller
   {
      [HttpGet]
      public string Index()
      {
         return "online";
      }
   }
}
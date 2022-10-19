using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
   [Route("/")]
   public class HomeController : Controller
   {
      [HttpGet]
      public IActionResult Index()
      {
         return View();
      }

      [HttpGet("secret")]
      [Authorize]
      public IActionResult GetSecretInfo()
      {
         return View("Secret", new SecretInfo());
      }
   }

   public class SecretInfo
   {
      public string Secret => "secret123";
   }
}
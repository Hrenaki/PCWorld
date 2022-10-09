using Microsoft.AspNetCore.Mvc;
using Core;
using Core.UserZone;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace HTTPAPI.Controllers
{
   [Route("/users")]
   public class UserController : Controller
   {
      private readonly IUserAuthenticationService authenticationService;

      public UserController(IUserAuthenticationService authenticationService)
      {
         this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
      }

      [HttpPost("signin")]
      public string SignIn([FromBody] JObject content)
      {
         if (content == null)
            return JsonConvert.SerializeObject(new { Successful = false, Message = "Params were empty" }).ToString();

         if (!content.ContainsKey("username"))
            return JsonConvert.SerializeObject(new Result() { Successful = false, Message = "Params don't contain 'username'" }).ToString();

         if(!content.ContainsKey("password"))
            return JsonConvert.SerializeObject(new Result() { Successful = false, Message = "Params don't contain 'password'" }).ToString();

         var username = content["username"]!.Value<string>();
         if(string.IsNullOrEmpty(username))
            return JsonConvert.SerializeObject(new Result() { Successful = false, Message = "Username was empty" }).ToString();

         var password = content["password"]!.Value<string>();
         if (string.IsNullOrEmpty(password))
            return JsonConvert.SerializeObject(new Result() { Successful = false, Message = "Password was empty" }).ToString();

         var result = authenticationService.TrySignIn(username, password, out _);
         return JsonConvert.SerializeObject(result);
      }
   }
}
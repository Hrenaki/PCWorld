using Core.UserZone;
using Data.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.ViewModels;

namespace Web.Controllers
{
   [Route("/account")]
   public class AccountController : Controller
   {
      private IUserAuthenticationService authenticationService;

      public AccountController(IUserAuthenticationService service)
      {
         authenticationService = service;
      }

      [HttpGet("login")]
      public IActionResult Login()
      {
         return PartialView();
      }

      [HttpGet("register")]
      public IActionResult Register()
      {
         return PartialView();
      }

      [HttpPost("loginAction")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> LoginAction(LoginUserModel model)
      {
         if (!ModelState.IsValid)
            return PartialView("Login", model);

         Result result = authenticationService.TrySignIn(model.Username, model.Password, out UserEntity? userEntity);
         if (!result.Successful)
         {
            ModelState.AddModelError(string.Empty, result.Message);
            return PartialView("Login", model);
         }

         await Authenticate(userEntity!.Name, userEntity!.UserRoleEntity.Name);
         return PartialView("Login", model);
      }

      [HttpPost("registerAction")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> RegisterAction(RegisterUserModel model)
      {
         if (!ModelState.IsValid)
            return PartialView("Register", model);

         Result result = authenticationService.TryRegister(model.Username, model.Password, model.Email, out UserEntity? userEntity);
         if(!result.Successful)
         {
            ModelState.AddModelError("", result.Message);
            return PartialView("Register", model);
         }

         await Authenticate(userEntity!.Name, userEntity!.UserRoleEntity.Name);
         return PartialView("Register", model);
      }

      private async Task Authenticate(string username, string userRole)
      {
         var claims = new List<Claim>()
         {
            new Claim(ClaimsIdentity.DefaultNameClaimType, username),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole)
         };

         ClaimsIdentity ci = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci));
      }
   }
}
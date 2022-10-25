using Microsoft.AspNetCore.Mvc;

namespace Web.Extensions
{
   public static class ControllerExtensions
   {
      private static string controllerStr = nameof(Controller);

      public static string GetName<T>() where T : Controller
      {
         string currentControllerFullname = typeof(T).Name;
         return currentControllerFullname.Substring(0, currentControllerFullname.Length - controllerStr.Length);
      }
   }
}
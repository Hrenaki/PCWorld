using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
   public class LoginUserModel
   {
      [Required(ErrorMessage = $"{nameof(Username)} isn't set")]
      public string Username { get; set; }

      [Required(ErrorMessage = $"{nameof(Password)} isn't set")]
      [DataType(DataType.Password)]
      public string Password { get; set; }
   }
}
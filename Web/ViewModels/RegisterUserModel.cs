using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
   public class RegisterUserModel
   {
      [Required(ErrorMessage = $"{nameof(Username)} isn't set")]
      public string Username { get; set; }

      [Required(ErrorMessage = $"{nameof(Email)} isn't set")]
      [DataType(DataType.EmailAddress)]
      public string Email { get; set; }

      [Required(ErrorMessage = $"{nameof(Password)} isn't set")]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Compare(nameof(Password), ErrorMessage = $"Passwords aren't equal")]
      [DataType(DataType.Password)]
      public string RepeatedPassword { get; set; }
   }
}
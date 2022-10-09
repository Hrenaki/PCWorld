using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Core.UserZone
{
	public class Result
	{
		public bool Successful { get; set; }
		public string Message { get; set; }
	}

	public interface IUserAuthenticationService
	{
		public Result TryRegister(string username, string password, string email, out User? user);
      public Result TrySignIn(string username, string password, out User? user);
	}

	public static class UserAuthenticationServiceFactory
	{
		public static IUserAuthenticationService CreateDefault(IHashService hashService, IUserService userService)
		{
			return new UserAuthenticationService(hashService, userService);
		}
	}

	internal class UserAuthenticationService : IUserAuthenticationService
   {
		private readonly IHashService hashService;
		private readonly IUserService userService;

		private readonly static string UserExistsMessage = "User exists";
      private readonly static string NotFoundUserMessageFormat = "Can't find user with username '{0}'";
      private readonly static string UserPasswordNotValidMessage = "Password isn't valid";

      public UserAuthenticationService(IHashService hashService, IUserService userService)
		{
			this.hashService = hashService;
			this.userService = userService;
		}

		public Result TryRegister(string username, string password, string email, out User? user)
		{
			user = null;

			bool isUserExist = userService.TryGetUserByName(username, out _);
			if (isUserExist)
				return new Result() { Successful = false, Message = UserExistsMessage };

			if (!userService.TryAddUser(username, hashService.GetStringHash(password, username), email, out user))
				return new Result() { Successful = false, Message = "Can't register user" };

			return new Result() { Successful = true, Message = "Registered" };
		}

		public Result TrySignIn(string username, string password, out User? user)
		{
         if (!userService.TryGetUserByName(username, out user))
            return new Result() { Successful = false, Message = string.Format(NotFoundUserMessageFormat, username) };

         bool isPasswordValid = hashService.GetStringHash(password, username) == user!.PasswordHash;
         if (!isPasswordValid)
            return new Result() { Successful = false, Message = UserPasswordNotValidMessage };

         return new Result() { Successful = true, Message = "Signed in" };
      }
	}
}
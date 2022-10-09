using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Core.UserZone
{
	public interface IHashService
	{
		public string GetStringHash(string input, string salt);
	}

	public static class HashServiceFactory
	{
		public static IHashService CreateDefault()
		{
			return new HashService();
		}
	}

   internal class HashService : IHashService
	{
		public string GetStringHash(string input, string salt)
		{
			var saltedInput = string.Concat(input, salt);

			using(var sha256Algorithm = SHA256.Create())
			{
				byte[] hashBytes = sha256Algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltedInput));
				return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
			}
		}
	}
}

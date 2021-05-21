using System;
using System.Security.Cryptography;

namespace Fitverse.MembersService.Helpers
{
	public static class PasswordHasher
	{
		public static string Hash(string password)
		{
			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
			var hash = pbkdf2.GetBytes(20);

			var hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			var savedPasswordHash = Convert.ToBase64String(hashBytes);

			return savedPasswordHash;
		}

		public static bool UnHash(string password, string savedPassword)
		{
			var hashBytes = Convert.FromBase64String(savedPassword);
			var salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
			var hash = pbkdf2.GetBytes(20);
			for (var i = 0; i < 20; i++)
			{
				if (hashBytes[i + 16] != hash[i])
					return false;
			}

			return true;
		}
	}
}
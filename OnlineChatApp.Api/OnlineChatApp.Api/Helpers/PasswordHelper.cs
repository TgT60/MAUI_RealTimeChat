using BCrypt.Net;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace OnlineChatApp.Api.Helpers
{
	public class PasswordHelper
	{
		public byte[] GenerateSalt()
		{
			byte[] salt = new byte[16];
			using (var rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(salt);
			}
			return salt;
		}

		public string HashPassword(string password, byte[] salt)
		{
			string encryptedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));
			return encryptedPassword;
		}

	}
}

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineChatApp.Api.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace OnlineChatApp.Api.Functions.User
{
	public class UserFunction : IUserFunction
	{
		private readonly ChatAppContext _chatAppContext;

        public UserFunction(ChatAppContext chatAppContext)
        {
			_chatAppContext = chatAppContext;
		}


        public User? Authenticate(string loginId, string password)
		{
			try 
			{
				var entity = _chatAppContext.TblUsers.SingleOrDefault(x => x.LoginId == loginId);
				
				if (entity == null) return null;

				var isPasswordMatched = VerifyPassword(password,entity.StoredSalt,entity.Password);

				if (!isPasswordMatched) return null;

				var token = GenerateJwtToken(entity);

				return new User
				{
					Id = entity.Id,
					UserName = entity.UserName,
					Token = token,
				};
			}
			catch (Exception ex)
			{
				return null;
			}
		}
        public TblUser Register(string userName, string loginId, string password)
        {
	        var existingUser = _chatAppContext.TblUsers.SingleOrDefault(x => x.LoginId == loginId);

	        if (existingUser != null)
	        {
		        return null;
	        }

	        var passwordHelper = new PasswordHelper();
	        var salt = passwordHelper.GenerateSalt();
	        var hashedPassword = passwordHelper.HashPassword(password, salt);

	        var newUser = new TblUser
	        {
		        UserName = userName,
		        LoginId = loginId,
		        Password = hashedPassword,
		        AvatarSourceName = "Test",
		        StoredSalt = salt,
		        IsOnline = false,
		        LastLogonTime = DateTime.Now
	        };

	        _chatAppContext.TblUsers.Add(newUser);
	        _chatAppContext.SaveChanges();

	        Authenticate(loginId, password);

	        return newUser;
        }

		public User GetUserById(int id)
		{
			var entity =  _chatAppContext.TblUsers
				.Where(x => x.Id == id)
				.FirstOrDefault();
			if (entity == null) return new User();

			return new User 
			{
				UserName = entity.UserName,
				Id = entity.Id,
				AvatarSourceName = entity.AvatarSourceName,
				AwayDuration = "",
				IsOnline = entity.IsOnline,
				LastLogonTime = entity.LastLogonTime
			};
		}

		public async Task<IEnumerable<User>> GetMembers()
		{
			var tblUsers = await _chatAppContext.TblUsers.ToListAsync();

			if (tblUsers == null) return new List<User>();

			var allUsers = tblUsers.Select(tblUsers => new User
			{
				UserName = tblUsers.UserName,
				Id = tblUsers.Id,
				AvatarSourceName = tblUsers.AvatarSourceName,
				AwayDuration = "",
				IsOnline = tblUsers.IsOnline,
				LastLogonTime = tblUsers.LastLogonTime
			});

			return allUsers;
		}


		private bool VerifyPassword(string enteredPassword, byte[] storedSalt, string storedPassword)
		{ 
			string encryptyedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: enteredPassword,
				salt: storedSalt,
				prf:KeyDerivationPrf.HMACSHA1,
				iterationCount:10000,
				numBytesRequested: 256 / 8));

			return encryptyedPassword.Equals(storedPassword);
		}

		private string GenerateJwtToken(TblUser user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("1234567890123456");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}


	}

}

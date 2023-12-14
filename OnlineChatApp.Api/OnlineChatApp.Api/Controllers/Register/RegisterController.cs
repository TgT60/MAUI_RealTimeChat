using Microsoft.AspNetCore.Mvc;

namespace OnlineChatApp.Api.Controllers.Register
{
	[ApiController]
	[Route("[controller]")]
	public class RegisterController : Controller
	{
		private IUserFunction _userFunction;

		public RegisterController(IUserFunction userFunction)
		{
			_userFunction = userFunction;
		}
		
		[HttpPost("Register")]
		public IActionResult Register(RegisterRequest request)
		{

			var registerResponse = _userFunction.Register(request.UserName, request.LoginId, request.Password);
			if (registerResponse == null)
			{
				return BadRequest(new { StatusMessage = "Login name have already exist" });
			}

			var authResponse = _userFunction.Authenticate(request.LoginId, request.Password);

			return Ok(authResponse);
		}
	}
}

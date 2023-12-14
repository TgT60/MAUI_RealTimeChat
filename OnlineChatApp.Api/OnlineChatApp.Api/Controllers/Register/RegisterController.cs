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

			var response = _userFunction.Register(request.UserName, request.LoginId, request.Password);
			if (response == null)
			{
				return BadRequest(new { StatusMessage = "Login name have already exist" });
			}

			return Ok(response);


		}
	}
}

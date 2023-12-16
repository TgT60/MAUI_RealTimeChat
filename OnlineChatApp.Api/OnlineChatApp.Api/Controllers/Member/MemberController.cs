using Microsoft.AspNetCore.Mvc;

namespace OnlineChatApp.Api.Controllers.Member
{
	[ApiController]
	[Route("[controller]")]
    //[Authorize]
	public class MemberController : Controller
	{
		private IUserFunction _userFunction;

		public MemberController(IUserFunction userFunction)
		{
			_userFunction = userFunction;
		}

		[HttpPost("Members")]
		public async Task<ActionResult> GetAllMembers([FromBody] int userId)
		{
			var response = new MemberResponse
			{
				User = _userFunction.GetUserById(userId),
				AllMembers = await _userFunction.GetMembers()
			};
			return Ok(response);
		}
	}
}

using System.Threading.Tasks;
using Actvt.Application.Followers;
using Microsoft.AspNetCore.Mvc;

namespace Actvt.API.Controllers
{
    public class FollowController : BaseApiController
    {

        [HttpPost("{username}")]

        public async Task<IActionResult> Follow(string username)
        {
            return HandleResult(await Mediator.Send(new FollowerToggle.Command{TargetUsername = username}));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetFollowing(string username, string predicate)
        {
            return HandleResult(await Mediator.Send(new List.Query{Username = username,Predicate=predicate}));
        }
    }
}
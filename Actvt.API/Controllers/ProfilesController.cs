using System.Threading.Tasks;
using Actvt.Application.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Actvt.API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Username = username }));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditProfile(Edit.Command command)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { DisplayName=command.DisplayName,Bio=command.Bio }));
        }

    }
}
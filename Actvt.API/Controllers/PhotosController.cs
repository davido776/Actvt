using System.Threading.Tasks;
using Actvt.Application.Photos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Actvt.API.Controllers
{
    public class PhotosController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Add2()
        {
            return Ok("testing");
            //return HandleResult(await Mediator.Send(command));
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Add.Command command)
        {
            
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("id")]

        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command{Id = id}));
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMain(string id)
        {
            return HandleResult(await Mediator.Send(new SetMain.Command{Id = id}));
        }


    }
}
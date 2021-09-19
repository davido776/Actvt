using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actvt.Application.Activities;
using Actvt.Application.Core;
using Actvt.Application.Profiles;
using Actvt.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Actvt.API.Controllers
{
    
    public class ActivitiesController : BaseApiController
    {
        

        [HttpGet]
        public async Task<IActionResult> GetActivities([FromQuery]ActivityParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query{Params = param}));
        }
        
       
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            var result = await Mediator.Send(new Application.Activities.Details.Query{Id = id});
            return HandleResult(result);
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            return HandleResult(await Mediator.Send(new Create.Command{Activity = activity}));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
             activity.Id = id;
             return Ok(await Mediator.Send(new Application.Activities.Edit.Command{Activity = activity}));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command{Id = id}));
        }


        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return HandleResult(await Mediator.Send(new UpdateAttendance.Command{Id = id}));
        }

        
    }
}
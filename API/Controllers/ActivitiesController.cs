using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
    
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken cancellationToken){
            
            // for(int i=0;i<10;i++){
            //     await Task.Delay(1000,cancellationToken);
            // }

            return await Mediator.Send(new List.Query());
        }
//"{id}"
         [HttpGet("{id}")] // activities/id //[HttpGet("{id}/{ad}")] // activities/id
        public async Task<ActionResult<Activity>> GetActivities(Guid id){
            
            Details.Query query = new Details.Query();
            query.Id = id;

            
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActiviy([FromBody]Activity activity) {

             return Ok(await Mediator.Send(new Create.Command { Activity = activity}));

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActiviy(Guid id,[FromBody]Activity activity) {

             activity.Id = id;
             return Ok(await Mediator.Send(new Edit.Command { Activity = activity}));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActiviy(Guid id) {

           
             return Ok(await Mediator.Send(new Delete.Command { Id = id}));

        }


    }
}
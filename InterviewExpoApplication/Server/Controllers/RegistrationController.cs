using InterviewExpoApplication.Shared.EventRegistration;
using Microsoft.AspNetCore.Mvc;

namespace InterviewExpoApplication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        [HttpPost]
        public IActionResult RegisterEvent([FromBody]CreateEventRegistrationDto eventRegistrationDto)
        {
            if(ModelState.IsValid)
            {
                return Ok(eventRegistrationDto);
            }


            return BadRequest(ModelState);
        }
    }
}
